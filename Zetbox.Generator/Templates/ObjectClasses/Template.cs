// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public class Template
        : TypeBase
    {
        protected ObjectClass ObjectClass { get; private set; }

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, ObjectClass t)
            : base(_host, ctx, t)
        {
            this.ObjectClass = t;
        }

        protected override string GetClassModifiers()
        {
            return ObjectClass.IsAbstract ? " abstract" : string.Empty;
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(this.ObjectClass.BaseObjectClass.Module.Namespace + "." + this.ObjectClass.BaseObjectClass.Name);
            }
            else
            {
                return ImplementationNamespace + ".DataObject" + ImplementationSuffix;
            }
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            Properties.ObjectReferencePropertyTemplate.Call(
                Host, ctx, this.MembersToSerialize,
                prop, true, true);
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject property");
            Properties.CompoundObjectPropertyTemplate.Call(Host, ctx, MembersToSerialize, prop);
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop,
                "ClientValueCollectionWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);

            var relEnd = rel.GetEnd(prop);
            //var otherEnd = rel.GetOtherEnd(relEnd);

            // without navigator, there should be no property
            if (relEnd.Navigator == null)
                return;

            switch ((StorageType)rel.Storage)
            {
                case StorageType.MergeIntoA:
                case StorageType.MergeIntoB:
                case StorageType.Replicate:
                    // simple and direct reference
                    this.WriteLine("        // object list property");
                    ApplyObjectListPropertyTemplate(prop);
                    break;
                case StorageType.Separate:
                    this.WriteLine("        // collection entry list property");
                    ApplyCollectionEntryListTemplate(prop);
                    break;
                default:
                    throw new NotImplementedException("unknown StorageHint for ObjectReferenceProperty[IsList == true]");
            }
        }

        protected virtual void ApplyObjectListPropertyTemplate(ObjectReferenceProperty prop)
        {
            Properties.ObjectListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 prop);
        }

        protected virtual void ApplyCollectionEntryListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);

            Properties.CollectionEntryListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 rel, relEnd.GetRole());
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            Properties.ValueCollectionProperty.Call(Host, ctx,
                MembersToSerialize,
                prop,
                "ClientValueCollectionWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyConstructorTemplate()
        {
            base.ApplyConstructorTemplate();

            ObjectClasses.Constructors.Call(
                Host, ctx,
                GetTypeName(),
                this.DataType
                    .Properties
                    .OfType<CompoundObjectProperty>());
        }

        protected override void ApplyApplyChangesFromMethod()
        {
            base.ApplyApplyChangesFromMethod();
            ApplyChangesFromMethod.Call(Host, ctx, DataType, GetTypeName());
        }

        protected override void ApplySetNewMethod()
        {
            base.ApplySetNewMethod();

            this.WriteLine("        public override void SetNew()");
            this.WriteLine("        {");
            this.WriteLine("            base.SetNew();");
            foreach (var prop in ObjectClass.Properties.Where(p => p.IsCalculated()).OrderBy(p => p.Name))
            {
                this.WriteObjects("            _", prop.Name, "_IsDirty = true;\r\n");
            } 
            this.WriteLine("        }");
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();

            // Implement ObjectClassID by using a static backing store
            this.WriteLine("        private static readonly Guid _objectClassID = new Guid(\"{0}\");", DataType.ExportGuid);
            this.WriteLine("        public override Guid ObjectClassID { get { return _objectClassID; } }");
            this.WriteLine();
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            ApplyUpdateParentTemplate();
            ApplyOnPropertyChangeTemplate();
            ReloadReferences.Call(Host, ctx, this.DataType);
            CustomTypeDescriptor.Call(Host, ctx, this.ObjectClass, DataType.Name, GetTypeName());
            DefaultMethods.Call(Host, ctx, this.DataType);
        }

        protected virtual void ApplyUpdateParentTemplate()
        {
            UpdateParentTemplate.Call(Host, ctx, this.DataType);
        }

        protected virtual void ApplyOnPropertyChangeTemplate()
        {
            OnPropertyChange.Call(Host, ctx, this.DataType);
        }

        protected override IEnumerable<App.Base.Method> MethodsToGenerate()
        {
            return SelectAndParents(this.ObjectClass).SelectMany(cls => cls.Methods).Where(m => !m.IsDefaultMethod());
        }

        protected static IEnumerable<ObjectClass> SelectAndParents(ObjectClass cls)
        {
            yield return cls;
            while (cls.BaseObjectClass != null)
            {
                cls = cls.BaseObjectClass;
                yield return cls;
            }
        }

        public static bool NeedsRightsTable(DataType dt)
        {
            var cls = dt as ObjectClass;
            if (cls != null)
            {
                if (cls.NeedsRightsTable())
                {
                    if (cls.BaseObjectClass != null)
                    {
                        // TODO: Currently only Basesclasses are supported
                        throw new NotSupportedException("Security Rules for derived classes are not supported yet");
                    }
                    return true;
                }
            }
            return false;
        }

        protected bool NeedsRightsTable()
        {
            return NeedsRightsTable(this.DataType);
        }
    }
}
