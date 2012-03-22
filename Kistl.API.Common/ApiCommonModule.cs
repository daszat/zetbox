
namespace Kistl.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;

    public class ApiCommonModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<CachingMetaDataResolver>()
                .As<CachingMetaDataResolver>()
                .As<IMetaDataResolver>()
                .SingleInstance();
        }
    }
}
