namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Configuration;
    using Interfaces;

    public static class InitGroup
    {
        public static IMaster Master { get; private set; }

        public static List<ISlave> Slaves { get; private set; }

        public static void InitializeGroup()
        {
            var elements = RegisterGroup.GetConfig().GroupItems;
            Slaves = new List<ISlave>();

            for (int i = 0; i < elements.Count; i++)
            {
                AppDomain appDom = AppDomain.CreateDomain(elements[i].GroupType + " ( " + i + " );");

                var type = typeof(DomainLoader);
                var loader = (DomainLoader)appDom.CreateInstanceAndUnwrap(Assembly.GetAssembly(type).FullName, type.FullName);
                var service = loader.LoadService(elements[i].GroupType, Master);

                if (elements[i].GroupType == "slave")
                {
                    Slaves.Add(service as ISlave);
                }
                else
                {
                    Master = service as IMaster;
                }
            }
        }
    }
}