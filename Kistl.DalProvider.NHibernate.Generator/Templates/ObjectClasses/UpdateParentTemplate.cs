
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Templates = Kistl.Generator.Templates;

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

        private void ApplyCase(ObjectReferenceProperty prop)
        {
            string name = prop.Name;
            var ifType = String.Format("{0}.{1}", prop.GetReferencedObjectClass().Module.Namespace, prop.GetReferencedObjectClass().Name);
            var implType = ifType + ImplementationSuffix;

            this.WriteObjects("                case \"", name, "\":");
            this.WriteLine();
            this.WriteObjects("                    {");
            this.WriteLine();
            this.WriteObjects("                        var __oldValue = (", implType, ")OurContext.AttachAndWrap(this.Proxy.", name, ");");
            this.WriteLine();
            this.WriteObjects("                        var __newValue = (", implType, ")(id == null ? null : OurContext.Find<", ifType, ">(id.Value));");
            this.WriteLine();
            this.WriteObjects("                        NotifyPropertyChanging(\"", name, "\", __oldValue, __newValue);");
            this.WriteLine();
            this.WriteObjects("                        this.Proxy.", name, " = __newValue == null ? null : __newValue.Proxy;");
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
