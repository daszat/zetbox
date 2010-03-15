using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Arebis.CodeGeneration;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class SimplePropertyListTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Property p)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Interface.DataTypes.SimplePropertyListTemplate", ctx, p);
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
