using System;

namespace AsCore.Infrastructure.Workers
{
    public interface IScheduleConfig<TJob> where TJob : CronJobService
    {
        string CronExpression { get; }
        TimeZoneInfo TimeZoneLocal { get; }
        TimeZoneInfo TimeZoneUtc { get; }
        bool TriggerOnStartup { get; }
    }
}
