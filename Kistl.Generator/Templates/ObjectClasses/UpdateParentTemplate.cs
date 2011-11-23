
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

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
                    //RelationEnd relEnd = rel.GetEnd(p);

                    return (rel.Storage == StorageType.MergeIntoA && rel.A.Navigator == p)
                        || (rel.Storage == StorageType.MergeIntoB && rel.B.Navigator == p);
                }).ToList();

            host.CallTemplate("ObjectClasses.UpdateParentTemplate",
                ctx, props);
        }

        protected virtual string GetPropertyBackingStore(ObjectReferenceProperty prop)
        {
            return "_fk_" + prop.Name;
        }

        protected virtual string GetParentObjExpression(ObjectReferenceProperty prop)
        {
            return "parentObj == null ? (int?)null : parentObj.ID";
        }

        private void ApplyCase(ObjectReferenceProperty prop)
        {
            string name = prop.Name;
            string propertyBackingStore = GetPropertyBackingStore(prop);
            string parentObjExpression = GetParentObjExpression(prop);

            this.WriteObjects("                case \"", name, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = ", propertyBackingStore, ";");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = ", parentObjExpression, ";");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", name, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        ", propertyBackingStore, " = __newValue;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanged(\"", name, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    }");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
