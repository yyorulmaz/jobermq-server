using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Distributor;
using JoberMQ.Common.Enums.Permission;
using JoberMQ.Common.Enums.Queue;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Base;
using JoberMQ.Common.Models.Response;
using JoberMQ.Abstraction.Broker;
using JoberMQ.Factories.Distributor;
using JoberMQ.Factories.Queue;

namespace JoberMQ.Implementation.Broker.Default
{
    //todo distributor ve queue işlemlerinde distributor e bağlı queue bağlı ise işlem yapılmayabilir veya queue içerisinde mesajlar var ise işlem yaptırmayabiliriz butarz durumları düşün
    internal class DefaultMessageBroker : IMessageBroker
    {
        public async Task<ResponseBaseModel> ImportDatabaseDistributors()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.1");

            foreach (var item in JoberHost.JoberMQ.Database.Distributor.GetAll(x => x.IsActive == true))
            {
                try
                {
                    var dis = DistributorFactory.Create(JoberHost.JoberMQ.Configuration.ConfigurationDistributor.DistributorFactory, item.DistributorKey, item.DistributorType, item.DistributorSearchSourceType, item.PermissionType, item.IsDurable, item.IsDefault);
                    JoberHost.JoberMQ.Distributors.Add(item.DistributorKey, dis);
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.2");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> ImportDatabaseQueues()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.3");

            foreach (var item in JoberHost.JoberMQ.Database.Queue.GetAll(x => x.IsActive == true))
            {
                try
                {
                    var que = QueueFactory.Create(
                        JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueMatchTypeFactory,
                        JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueOrderOfSendingTypeFactory,
                        item.QueueKey,
                        item.Tags,
                        item.QueueMatchType, 
                        item.QueueOrderOfSendingType, 
                        item.PermissionType, 
                        item.IsDurable, 
                        item.IsDefault,
                        item.IsActive);
                    JoberHost.JoberMQ.Queues.Add(item.QueueKey, que);
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.4");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> CreateDefaultDistributors()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.5");


            foreach (var item in JoberHost.JoberMQ.Configuration.ConfigurationDistributor.DefaultDistributorConfigDatas)
            {
                try
                {
                    if (JoberHost.JoberMQ.Distributors.Get(item.Key) == null)
                    {
                        var dis = DistributorFactory.Create(
                            JoberHost.JoberMQ.Configuration.ConfigurationDistributor.DistributorFactory, 
                            item.Value.DistributorKey, 
                            item.Value.DistributorType, 
                            item.Value.DistributorSearchSourceType, 
                            item.Value.PermissionType, 
                            item.Value.IsDurable, 
                            item.Value.IsDefault);
                        JoberHost.JoberMQ.Distributors.Add(item.Value.DistributorKey, dis);
                    }
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.6");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> CreateDefaultQueues()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            result.IsSucces = true;
            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.7");

            foreach (var item in JoberHost.JoberMQ.Configuration.ConfigurationQueue.DefaultQueueConfigDatas)
            {
                try
                {
                    if (JoberHost.JoberMQ.Queues.Get(item.Key) == null)
                    {
                        var que = QueueFactory.Create(
                            JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueMatchTypeFactory,
                            JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueOrderOfSendingTypeFactory, 
                            item.Value.QueueKey, 
                            item.Value.Tags, 
                            item.Value.QueueMatchType.Value, 
                            item.Value.QueueOrderOfSendingType.Value, 
                            item.Value.PermissionType, 
                            item.Value.IsDurable, 
                            item.Value.IsDefault, 
                            item.Value.IsActive);
                        JoberHost.JoberMQ.Queues.Add(item.Value.QueueKey, que);
                    }
                }
                catch (Exception)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.8");
                }
            }

            return result;
        }
        public async Task<ResponseBaseModel> QueueSetMessages()
        {
            await Task.Delay(0);
            var result = new ResponseBaseModel();
            var errMsgList = "Add Message Error Id List : ";

            //todo kontrol et
            var messages = JoberHost.JoberMQ.Database.Message.GetAll(x => x.IsActive == true && x.IsDelete == false && x.Status.StatusTypeMessage == StatusTypeMessageEnum.None);

            foreach (var msg in messages)
            {
                var queue = JoberHost.JoberMQ.Queues.Get(msg.Message.Routing.QueueKey);
                var addResult = await queue.Queueing(msg);

                if (!addResult.IsSucces)
                {
                    result.IsSucces = false;
                    errMsgList += msg.Id + ",";
                    continue;
                }
            }

            if (result.IsSucces)
                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.9");
            else
                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.10") + " " + errMsgList;


            return result;
        }



        public async Task<ResponseBaseModel> DistributorCreate(string distributorKey, DistributorTypeEnum distributorType, DistributorSearchSourceTypeEnum distributorSearchSourceType, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = JoberHost.JoberMQ.Distributors.Get(distributorKey);
                if (distributor != null)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.1");
                }
                else
                {
                    var newDistributor = DistributorFactory.Create(JoberHost.JoberMQ.Configuration.ConfigurationDistributor.DistributorFactory, distributorKey, distributorType, distributorSearchSourceType, PermissionTypeEnum.All, isDurable, false);
                    var resultDatabase = JoberHost.JoberMQ.Database.Distributor.Add(Guid.NewGuid(), new DistributorDbo
                    {
                        Id = Guid.NewGuid(),
                        DistributorKey = distributorKey,
                        DistributorType = distributorType,
                        PermissionType = permissionType,
                        IsDurable = isDurable
                    });
                    var resultDistributorAdd = JoberHost.JoberMQ.Distributors.Add(distributorKey, newDistributor);

                    if (resultDistributorAdd == true && resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.2");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.3");
                    }
                }
            });

            return result;
        }
        public async Task<ResponseBaseModel> DistributorUpdate(string distributorKey, PermissionTypeEnum permissionType, bool isDurable)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var distributor = JoberHost.JoberMQ.Distributors.Get(distributorKey);
                if (distributor == null)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.11");
                }
                else if (distributor.IsDefault)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.12");
                }
                else
                {
                    var getDatabaseDistributor = JoberHost.JoberMQ.Database.Distributor.Get(x => x.DistributorKey == distributorKey);
                    getDatabaseDistributor.DistributorKey = distributorKey;
                    getDatabaseDistributor.PermissionType = permissionType;
                    getDatabaseDistributor.IsDurable = isDurable;
                    var resultDatabase = JoberHost.JoberMQ.Database.Distributor.Update(getDatabaseDistributor.Id, getDatabaseDistributor);

                    distributor.DistributorKey = distributorKey;
                    distributor.PermissionType = permissionType;
                    distributor.IsDurable = isDurable;
                    var resultDistributorUpdate = JoberHost.JoberMQ.Distributors.Update(distributorKey, distributor);


                    if (resultDistributorUpdate == true && resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.13");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.14");
                    }
                }
            });



            return result;
        }
        public async Task<ResponseBaseModel> DistributorRemove(string distributorKey)
        {
            var result = new ResponseBaseModel();
            result.IsOnline = true;

            //todo burayı yap
            
            //await Task.Run(() =>
            //{
            //    var distributor = JoberHost.JoberMQ.Distributors.Get(distributorKey);
            //    if (distributor == null)
            //    {
            //        result.IsSucces = false;
            //        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.11");
            //    }
            //    else
            //    {
            //        JoberHost.JoberMQ.Configuration.ConfigurationDistributor.DefaultDistributorConfigDatas.TryGetValue(distributorKey, out var defaultDistributor);
            //        if (defaultDistributor != null)
            //        {
            //            result.IsSucces = false;
            //            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.15");
            //        }
            //        else
            //        {
            //            var checkBind = JoberHost.JoberMQ.Queues.GetAll(x => x.DistributorKey == distributorKey);
            //            if (checkBind != null && checkBind.Count > 0)
            //            {
            //                var errorMessage = ". Queue List : ";
            //                foreach (var item in checkBind)
            //                {
            //                    errorMessage += item.QueueKey + " , ";
            //                }

            //                result.IsSucces = false;
            //                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.16") + errorMessage;
            //            }
            //            else
            //            {
            //                var getDatabaseDistributor = JoberHost.JoberMQ.Database.Distributor.Get(x => x.DistributorKey == distributorKey);
            //                var resultDatabase = JoberHost.JoberMQ.Database.Distributor.Delete(getDatabaseDistributor.Id, getDatabaseDistributor);

            //                var resultDistributorRemove = messageDistributors.Remove(distributorKey);
            //                if (resultDistributorRemove != null && resultDatabase == true)
            //                {
            //                    result.IsSucces = true;
            //                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.17");
            //                }
            //                else
            //                {
            //                    result.IsSucces = false;
            //                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.18");
            //                }

            //            }
            //        }
            //    }
            //});

            return result;
        }



        public async Task<ResponseBaseModel> QueueCreate(string queueKey, string[] tags, QueueMatchTypeEnum queueMatchType, QueueOrderOfSendingTypeEnum queueOrderOfSendingType, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        {
            var result = new ResponseModel();
            result.IsOnline = true;
            
            await Task.Run(() =>
            {
                var queue = JoberHost.JoberMQ.Queues.Get(queueKey);
                if (queue != null)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.51");
                }
                else
                {
                    var resultDatabase = JoberHost.JoberMQ.Database.Queue.Add(Guid.NewGuid(), new QueueDbo
                    {
                        Id = Guid.NewGuid(),
                        QueueKey = queueKey,
                        QueueMatchType = queueMatchType,
                        QueueOrderOfSendingType = queueOrderOfSendingType,
                        PermissionType = permissionType,
                        IsDurable = isDurable
                    });

                    var newtQueue = QueueFactory.Create(
                        JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueMatchTypeFactory,
                        JoberHost.JoberMQ.Configuration.ConfigurationQueue.QueueOrderOfSendingTypeFactory,
                        queueKey,
                        tags,
                        queueMatchType,
                        queueOrderOfSendingType,
                        permissionType,
                        isDurable,
                        false,
                        isActive);
                    var resultQueueAdd = JoberHost.JoberMQ.Queues.Add(queueKey, newtQueue);

                    if (resultQueueAdd == true && resultDatabase == true)
                    {
                        result.IsSucces = true;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.52");
                    }
                    else
                    {
                        result.IsSucces = false;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.53");
                    }
                }
            });

            return result;
        }
        public async Task<ResponseBaseModel> QueueUpdate(string queueKey, string[] tags, PermissionTypeEnum permissionType, bool isDurable, bool isActive)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = JoberHost.JoberMQ.Queues.Get(queueKey);
                if (queue == null)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.54");
                }
                else
                {
                    JoberHost.JoberMQ.Configuration.ConfigurationQueue.DefaultQueueConfigDatas.TryGetValue(queueKey, out var defaultQueue);
                    if (defaultQueue != null)
                    {
                        result.IsSucces = false;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.55");
                    }
                    else
                    {
                        var getDatabaseQueue = JoberHost.JoberMQ.Database.Queue.Get(x => x.QueueKey == queueKey);
                        getDatabaseQueue.QueueKey = queueKey;
                        getDatabaseQueue.Tags = tags;
                        getDatabaseQueue.PermissionType = permissionType;
                        getDatabaseQueue.IsDurable = isDurable;
                        getDatabaseQueue.IsActive = isActive;
                        // TODO IsActive durumunu false ise kuyruğu kapatmalı yada durdurmalıyım
                        var resultDatabase = JoberHost.JoberMQ.Database.Queue.Update(getDatabaseQueue.Id, getDatabaseQueue);

                        queue.QueueKey = queueKey;
                        queue.Tags = tags;
                        queue.PermissionType = permissionType;
                        queue.IsDurable = isDurable;
                        queue.IsActive = isActive;
                        // TODO IsActive durumunu false ise kuyruğu kapatmalı yada durdurmalıyım
                        var resultQueueUpdate = JoberHost.JoberMQ.Queues.Update(queueKey, queue);


                        // TODO queueKey değiştirildiğinde SubscriptDbo nunda değiştirilmesi gerekir


                        if (resultQueueUpdate == true && resultDatabase == true)
                        {
                            result.IsSucces = true;
                            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.56");
                        }
                        else
                        {
                            result.IsSucces = false;
                            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.57");
                        }
                    }
                }
            });



            // todo kuşullar sağlandımı kontrol (permission kontrol, bu kuyruk var mı vb.)

            return result;
        }
        public async Task<ResponseBaseModel> QueueRemove(string queueKey)
        {
            var result = new ResponseModel();
            result.IsOnline = true;

            await Task.Run(() =>
            {
                var queue = JoberHost.JoberMQ.Queues.Get(queueKey);
                if (queue == null)
                {
                    result.IsSucces = false;
                    result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.54");
                }
                else
                {
                    JoberHost.JoberMQ.Configuration.ConfigurationQueue.DefaultQueueConfigDatas.TryGetValue(queueKey, out var defaultQueue);
                    if (defaultQueue != null)
                    {
                        result.IsSucces = false;
                        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.58");
                    }
                    else
                    {
                        var checkMessage = queue.ChildMessageCount;
                        if (checkMessage > 0)
                        {
                            result.IsSucces = false;
                            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.59");
                        }
                        else
                        {
                            var getDatabaseQueue = JoberHost.JoberMQ.Database.Queue.Get(x => x.QueueKey == queueKey);
                            var resultDatabase = JoberHost.JoberMQ.Database.Queue.Delete(getDatabaseQueue.Id, getDatabaseQueue);

                            var resultQueueRemove = JoberHost.JoberMQ.Queues.Remove(queueKey);
                            if (resultQueueRemove != null && resultDatabase == true)
                            {
                                result.IsSucces = true;
                                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.60");
                            }
                            else
                            {
                                result.IsSucces = false;
                                result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.61");
                            }
                        }
                    }
                }
            });

            return result;
        }
        public async Task<ResponseBaseModel> QueueMerge(string distributorKey, string queueKey)
        {
            //todo BU YAPI OLMAYACAK



            //todo default queue leri merge yaptırmamalıyım
            var result = new ResponseModel();
            result.IsOnline = true;

            //await Task.Run(() =>
            //{
            //    var distributor = JoberHost.JoberMQ.Distributors.Get(distributorKey);
            //    var queue = JoberHost.JoberMQ.Queues.Get(queueKey);
            //    if (distributor == null)
            //    {
            //        result.IsSucces = false;
            //        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.12");
            //    }
            //    if (queue == null)
            //    {
            //        result.IsSucces = false;
            //        result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.54");
            //    }
            //    else
            //    {
            //        var getDatabaseQueue = JoberHost.JoberMQ.Database.Queue.Get(x => x.QueueKey == queueKey);
            //        getDatabaseQueue.DistributorKey = distributorKey;
            //        var resultDatabase = JoberHost.JoberMQ.Database.Queue.Update(getDatabaseQueue.Id, getDatabaseQueue);

            //        queue.DistributorKey = distributorKey;
            //        var resultQueueUpdate = JoberHost.JoberMQ.Queues.Update(queueKey, queue);

            //        if (resultQueueUpdate == true && resultDatabase == true)
            //        {
            //            result.IsSucces = true;
            //            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.62");
            //        }
            //        else
            //        {
            //            result.IsSucces = false;
            //            result.Message = JoberHost.JoberMQ.StatusCode.GetStatusMessage("1.7.63");
            //        }
            //    }
            //});

            return result;
        }

    }
}
