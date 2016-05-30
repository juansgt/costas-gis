using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectContainer
{
    internal class SingletonInstance<ObjectType>
    {
        private static volatile SingletonInstance<ObjectType> instance = null;
        private static readonly object padlock = new object();
        public ObjectType InstanciaObjetivo { get; set; }

        public static SingletonInstance<ObjectType> Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new SingletonInstance<ObjectType>();
                    }
                }

                return instance;
            }
        }
    }
}
