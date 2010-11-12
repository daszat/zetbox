
namespace Kistl.Generator.InterfaceTemplates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class SimplePropertyListTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Property p)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.SimplePropertyListTemplate", ctx, p);
        }

        protected virtual string GetPropertyTypeString()
        {
            return prop.GetCollectionTypeString();
        }

        protected virtual string GetPropertyName()
        {
            return prop.Name;
        }
    }
}
