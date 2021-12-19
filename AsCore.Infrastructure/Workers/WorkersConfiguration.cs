using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsCore.Infrastructure.Workers
{
    public static class WorkersConfiguration
    {
        private const string EmptyCronExpressionMessage = "Cron expression can't be empty.";
        
        public static IServiceCollection AddCronJob<TJob>(
            this IServiceCollection services,
            IConfiguration configuration,
            string cronExpressionSectionKey,
            string triggerOnStartupSectionKey = default) where TJob : CronJobService
        {
            var cronExpression = configuration.GetSection(cronExpressionSectionKey).Value;
            var missingCronExpression = string.IsNullOrEmpty(cronExpressionSectionKey)
                                        || string.IsNullOrWhiteSpace(cronExpression);

            if (missingCronExpression)
            {
                throw new ArgumentException(EmptyCronExpressionMessage);
            }
            
            var triggerOnStartup = bool.TryParse(
                configuration.GetSection(triggerOnStartupSectionKey).Value,
                out var parsingResult) && parsingResult;

            var config = new ScheduleConfig<TJob>
            {
                CronExpression = cronExpression,
                TimeZoneLocal = TimeZoneInfo.Local,
                TimeZoneUtc = TimeZoneInfo.Utc,
                TriggerOnStartup = triggerOnStartup
            };

            return services
                .AddSingleton<IScheduleConfig<TJob>>(config)
                .AddHostedService<TJob>();
        }
    }
}
