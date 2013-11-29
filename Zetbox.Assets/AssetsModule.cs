using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Zetbox.API;

namespace Zetbox.Assets
{
    public class AssetsModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            base.Load(moduleBuilder);

            moduleBuilder.RegisterZetboxImplementors(typeof(AssetsModule).Assembly);
        }
    }
}
