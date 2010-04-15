
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
        /// <param name="ifFilter">An interface type filter, passed in from the container</param>
        /// <param name="interfaces">The assembly containing the interfaces available in this context. MUST not be null.</param>
        /// <param name="implementations">The assembly containing the classes implementing the interfaces in this context. MUST not be null.</param>
        public MemoryContext(IInterfaceTypeFilter ifFilter, Assembly interfaces, Assembly implementations)
            : base(ifFilter, interfaces, implementations)
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
            var implType = ImplementationAssembly.GetType(ifType.Type.FullName + Kistl.API.Helper.ImplementationSuffix);
            return Activator.CreateInstance(implType);
        }
    }
}
