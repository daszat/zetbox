using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    public partial class ModelSsdlEntityTypeColumns
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, IEnumerable<Property> properties, string prefix)
        {
            host.CallTemplate("Implementation.EfModel.ModelSsdlEntityTypeColumns", ctx, properties, prefix);
        }

        protected virtual void ApplyEntityTypeColumnDefs(IEnumerable<Property> properties, string prefix)
        {
            Call(Host, ctx, properties, prefix);
        }

        protected string GetDBType(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                return "int";
            }
            else if (p is EnumerationProperty)
            {
                return "int";
            }
            else
            {
                string clrType = p.GetPropertyTypeString();
                // Try to get the CLRType
                Type t = Type.GetType(clrType, false, false);

                if (t == null) throw new ArgumentException("DatabaseType for Property not found", "p");

                // Resolve...
                if (t == typeof(int))
                    return "int";
                if (t == typeof(string))
                    return "nvarchar";
                if (t == typeof(double))
                    return "float";
                if (t == typeof(bool))
                    return "bit";
                if (t == typeof(DateTime))
                    return "datetime";
                if (t == typeof(Guid))
                    return "uniqueidentifier";

                throw new ArgumentException("DatabaseType for Property not found", "p");
            }
        }
    }
}
