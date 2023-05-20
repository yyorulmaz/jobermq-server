//using JoberMQ.Library.Enums.Jober;
//using JoberMQ.State.Abstraction;
//using JoberMQ.State.Implementation.Default;

//namespace JoberMQ.State.Factories
//{
//    internal class JoberStateFactory
//    {
//        internal static IJoberState Create(JoberStateFactoryEnum factory)
//        {
//            IJoberState joberState;

//            switch (factory)
//            {
//                case JoberStateFactoryEnum.Default:
//                    joberState = new DfJoberState();
//                    break;
//                default:
//                    joberState = new DfJoberState();
//                    break;
//            }

//            return joberState;
//        }
//    }
//}
