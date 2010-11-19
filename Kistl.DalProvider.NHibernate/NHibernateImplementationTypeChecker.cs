
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    public interface INHibernateImplementationTypeChecker
        : IImplementationTypeChecker
    {
    }

    public sealed class NHibernateImplementationType
        : ImplementationType
    {
        public delegate NHibernateImplementationType MemoryFactory(Type type);

        private readonly INHibernateImplementationTypeChecker _typeChecker;

        public NHibernateImplementationType(Type type, InterfaceType.Factory iftFactory, INHibernateImplementationTypeChecker typeChecker)
            : base(type, iftFactory, typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public override InterfaceType ToInterfaceType()
        {
            if (Type.IsGenericType)
            {
                // convert args of things like Generic Collections
                Type genericType = Type.GetGenericTypeDefinition();
                var genericArguments = Type.GetGenericArguments().Select(t => new NHibernateImplementationType(t, IftFactory, _typeChecker).ToInterfaceType().Type).ToArray();
                return IftFactory(genericType.MakeGenericType(genericArguments));
            }
            else
            {
                // TODO: #1570 using wrong suffix
                var ifTypeName = Type.FullName.Replace("NHibernate" + Helper.ImplementationSuffix, String.Empty);
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
