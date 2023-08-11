using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Abstraction.Queue;
using JoberMQ.Implementation.Queue.Default;

namespace JoberMQ.Factories.Queue
{
    internal class QueueFactory
    {
        internal static IMessageQueue Create(
            QueueMatchTypeFactoryEnum queueMatchTypeFactoryEnum, 
            QueueOrderOfSendingTypeFactoryEnum queueOrderOfSendingTypeFactory,

            string queueKey,
            string[] tags,
            QueueMatchTypeEnum queueMatchType, 
            QueueOrderOfSendingTypeEnum queueOrderOfSendingType, 
            PermissionTypeEnum permissionType, 
            bool isDurable,
            bool isDefault,
            bool isActive)
        {
            IMessageQueue result;

            

            switch (queueMatchTypeFactoryEnum)
            {
                case QueueMatchTypeFactoryEnum.Default:
                    switch (queueMatchType)
                    {
                        case QueueMatchTypeEnum.ClientKey:
                            switch (queueOrderOfSendingTypeFactory)
                            {
                                case QueueOrderOfSendingTypeFactoryEnum.Default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueClientKeyFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueClientKeyPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueClientKeyFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueClientKeyLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                                default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueClientKeyFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueClientKeyPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueClientKeyFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueClientKeyLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                            }
                            break;
                        case QueueMatchTypeEnum.InOrder:
                            switch (queueOrderOfSendingTypeFactory)
                            {
                                case QueueOrderOfSendingTypeFactoryEnum.Default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueInOrderFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueInOrderPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueInOrderFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueInOrderLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                                default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueInOrderFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueInOrderPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueInOrderFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueInOrderLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                            }
                            break;
                        case QueueMatchTypeEnum.All:
                            switch (queueOrderOfSendingTypeFactory)
                            {
                                case QueueOrderOfSendingTypeFactoryEnum.Default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueAllFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueAllPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueAllFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueAllLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                                default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueAllFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueAllPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueAllFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueAllLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                            }
                            break;
                        case QueueMatchTypeEnum.Tag:
                            switch (queueOrderOfSendingTypeFactory)
                            {
                                case QueueOrderOfSendingTypeFactoryEnum.Default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueTagFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueTagPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueTagFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueTagLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                                default:
                                    switch (queueOrderOfSendingType)
                                    {
                                        case QueueOrderOfSendingTypeEnum.Free:
                                            result = new DefaultMessageQueueTagFree(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.Priority:
                                            result = new DefaultMessageQueueTagPriority(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.FIFO:
                                            result = new DefaultMessageQueueTagFIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        case QueueOrderOfSendingTypeEnum.LIFO:
                                            result = new DefaultMessageQueueTagLIFO(queueKey, tags, queueMatchType, queueOrderOfSendingType, permissionType, isDurable, isDefault, isActive);
                                            break;
                                        default:
                                            throw new System.Exception("none");
                                    }
                                    break;
                            }
                            break;
                        default:
                            throw new System.Exception("none");
                    }
                    break;
                default:
                    throw new System.Exception("none");
            }

            

            return result;
        }
    }
}
