using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class UpdateParentTemplate
    {

        public static void Call(IGenerationHost host, IKistlContext ctx,
            DataType dataType)
        {
            host.CallTemplate("Implementation.ObjectClasses.UpdateParentTemplate",
                ctx, dataType);
        }

        public void ApplyCase(ObjectReferenceProperty prop)
        {
            Relation rel = RelationExtensions.Lookup(ctx, prop);
            RelationEnd relEnd = rel.GetEnd(prop);

            if ((rel.Storage == StorageType.MergeIntoA && rel.A.Navigator == prop)
                || (rel.Storage == StorageType.MergeIntoB && rel.B.Navigator == prop))
            {
                string name = prop.PropertyName;
                string fkName = "fk_" + name;

                this.WriteObjects("                case \"", name, "\":");
                this.WriteLine();
                this.WriteObjects("                    ", fkName, " = id;");
                this.WriteLine();
                this.WriteObjects("                    break;");
                this.WriteLine();
            }
        }
    }
}
