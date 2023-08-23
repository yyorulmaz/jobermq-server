using JoberMQ.Abstraction.Timing;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Message;
using JoberMQ.Common.Enums.Operation;
using JoberMQ.Common.Enums.Priority;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Consuming;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Operation;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Routing;
using JoberMQ.Common.Models.Status;
using Microsoft.AspNetCore.Components.Forms;
using TimerFramework;

namespace JoberMQ.Implementation.Timing
{
    public class ScheduledNotifications : IScheduledNotifications
    {
        ITimer timer;
        public ScheduledNotifications()
        {
            var factory = new TimerFactory();
            timer = factory.CreateTimer();
        }
        public bool Start()
        {
            timer.Receive += Timer_Receive;
            TimerAdd(Guid.NewGuid(), "0 * * ? * * *", "ScheduledNotifications", "def.que.all.free.1m");
            TimerAdd(Guid.NewGuid(), "0 */5 * ? * * *", "ScheduledNotifications", "def.que.all.free.5m");
            TimerAdd(Guid.NewGuid(), "0 */15 * ? * * *", "ScheduledNotifications", "def.que.all.free.15m");
            TimerAdd(Guid.NewGuid(), "0 0 * ? * * *", "ScheduledNotifications", "def.que.all.free.1h");
            TimerAdd(Guid.NewGuid(), "0 0 */4 ? * * *", "ScheduledNotifications", "def.que.all.free.4h");
            TimerAdd(Guid.NewGuid(), "0 0 0 ? * * *", "ScheduledNotifications", "def.que.all.free.1d");

            return true;
        }

        private async void Timer_Receive(TimerModel obj)
        {
            var queue = JoberHost.JoberMQ.Queues.Get(obj.Data);
            


            char separator = '.';
            string[] parts = obj.Data.Split(separator);
            string extractedText = parts[parts.Length - 1];

            var messageDbo = MessageDefault(obj.Data, extractedText);


            await queue.Queueing(messageDbo);
        }

        public void TimerAdd(Guid id, string retryTimeCrone, string timerGroup, string data)
        {
            var checkOperationDataTimer = new TimerModel();
            checkOperationDataTimer.Id = id;
            checkOperationDataTimer.CronTime = retryTimeCrone;
            checkOperationDataTimer.TimerGroup = timerGroup;
            checkOperationDataTimer.Data = data;
            timer.Add(checkOperationDataTimer);
        }

        static MessageDbo MessageDefault(string queueKey, string msg)
        => new MessageDbo
        {
            Id = Guid.NewGuid(),
            Operation = new OperationModel
            {
                Version = 0,
                OperationType = OperationTypeEnum.Message
            },
            Producer = new ProducerModel
            {
                ClientKey = null,
            },
            Message = new MessageModel
            {
                MessageType = MessageTypeEnum.Text,
                Message = msg,
                Routing = new RoutingModel
                {
                    DistributorKey = null,
                    RoutingKey = null,
                    QueueKey = queueKey,
                    ClientKey = null
                },
                Info = null,
                GeneralData = null,
                PriorityType = PriorityTypeEnum.None,
                MessageConsuming = new ConsumingModel(),
            },
            IsResult = false,
            ResultMessage = null,
            TriggerGroupsId = null,
            CreatedJobId = null,
            CreatedJobDetailId = null,
            CreatedJobTransactionId = null,
            CreatedJobTransactionDetailId = null,
            EventGroupsId = null,
            Status = new StatusModel
            {
                IsCompleted = false,
                IsError = false,
                StatusTypeMessage = StatusTypeMessageEnum.None,
                TempAgainDate = null
            },
            IsDbTextSave = false
        };
    }
}
