
namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;

    public partial class ReloadOneReference
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, 
            string referencedInterface,
            string referencedImplementation,
            string name,
		    string implName,
		    string fkBackingName,
            string fkGuidBackingName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation,
                name, implName, fkBackingName, fkGuidBackingName);
        }
    }
}
