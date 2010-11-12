
namespace Kistl.DalProvider.Ef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    public interface IEfImplementationTypeChecker
        : IImplementationTypeChecker
    {
    }

    // TODO: move to generated assembly, optimize there
    public sealed class EfImplementationType
        : ImplementationType
    {
        public delegate EfImplementationType EfFactory(Type type);

        private readonly IEfImplementationTypeChecker _typeChecker;

        public EfImplementationType(Type type, InterfaceType.Factory iftFactory, IEfImplementationTypeChecker typeChecker)
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
                var genericArguments = Type.GetGenericArguments().Select(t => new EfImplementationType(t, IftFactory, _typeChecker).ToInterfaceType().Type).ToArray();
                return IftFactory(genericType.MakeGenericType(genericArguments));
            }
            else
            {
                var parts = Type.FullName.Split(new string[] { "Ef" + Helper.ImplementationSuffix }, StringSplitOptions.RemoveEmptyEntries);
                return IftFactory(Type.GetType(parts[0] + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
