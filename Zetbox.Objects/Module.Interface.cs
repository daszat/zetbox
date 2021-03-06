// <autogenerated/>

namespace Zetbox.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;

    public sealed class InterfaceModule
        : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder
                .Register<InterfaceTypeChecker>(c => new InterfaceTypeChecker(c.Resolve<Func<IEnumerable<IImplementationTypeChecker>>>()))
                .As<IInterfaceTypeChecker>()
                .SingleInstance();

            builder.RegisterInstance(new Zetbox.API.Utils.TypeMapAssembly(typeof(InterfaceModule).Assembly));
        }
    }

    internal sealed class InterfaceTypeChecker 
        : BaseInterfaceTypeChecker, IInterfaceTypeChecker
    {
        internal InterfaceTypeChecker(Func<IEnumerable<IImplementationTypeChecker>> implTypeCheckersFactory)
            : base(implTypeCheckersFactory)
        {
        }

        protected override System.Reflection.Assembly GetAssembly()
        {
            return typeof(InterfaceTypeChecker).Assembly;
        }
    }
}

