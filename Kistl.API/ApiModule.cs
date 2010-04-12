using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Kistl.API
{
    public sealed class ApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);
        }
    }
}
