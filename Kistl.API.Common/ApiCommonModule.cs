
namespace Kistl.API.Common
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ApiCommonModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<ServiceControlManager.Module>();
        }
    }
}
