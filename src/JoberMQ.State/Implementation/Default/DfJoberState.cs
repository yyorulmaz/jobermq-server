//using JoberMQ.State.Abstraction;
//using System;

//namespace JoberMQ.State.Implementation.Default
//{
//    internal class DfJoberState : IJoberState
//    {
//        bool isJoberActive { get; set; }
//        public bool IsJoberActive { get => isJoberActive; set => isJoberActive = value; }
//        public event Action<bool> IsJoberActiveEvent;
//        public void JoberActive(bool value) => IsJoberActiveEvent?.Invoke(value);
//    }
//}
