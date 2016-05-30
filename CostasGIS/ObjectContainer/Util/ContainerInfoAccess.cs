using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Web;

namespace ObjectContainer.Util
{
    internal class ContainerInfoAccess
    {
        private static volatile ContainerInfoAccess instance = null;
        private static readonly object padlock = new object();
        private XElement root;
        
        public enum LifetimeType 
        {
            [StringValue("singleton")]
            singleton,
            [StringValue("singletonPerRequest")]
            singletonPerRequest 
        }

        private ContainerInfoAccess()
        {
            this.root = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + "/Configuration/ObjectContainerConfig.xml");
        }

        public static ContainerInfoAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new ContainerInfoAccess();
                    }
                }

                return instance;
            }
        }

        public string GetInstanceAssemblyQualifiedName(string classTypeName)
        {
            IEnumerable<string> nodeValue = from c in root.Elements("Class")
                                            where c.Element("ClassType").Value == classTypeName
                                            select c.Element("ClassInstance").Value;

            return nodeValue.FirstOrDefault();
        }


        public bool CheckObjectLifetime(string classTypeName, LifetimeType lifetimeType)
        {
            IEnumerable<string> nodeValue = from c in root.Elements("Class")
                               where c.Element("ClassType").Value == classTypeName
                               select c.Attribute("lifetime").Value;

            if (nodeValue.FirstOrDefault() == StringEnum.GetStringValue(lifetimeType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetObjectTypeName(Type tipoInterfaz)
        {
            string nombreInterfaz;

            if (tipoInterfaz.Name.Contains('`'))
            {
                char[] delimiterChars = { '[', ']', ' ' };
                string[] result;
                int count;
                int k = 2;
                int i = 1;
                List<string> lista = new List<string>();

                result = tipoInterfaz.FullName.Split(delimiterChars);
                count = Convert.ToInt32(result[0].Split('`').LastOrDefault());
                nombreInterfaz = tipoInterfaz.Namespace + "." + tipoInterfaz.Name.Split('`')[0] + "[";

                for (i = 0; i < count; i++)
                {
                    if (i + 1 == count)
                    {
                        nombreInterfaz = nombreInterfaz + result[k].Split('.').LastOrDefault().Split(',')[0];

                    }
                    else
                    {
                        nombreInterfaz = nombreInterfaz + result[k].Split('.').LastOrDefault();
                    }
                    k = k + 6;
                }

                nombreInterfaz = nombreInterfaz + "]";
            }
            else
            {
                nombreInterfaz = tipoInterfaz.FullName;
            }

            return nombreInterfaz;
        }
    }
}
