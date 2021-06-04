using System;
using Cronos;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Timer = System.Timers.Timer;

namespace AsCore.Infrastructure.Workers
{
    public abstract class CronJobService : IHostedService, IDisposable
    {
        private bool _hasBeenTriggeredOnStartup;
        private Timer _timer;

        protected CronJobService(string cronExpression,
            TimeZoneInfo timeZoneLocal,
            TimeZoneInfo timeZoneUtc,
            bool triggerOnStartup)
        {
            CronExpression = CronExpression.Parse(cronExpression);
            TimeZoneLocal = timeZoneLocal;
            TimeZoneUtc = timeZoneUtc;
            TriggerOnStartup = triggerOnStartup;
        }
        
        protected CronExpression CronExpression { get; }
        protected TimeZoneInfo TimeZoneLocal { get; }
        protected TimeZoneInfo TimeZoneUtc { get; }
        protected bool TriggerOnStartup { get; }

        public async Task StartAsync(CancellationToken cancellationToken) =>
            await ScheduleJob(cancellationToken);

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            await Task.CompletedTask;
        }
        
        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        private async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var initialTriggerRequired = TriggerOnStartup && !_hasBeenTriggeredOnStartup;
            
            if (initialTriggerRequired)
            {
                await ExecuteAsync(cancellationToken);
                _hasBeenTriggeredOnStartup = true;
            }
            
            var nextTriggerTime = CronExpression
                .GetNextOccurrence(DateTimeOffset.Now, TimeZoneLocal);

            if (!nextTriggerTime.HasValue)
            {
                return;
            }

            var delay = nextTriggerTime.Value - DateTimeOffset.Now;
            var demandsAnotherRecalculation = delay.TotalMilliseconds <= 0;

            if (demandsAnotherRecalculation)
            {
                await ScheduleJob(cancellationToken);
            }
            
            StartTimer(delay, cancellationToken);
            
            await Task.CompletedTask;
        }

        private void StartTimer(TimeSpan delay, CancellationToken cancellationToken)
        {
            _timer = new Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (_, _) =>
            {
                _timer.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    await ExecuteAsync(cancellationToken);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    await ScheduleJob(cancellationToken);
                }
            };

            _timer.Start();
        }
    }
}
