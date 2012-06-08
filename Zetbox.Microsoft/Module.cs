
namespace Kistl.Microsoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API.Client;

    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<ScreenshotTool>(c => new ScreenshotTool())
                .As<IScreenshotTool>()
                .SingleInstance();
        }
    }
}
