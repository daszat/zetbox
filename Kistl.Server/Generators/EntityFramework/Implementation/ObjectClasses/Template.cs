using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;
using System.Diagnostics;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public class Template
        : Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            WriteLine("    [EdmEntityType(NamespaceName=\"Model\", Name=\"{0}\")]", this.DataType.ClassName);
        }

        protected override void ApplyIDPropertyTemplate()
        {
            bool hasId = false;
            // only implement ID if necessary
            if (this.DataType is ObjectClass && ((ObjectClass)this.DataType).BaseObjectClass == null)
            {
                hasId = true;
            }
            else if (!(this.DataType is ObjectClass))
            {
                hasId = true;
            }

            if (hasId)
            {
                base.ApplyIDPropertyTemplate();
                Host.CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);
            }
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.API.Server",
                "Kistl.DALProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses" 
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + "__Implementation__";
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseServerDataObject_EntityFramework";
            }
        }

        protected override void ApplyPropertyTemplate(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                ApplyObjectReferencPropertyTemplate((ObjectReferenceProperty)p);
            }
            else if (p is EnumerationProperty)
            {
                ApplyEnumerationPropertyTemplate((EnumerationProperty)p);
            }
            else if (p is StructProperty)
            {
                ApplyStructPropertyTemplate((StructProperty)p);
            }
            else
            {
                base.ApplyPropertyTemplate(p);
            }
        }

        private void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            this.WriteLine("        // enumeration property");
            this.Host.CallTemplate("Implementation.ObjectClasses.EnumerationPropertyTemplate", ctx,
                prop);
        }

        private void ApplyStructPropertyTemplate(StructProperty prop)
        {
            this.WriteLine("        // struct property");
            this.Host.CallTemplate("Implementation.ObjectClasses.StructPropertyTemplate", ctx,
                prop);
        }

        private void ApplyObjectReferencPropertyTemplate(ObjectReferenceProperty prop)
        {
            var rel = NewRelation.Lookup(ctx, prop);

            Debug.Assert(rel.A.Navigator == prop || rel.B.Navigator == prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = relEnd.Other;

            this.WriteLine("    /*");
            this.CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);
            this.WriteLine("    */");

            switch (rel.GetPreferredStorage())
            {
                case StorageHint.MergeA:
                case StorageHint.MergeB:
                case StorageHint.Replicate:

                    // simple and direct reference
                    if (otherEnd.Multiplicity.UpperBound() > 1)
                    {
                        this.WriteLine("        // object list property");
                        this.Host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty", ctx,
                            relEnd);
                    }
                    else if (otherEnd.Multiplicity.UpperBound() == 1)
                    {
                        this.WriteLine("        // object reference property");
                        this.CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
                            prop.PropertyName,
                            rel.GetAssociationName(), otherEnd.RoleName,
                            otherEnd.Type.NameDataObject,
                            otherEnd.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
                            (relEnd.Multiplicity.UpperBound() > 1 && relEnd.HasPersistentOrder)
                            );
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    break;
                case StorageHint.Separate:
                    this.WriteLine("        // collection reference property");
                    this.CallTemplate("Implementation.ObjectClasses.CollectionEntryListProperty", ctx, relEnd);
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint");
            }
        }

        protected override void ApplyMethodTemplate(Method m)
        {
            base.ApplyMethodTemplate(m);
        }

        // HACK: workaround the fact this is missing on the server
        // TODO: remove this and move the client action "OnGetInheritedMethods_ObjectClass" into a common action assembly
        private static void GetMethods(ObjectClass obj, List<Method> e)
        {
            if (obj.BaseObjectClass != null)
                GetMethods(obj.BaseObjectClass, e);
            e.AddRange(obj.Methods);
        }

        protected override IEnumerable<Method> MethodsToGenerate()
        {
            var inherited = new List<Method>();
            GetMethods(this.ObjectClass, inherited);
            // TODO: fix Default methods in DB, remove the filter here and remove them from TailTemplate
            return inherited.Where(m => !m.IsDefaultMethod());
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            this.CallTemplate("Implementation.ObjectClasses.Tail", ctx, this.ObjectClass);
        }

    }
}
