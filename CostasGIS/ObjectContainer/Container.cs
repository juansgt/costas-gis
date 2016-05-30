using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectContainer.Properties;
using System.Configuration;
using System.Collections;
using System.Web;
using ObjectContainer.Util;

namespace ObjectContainer
{
    public class Container
    {
        private static volatile Container instance = null;
        private static readonly object padlock = new object();
        private static readonly object instancelock = new object();
        //private const string ObjectContextKey = "ObjectContext";
        private ContainerInfoAccess containerInfoAccess;

        public Container()
        {
            this.containerInfoAccess = ContainerInfoAccess.Instance;
        }

        public static Container Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new Container();
                    }
                }

                return instance;
            }
        }

        public Object ResolveObject<ObjectType>()
        {
            return this.Resolve<ObjectType>();
        }

        public ObjectType Resolve<ObjectType>()
        {
            try
            {
                HttpContext httpContext;
                Type tipoInterfaz = typeof(ObjectType);
                string nombreInterfaz = containerInfoAccess.GetObjectTypeName(tipoInterfaz);
                ObjectType instancia = default(ObjectType);

                if (containerInfoAccess.CheckObjectLifetime(nombreInterfaz, ContainerInfoAccess.LifetimeType.singleton))
                {
                    SingletonInstance<ObjectType> singletonInstance = SingletonInstance<ObjectType>.Instance;
                    if (singletonInstance.InstanciaObjetivo != null)
                    {
                        instancia = (ObjectType)singletonInstance.InstanciaObjetivo;
                    }
                }

                if (containerInfoAccess.CheckObjectLifetime(nombreInterfaz, ContainerInfoAccess.LifetimeType.singletonPerRequest))
                {
                    httpContext = HttpContext.Current;

                    if (httpContext != null)
                    {
                        instancia = (ObjectType)httpContext.Items[nombreInterfaz];
                    }
                    else
                    {
                        throw new ApplicationException("There is no Http Context available");
                    }
                }

                if (instancia == null)
                {
                    string claseInstanciar = containerInfoAccess.GetInstanceAssemblyQualifiedName(nombreInterfaz);
                    Type tipoClaseInstanciar = Type.GetType(claseInstanciar);

                    lock (instancelock)
                    {
                        if (instancia == null)
                        {
                            instancia = (ObjectType)Activator.CreateInstance(tipoClaseInstanciar);

                            if (containerInfoAccess.CheckObjectLifetime(nombreInterfaz, ContainerInfoAccess.LifetimeType.singleton))
                            {
                                SingletonInstance<ObjectType> singletonInstance = SingletonInstance<ObjectType>.Instance;
                                singletonInstance.InstanciaObjetivo = instancia;
                            }
                            if (containerInfoAccess.CheckObjectLifetime(nombreInterfaz, ContainerInfoAccess.LifetimeType.singletonPerRequest))
                            {
                                httpContext = HttpContext.Current;
                                httpContext.Items.Add(nombreInterfaz, instancia);
                            }
                        }
                    }
                }
                
                return instancia;
            }
            catch (NullReferenceException)
            {
                throw;
            }
        }
    }
}
