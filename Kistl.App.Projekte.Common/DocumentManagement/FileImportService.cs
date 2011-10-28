namespace Kistl.App.Projekte.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Autofac;

    [ServiceDescriptor]
    public class FileImportService : Kistl.API.IService
    {
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                // Register explicit overrides here
                moduleBuilder
                    .RegisterType<FileImportService>()
                    .SingleInstance();
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
