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
            if (host == null) { throw new ArgumentNullException("host"); }
            if (dataType == null) { throw new ArgumentNullException("dataType"); }

            var props = dataType
                .Properties
                .OfType<ObjectReferenceProperty>()
                .OrderBy(p => p.Name)
                .Where(p =>
                {
                    Relation rel = RelationExtensions.Lookup(ctx, p);
                    RelationEnd relEnd = rel.GetEnd(p);

                    return (rel.Storage == StorageType.MergeIntoA && rel.A.Navigator == p)
                        || (rel.Storage == StorageType.MergeIntoB && rel.B.Navigator == p);
                }).ToList();

            host.CallTemplate("Implementation.ObjectClasses.UpdateParentTemplate",
                ctx, props);
        }

        private void ApplyCase(ObjectReferenceProperty prop)
        {
            string name = prop.Name;
            string fkBackingName = "_fk_" + name;

            this.WriteObjects("                case \"", name, "\":");
            this.WriteLine();
            this.WriteObjects("                    __oldValue = ", fkBackingName, ";");
            this.WriteLine();
            this.WriteObjects("                    NotifyPropertyChanging(\"", name, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    ", fkBackingName, " = __newValue;");
            this.WriteLine();
            this.WriteObjects("                    NotifyPropertyChanged(\"", name, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
