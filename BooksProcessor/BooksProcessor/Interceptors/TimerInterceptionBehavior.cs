using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using BooksProcessor.Interfaces;

namespace BooksProcessor
{
    public class TimerInterceptionBehavior : IInterceptionBehavior
    {
        ITimer timer;
        public TimerInterceptionBehavior(ITimer timer)
        {
            this.timer = timer;
        }
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            this.timer.Start();

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            this.timer.Stop();            

            return result;
        }
    }
}
