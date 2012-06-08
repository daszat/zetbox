
namespace Kistl.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    public interface IClientImplementationTypeChecker
        : IImplementationTypeChecker
    {
    }

    public sealed class ClientImplementationType
        : ImplementationType
    {
        public delegate ClientImplementationType ClientFactory(Type type);

        private readonly IClientImplementationTypeChecker _typeChecker;

        public ClientImplementationType(Type type, InterfaceType.Factory iftFactory, IClientImplementationTypeChecker typeChecker)
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
                var genericArguments = Type.GetGenericArguments().Select(t => new ClientImplementationType(t, IftFactory, _typeChecker).ToInterfaceType().Type).ToArray();
                return IftFactory(genericType.MakeGenericType(genericArguments));
            }
            else
            {
                var ifTypeName = Type.FullName.Replace("Client" + Helper.ImplementationSuffix, String.Empty);
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
