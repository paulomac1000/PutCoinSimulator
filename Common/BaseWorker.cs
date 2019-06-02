using System;
using Common.Interfaces;

namespace Common
{
    public abstract class BaseWorker : IBaseWorker
    {
        private readonly Action action;

        protected BaseWorker()
        {
            action = Work;
        }

        public void Start()
        {
            action.Invoke();
        }

        public abstract void Work();
    }
}
