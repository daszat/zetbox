using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace Kistl.Server.HttpService
{
    public class MonoModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
         
            builder.RegisterType<MonoAspNetBasicAuthIdentityResolver>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}