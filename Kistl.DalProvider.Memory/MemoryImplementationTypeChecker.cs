
namespace Kistl.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;

    public interface IMemoryImplementationTypeChecker
        : IImplementationTypeChecker
    {
    }

    public sealed class MemoryImplementationType
        : ImplementationType
    {
        public delegate MemoryImplementationType MemoryFactory(Type type);

        private readonly IMemoryImplementationTypeChecker _typeChecker;

        public MemoryImplementationType(Type type, InterfaceType.Factory iftFactory, IMemoryImplementationTypeChecker typeChecker)
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
                var genericArguments = Type.GetGenericArguments().Select(t => new MemoryImplementationType(t, IftFactory, _typeChecker).ToInterfaceType().Type).ToArray();
                return IftFactory(genericType.MakeGenericType(genericArguments));
            }
            else
            {
                // TODO: #1570 using wrong suffix
                var ifTypeName = Type.FullName.Replace("Memory" + Helper.ImplementationSuffix, String.Empty);
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
