
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class Template
        : Templates.ObjectClasses.Template
    {
        private readonly ObjectClass cls;

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            this.cls = cls;
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();

            if (cls.BaseObjectClass == null)
            {
                Properties.IdProperty.Call(Host, ctx);
            }
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            Constructors.Call(Host, ctx,
                cls.Properties.OfType<CompoundObjectProperty>(),
                cls.Name,
                GetTypeName(),
                cls.BaseObjectClass == null ? null : cls.BaseObjectClass.Name);

            if (cls.BaseObjectClass == null)
            {
                SaveOrUpdateToMethod.Call(Host, ctx);
            }
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            var parent = this.ObjectClass.BaseObjectClass;
            if (parent != null)
            {
                ProxyClassChild.Call(
                    Host, ctx,
                    this.GetInterfaces().First(),
                    Mappings.ObjectClassHbm.GetInterfaceTypeReference(parent, this.Settings),
                    Mappings.ObjectClassHbm.GetImplementationTypeReference(parent, this.Settings),
                    GetPersistentProperties());
            }
            else
            {
                ProxyClass.Call(
                    Host, ctx,
                    this.GetInterfaces().First(),
                    GetPersistentProperties());
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetPersistentProperties()
        {
            var relationPosProperties = ctx.GetQuery<RelationEnd>()
                .Where(relEnd => relEnd.Type == this.ObjectClass)
                .ToList()
                .Where(relEnd => relEnd.Parent.NeedsPositionStorage(relEnd.GetRole()))
                .Select(relEnd => new KeyValuePair<string, string>("int?", relEnd.RoleName + Kistl.API.Helper.PositionSuffix));

            // Look for relations where we have no navigator where the storage should be
            // there we need to create navigators to map them with NHibernate
            var relationProperties = ctx.GetQuery<Relation>()
                .Where(rel => rel.A.Multiplicity == Multiplicity.One && rel.B.Multiplicity == Multiplicity.One
                    && ((rel.A.Type == this.ObjectClass && rel.B.Navigator == null && rel.Storage == StorageType.MergeIntoB)
                        || (rel.B.Type == this.ObjectClass && rel.A.Navigator == null && rel.Storage == StorageType.MergeIntoA)))
                .ToList()
                .Select(rel => rel.Storage == StorageType.MergeIntoA
                    ? new KeyValuePair<string, string>(rel.B.Type.Module.Namespace + "." + rel.B.Type, rel.A.RoleName + ImplementationPropertySuffix)
                    : new KeyValuePair<string, string>(rel.A.Type.Module.Namespace + "." + rel.A.Type, rel.B.RoleName + ImplementationPropertySuffix));

            var valuePosProperties = this.ObjectClass
                .Properties
                .OfType<ValueTypeProperty>()
                .Where(p => p.IsList)
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Kistl.API.Helper.PositionSuffix));

            var compoundPosProperties = this.ObjectClass
                .Properties
                .OfType<CompoundObjectProperty>()
                .Where(p => p.IsList)
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Kistl.API.Helper.PositionSuffix));

            var enumPosProperties = this.ObjectClass
                .Properties
                .OfType<EnumerationProperty>()
                .Where(p => p.IsList)
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Kistl.API.Helper.PositionSuffix));

            return this.ObjectClass
                .Properties
                .Select(p =>
                {
                    var type = p.ReferencedTypeAsCSharp();
                    var orp = p as ObjectReferenceProperty;

                    // object references have to be translated to internal proxy interfaces
                    if (orp != null)
                    {
                        Relation rel = RelationExtensions.Lookup(ctx, orp);
                        if (rel.Storage == StorageType.Separate)
                        {
                            type = rel.GetRelationFullName() + ImplementationSuffix;
                        }
                        else
                        {
                            type += ImplementationSuffix + "." + orp.GetReferencedObjectClass().Name + "Interface";
                        }
                        if (orp.IsList())
                        {
                            // always hold as collection, the wrapper has to translate/order the elements
                            type = "ICollection<" + type + ">";
                        }
                    }
                    return new KeyValuePair<string, string>(type, p.Name);
                })
                .Concat(relationPosProperties)
                .Concat(relationProperties)
                .Concat(valuePosProperties)
                .Concat(compoundPosProperties)
                .Concat(enumPosProperties)
                .OrderBy(kv => kv.Value);
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }

        #region Property Templates

        protected override void ApplyNotifyingValueProperty(Property prop, Templates.Serialization.SerializationMembersList serList)
        {
            Properties.ProxyProperty.Call(Host, ctx, prop.ReferencedTypeAsCSharp(), prop.Name, false, true);
            // TODO: Serialization
        }

        protected override void ApplyCollectionEntryListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);

            Templates.Properties.CollectionEntryListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 rel, relEnd.GetRole(),
                 String.Format("NHibernate{0}Side{1}Wrapper",
                    otherEnd.GetRole(),
                    relEnd.HasPersistentOrder ? "List" : "Collection"));
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            base.ApplyCompoundObjectListTemplate(prop);
        }

        protected override void ApplyCompoundObjectPropertyTemplate(CompoundObjectProperty prop)
        {
            base.ApplyCompoundObjectPropertyTemplate(prop);
        }

        protected override void ApplyEnumerationListTemplate(EnumerationProperty prop)
        {
            base.ApplyEnumerationListTemplate(prop);
        }

        protected override void ApplyEnumerationPropertyTemplate(EnumerationProperty prop)
        {
            base.ApplyEnumerationPropertyTemplate(prop);
        }

        protected override void ApplyListProperty(Property prop, Templates.Serialization.SerializationMembersList serList)
        {
            base.ApplyListProperty(prop, serList);
        }

        protected override void ApplyObjectListPropertyTemplate(ObjectReferenceProperty prop)
        {
            base.ApplyObjectListPropertyTemplate(prop);
        }

        protected override void ApplyObjectReferenceListTemplate(ObjectReferenceProperty prop)
        {
            base.ApplyObjectReferenceListTemplate(prop);
        }

        protected override void ApplyObjectReferencePropertyTemplate(ObjectReferenceProperty prop)
        {
            base.ApplyObjectReferencePropertyTemplate(prop);
        }

        protected override void ApplyOtherListTemplate(Property prop)
        {
            base.ApplyOtherListTemplate(prop);
        }

        protected override void ApplyOtherPropertyTemplate(Property prop)
        {
            base.ApplyOtherPropertyTemplate(prop);
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            base.ApplyValueTypeListTemplate(prop);
        }

        protected override void ApplyValueTypePropertyTemplate(ValueTypeProperty prop)
        {
            base.ApplyValueTypePropertyTemplate(prop);
        }

        #endregion
    }
}
