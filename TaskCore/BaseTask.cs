using System;

namespace TaskCore
{
    public abstract class BaseTask : MarshalByRefObject
    {
        protected BaseTask()
        {

        }

        public abstract bool Run(out string resultMessage);
    }
}
