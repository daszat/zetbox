using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class ReloadOneReference
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, 
            string referencedInterface,
            string referencedImplementation,
            string name,
		    string efName,
		    string fkBackingName,
            string fkGuidBackingName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ReloadOneReference", ctx, referencedInterface, referencedImplementation, 
                name, efName, fkBackingName, fkGuidBackingName);
        }
    }
}
