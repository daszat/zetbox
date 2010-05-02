
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Kistl.API.Utils;

    /// <summary>
    /// A temporary data context without permanent backing store.
    /// </summary>
    public class MemoryContext
        : BaseMemoryContext
    {
        /// <summary>
        /// Initializes a new instance of the MemoryContext class, using the specified assemblies for interfaces and implementation.
        /// </summary>
        /// <param name="typeTrans"></param>
        public MemoryContext(ITypeTransformations typeTrans)
            : base(typeTrans)
        {
        }

        /// <summary>Not supported.</summary>
        public override int SubmitChanges() { throw new NotSupportedException(); }

        /// <summary>
        /// Creates an unattached instance of the specified type. The implementation type is instantiated from the implementation assembly.
        /// </summary>
        /// <param name="ifType">The requested interface.</param>
        /// <returns>A newly created, unattached instance of the implementation for the specified interface.</returns>
        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            var implType = ToImplementationType(ifType).Type;
            return Activator.CreateInstance(implType);
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            // TODO: Maybe MemoryContext is not available so Type could not be loaded. ServerAssembly is always available
            //return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "," + Kistl.API.Helper.MemoryAssembly, true));
            return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "," + Kistl.API.Helper.ServerAssembly, true));
        }
    }
}
