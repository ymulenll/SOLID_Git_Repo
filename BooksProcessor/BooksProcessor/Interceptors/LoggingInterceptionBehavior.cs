using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using BooksProcessor.Interfaces;

namespace BooksProcessor
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        ILogger logger;
        public LoggingInterceptionBehavior(ILogger logger)
        {
            this.logger = logger;
        }
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            this.logger.LogInfo($"Method {input.MethodBase.Name} started");

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            this.logger.LogInfo(String.Format($"Method {input.MethodBase.Name} Finished"));

            return result;
        }
    }
}
