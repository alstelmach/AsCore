using System;

namespace AsCore.Infrastructure.Workers
{
    public sealed class ScheduleConfig<TJob> : IScheduleConfig<TJob> where TJob : CronJobService
    {
        public string CronExpression { get; init; }
        public TimeZoneInfo TimeZoneLocal { get; init; }
        public TimeZoneInfo TimeZoneUtc { get; init; }
        public bool TriggerOnStartup { get; init; }
    }
}
