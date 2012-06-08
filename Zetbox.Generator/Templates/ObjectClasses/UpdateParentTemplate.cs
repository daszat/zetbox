
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

    public class UpdateParentTemplateParams
    {
        public string IfType { get; set; }
        public string PropertyName { get; set; }
    }

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
                })
                .Select(p => new UpdateParentTemplateParams()
                {
                    PropertyName = p.Name,
                    IfType = string.Format("{0}.{1}", p.GetReferencedObjectClass().Module.Namespace, p.GetReferencedObjectClass().Name)
                })
                .ToList();

            host.CallTemplate("ObjectClasses.UpdateParentTemplate",
                props);
        }

        protected virtual string GetPropertyBackingStore(string propName)
        {
            return "_fk_" + propName;
        }

        protected virtual string GetParentObjExpression(string ifType)
        {
            return "parentObj == null ? (int?)null : parentObj.ID";
        }

        protected virtual void ApplyCase(UpdateParentTemplateParams prop)
        {
            string propertyBackingStore = GetPropertyBackingStore(prop.PropertyName);
            string parentObjExpression = GetParentObjExpression(prop.IfType);

            this.WriteObjects("                case \"", prop.PropertyName, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = ", propertyBackingStore, ";");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = ", parentObjExpression, ";");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        ", propertyBackingStore, " = __newValue;");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanged(\"", prop.PropertyName, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                    }");
            this.WriteLine();
            this.WriteObjects("                    break;");
            this.WriteLine();
        }
    }
}
