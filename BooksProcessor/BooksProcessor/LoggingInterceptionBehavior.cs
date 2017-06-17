﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace BooksProcessor
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            WriteLog(String.Format(
                "Invoking method {0} at {1}", 
                input.MethodBase,
                DateTime.Now.ToLongTimeString()));

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                WriteLog(String.Format(
                  "Method {0} threw exception {1} at {2}",
                  input.MethodBase, result.Exception.Message,
                  DateTime.Now.ToLongTimeString()));
            }
            else
            {
                WriteLog(String.Format(
                  "Method {0} returned {1} at {2}",
                  input.MethodBase, result.ReturnValue,
                  DateTime.Now.ToLongTimeString()));
            }
            return result;
        }

        private void WriteLog(string message)
        {
            Console.Write(message);
        }
    }
}
