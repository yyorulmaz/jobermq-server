using JoberMQ.Abstraction.Timing;
using JoberMQ.Implementation.Timing;

namespace JoberMQ.Factories.Timing
{
    internal class ScheduledNotificationsFactory
    {
        internal static IScheduledNotifications Create()
        {
            IScheduledNotifications scheduledNotifications = new ScheduledNotifications();

            return scheduledNotifications;
        }
    }
}
