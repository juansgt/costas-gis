using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ObjectContainer.Transactions
{
    public class TransactionScopeBuilder
    {
        private static volatile TransactionScopeBuilder instance = null;
        private static readonly object padlock = new object();

        public TransactionScopeBuilder()
        {
        }

        public static TransactionScopeBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new TransactionScopeBuilder();
                    }
                }

                return instance;
            }
        }

        public TransactionScope GetTransactionScope()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = new TimeSpan(0, 10, 0) //assume 10 min is the timeout time
            };
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}
