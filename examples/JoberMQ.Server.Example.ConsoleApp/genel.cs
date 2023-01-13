#region Dependency

//#region ServerService
//services.AddDependencyServerService(config);
//var serverService = services.GetDependencyServerService();
//#endregion

//#region DbOperation
//// Database
//services.AddDependencyDbOperationDatabase(config);
//var dbOperationDatabase = services.GetDependencyDbOperationDatabase();

//// Logger
//services.AddDependencyDbLogger(config);
//var dbLoggerDR = services.GetDependencyDbLogger();

//// DbMem
//services.AddDependencyDbMem(
//    InMemoryDatabase.DbMem.JobDatas,
//    InMemoryDatabase.DbMem.JobDataDetails,
//    InMemoryDatabase.DbMem.Jobs,
//    InMemoryDatabase.DbMem.JobDetails,
//    InMemoryDatabase.DbMem.JobMessages,
//    InMemoryDatabase.DbMem.JobMessageLogs,
//    InMemoryDatabase.DbMem.EventSubscribers,
//    InMemoryDatabase.DbMem.ErrorMessages,
//    InMemoryDatabase.DbMem.Users
//    );
//var dbMem = services.GetDependencyDbMem();

//// DbMemSenk
//services.AddDependencyDbMemSenk(
//    InMemoryDatabase.DbMemSenk.JobDatas,
//    InMemoryDatabase.DbMemSenk.JobDataDetails,
//    InMemoryDatabase.DbMemSenk.Jobs,
//    InMemoryDatabase.DbMemSenk.JobDetails,
//    InMemoryDatabase.DbMemSenk.JobMessages,
//    InMemoryDatabase.DbMemSenk.JobMessageLogs,
//    InMemoryDatabase.DbMemSenk.EventSubscribers,
//    InMemoryDatabase.DbMemSenk.ErrorMessages,
//    InMemoryDatabase.DbMemSenk.Users
//    );
//var dbMemSenk = services.GetDependencyDbMemSenk();

//// DbRealy
//services.AddDependencyDbRealy(config);
//var dbRealy = services.GetDependencyDbRealy();

//// DbOperation
//services.AddDependencyDbOperationJobDataDetail(config, DatabaseTableEnum.JobDataDetail, dbMem.JobDataDetailMemDal, dbMemSenk.JobDataDetailMemSenkDal, dbRealy.JobDataDetailDal, dbLoggerDR);
//var dbOperationJobDataDetail = services.GetDependencyDbOperation().JobDataDetailDbOperation;
//services.AddDependencyDbOperationJobData(config, DatabaseTableEnum.JobData, dbMem.JobDataMemDal, dbMemSenk.JobDataMemSenkDal, dbRealy.JobDataDal, dbLoggerDR, dbOperationJobDataDetail);
//services.AddDependencyDbOperationJobDetail(config, DatabaseTableEnum.JobDetail, dbMem.JobDetailMemDal, dbMemSenk.JobDetailMemSenkDal, dbRealy.JobDetailDal, dbLoggerDR);
//var dbOperationJobDetail = services.GetDependencyDbOperation().JobDetailDbOperation;
//services.AddDependencyDbOperationJob(config, DatabaseTableEnum.Job, dbMem.JobMemDal, dbMemSenk.JobMemSenkDal, dbRealy.JobDal, dbLoggerDR, dbOperationJobDetail);
//services.AddDependencyDbOperationJobMessage(config, DatabaseTableEnum.JobMessage, dbMem.JobMessageMemDal, dbMemSenk.JobMessageMemSenkDal, dbRealy.JobMessageDal, dbLoggerDR);
//services.AddDependencyDbOperationJobMessageLog(config, DatabaseTableEnum.JobMessageLog, dbMem.JobMessageLogMemDal, dbMemSenk.JobMessageLogMemSenkDal, dbRealy.JobMessageLogDal, dbLoggerDR);
//services.AddDependencyDbOperationEventSubscriber(config, DatabaseTableEnum.EventSubscriber, dbMem.EventSubscriberMemDal, dbMemSenk.EventSubscriberMemSenkDal, dbRealy.EventSubscriberDal, dbLoggerDR);
//services.AddDependencyDbOperationErrorMessage(config, DatabaseTableEnum.ErrorMessage, dbMem.ErrorMessageMemDal, dbMemSenk.ErrorMessageMemSenkDal, dbRealy.ErrorMessageDal, dbLoggerDR);
//services.AddDependencyDbOperationUser(config, DatabaseTableEnum.User, dbMem.UserMemDal, dbMemSenk.UserMemSenkDal, dbRealy.UserDal, dbLoggerDR);
//var dbOperation = services.GetDependencyDbOperation();
//#endregion

//#region ClientServer
//services.AddDependencyClientServerService(InMemoryDatabase.Client.Clients, dbOperation.EventSubscriberDbOperation);
//var clientServerService = services.GetDependencyClientServerService();
//#endregion

//#region CloneDbo
//services.AddDependencyCloneDbo(dbOperation.JobDataDbOperation, dbOperation.JobDataDetailDbOperation);
//var cloneDbo = services.GetDependencyCloneDbo();
//#endregion

//#region Schedule
//services.AddDependencySchedule(dbOperation.JobDataDbOperation, dbOperation.JobDataDetailDbOperation, dbOperation.JobDbOperation, dbOperation.JobMessageDbOperation, dbOperation.EventSubscriberDbOperation, cloneDbo);
//var schedule = services.GetDependencySchedule();
//#endregion

//#region QueueMessage
//services.AddDependencyQueueMessage(InMemoryDatabase.QueueMessage.QueueMessages);
//var queueMessage = services.GetDependencyQueueMessage();
//#endregion

//#region QueueMessageChild
//services.AddDependencyQueueMessageChild(queueMessage);
//var queueMessageChild = services.GetDependencyQueueMessageChild();
//#endregion

//#region QueueMessageError
//services.AddDependencyQueueMessageError(InMemoryDatabase.QueueMessageError.QueueMessageErrors);
//var queueMessageError = services.GetDependencyQueueMessageError();
//#endregion

//#region QueueMessageErrorChild
//services.AddDependencyQueueMessageErrorChild(queueMessageError);
//var queueMessageErrorChild = services.GetDependencyQueueMessageErrorChild();
//#endregion

//#region MessageService
//services.AddDependencyMessageService(
//        config,
//        queueMessageChild.QueueMessageSpecial,
//        queueMessageChild.QueueMessageGroup,
//        queueMessageChild.QueueMessageQueueKey,
//        queueMessageErrorChild.QueueMessageErrorSpecial,
//        queueMessageErrorChild.QueueMessageErrorGroup,
//        queueMessageErrorChild.QueueMessageErrorQueueKey,
//        dbOperation.JobDataDbOperation,
//        dbOperation.JobDataDetailDbOperation,
//        dbOperation.JobDbOperation,
//        dbOperation.JobDetailDbOperation,
//        dbOperation.JobMessageDbOperation,
//        dbOperation.JobMessageLogDbOperation,
//        dbOperation.EventSubscriberDbOperation,
//        dbOperation.ErrorMessageDbOperation,

//        dbOperation.UserDbOperation, schedule);
//var messageService = services.GetDependencyMessageService();
//#endregion

//#region JoberHub
////services.AddDependencyJoberHub(serverService, clientServerService, messageService);
////var joberHub = services.GetDependencyJoberHub();
//#endregion

//#region CheckData
//services.AddDependencyCheckData(config, dbOperation.JobDataDbOperation, dbOperation.JobDataDetailDbOperation, dbOperation.JobDbOperation, dbOperation.JobDetailDbOperation, dbOperation.JobMessageDbOperation, dbOperation.JobMessageLogDbOperation, dbOperation.EventSubscriberDbOperation, dbOperation.ErrorMessageDbOperation, dbOperation.UserDbOperation);
//var checkData = services.GetDependencyCheckData();
//#endregion

//#region Clear
//services.AddDependencyClear(config, dbOperation.JobDataDbOperation, dbOperation.JobDataDetailDbOperation, dbOperation.JobDbOperation, dbOperation.JobDetailDbOperation, dbOperation.JobMessageDbOperation, dbOperation.JobMessageLogDbOperation, dbOperation.EventSubscriberDbOperation, dbOperation.ErrorMessageDbOperation, dbOperation.UserDbOperation);
//var clear = services.GetDependencyClear();
//#endregion

//#region AccountController
////services.AddDependencyAccountController(serverService, clientServerService, dbOperation.UserDbOperation);
////var dependencyAccountController = services.GetDependencyAccountController();
//#endregion

//#region BrokerMessage
//services.AddDependencyBrokerMessageSpecial(clientServerService, queueMessageChild.QueueMessageSpecial, dbOperation.JobMessageDbOperation, x => x.IsConsumer == true && x.IsConsumeSpecial == true && x.RowNumber > 0);
//services.AddDependencyBrokerMessageGroup(clientServerService, queueMessageChild.QueueMessageGroup, dbOperation.JobMessageDbOperation, x => x.IsConsumer == true && x.IsConsumeGroup == true && x.RowNumber > 0);
//services.AddDependencyBrokerMessageQueueKey(clientServerService, queueMessageChild.QueueMessageQueueKey, dbOperation.JobMessageDbOperation, x => x.IsConsumer == true && x.IsConsumeQueueKeys == true && x.RowNumber > 0);
//var brokerMessage = services.GetDependencyBrokerMessage();
//#endregion

//#region BrokerMessageError
//services.AddDependencyBrokerMessageErrorSpecial(clientServerService, queueMessageErrorChild.QueueMessageErrorSpecial, dbOperation.ErrorMessageDbOperation, x => x.IsActive == true && x.IsConsumer == true && x.IsConsumeErrorSpecial == true && x.RowNumber > 0);
//services.AddDependencyBrokerMessageErrorGroup(clientServerService, queueMessageErrorChild.QueueMessageErrorGroup, dbOperation.ErrorMessageDbOperation, x => x.IsActive == true && x.IsConsumer == true && x.IsConsumeErrorGroup == true && x.RowNumber > 0);
//services.AddDependencyBrokerMessageErrorQueueKey(clientServerService, queueMessageErrorChild.QueueMessageErrorQueueKey, dbOperation.ErrorMessageDbOperation, x => x.IsActive == true && x.IsConsumer == true && x.IsConsumeErrorQueueKeys == true && x.RowNumber > 0);
//var brokerMessageError = services.GetDependencyBrokerMessageError();
//#endregion

#endregion









//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//todo ssl sertifika sorunu için bunu yapılandırabilirsin
//        services.AddDataProtection()
//// searches the cert store for the cert with this thumbprint
//.ProtectKeysWithCertificate("3BCE558E2AD3E0E34A7743EAB5AEA2A9BD2575A0");


//        services.AddDataProtection()
//// only the local user account can decrypt the keys
//.ProtectKeysWithDpapi();

//        services.AddDataProtection()
//// all user accounts on the machine can decrypt the keys
//.ProtectKeysWithDpapi(protectToLocalMachine: true);

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

