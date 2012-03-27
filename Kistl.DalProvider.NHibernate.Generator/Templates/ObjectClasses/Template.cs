
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class Template
        : Templates.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, Kistl.App.Base.ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            Constructors.Call(Host, ctx,
                Constructors.CompoundInitialisationDescriptor.CreateDescriptors(ObjectClass.Properties.OfType<CompoundObjectProperty>().Where(cop => !cop.IsList).OrderBy(cop => cop.Name), ImplementationSuffix),
                ObjectClass.Properties.Where(p => p.DefaultValue != null).Select(p => "_is" + p.Name + "Set"),
                ObjectClass.Name,
                GetTypeName(),
                ObjectClass.BaseObjectClass == null ? null : ObjectClass.BaseObjectClass.Name,
                false);
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            GetDeletedRelatives.Call(Host, ctx, this.ObjectClass);

            var parent = this.ObjectClass.BaseObjectClass;
            if (parent != null)
            {
                ProxyClassChild.Call(
                    Host, ctx,
                    this.GetInterfaces().First(),
                    Mappings.ObjectClassHbm.GetProxyTypeReference(parent, this.Settings),
                    GetPersistentInitialisations(),
                    GetPersistentProperties());
            }
            else
            {
                ProxyClass.Call(
                    Host, ctx,
                    this.GetInterfaces().First(),
                    GetPersistentInitialisations(),
                    GetPersistentProperties());

            }

            if (NeedsRightsTable())
            {
                this.WriteLine();
                this.WriteLine("        private Kistl.API.AccessRights? __currentAccessRights;");
                this.WriteLine("        public override Kistl.API.AccessRights CurrentAccessRights");
                this.WriteLine("        {");
                this.WriteLine("           get { ");
                this.WriteLine("             if(Context == null) return Kistl.API.AccessRights.Full;");
                this.WriteLine("             if(__currentAccessRights == null) { ");
                this.WriteLine("                 __currentAccessRights = base.CurrentAccessRights; ");
                this.WriteLine("                 var secRight = this.Proxy.SecurityRightsCollectionImpl != null ? this.Proxy.SecurityRightsCollectionImpl.SingleOrDefault(i => i.Identity == Context.Internals().IdentityID) : null;");
                this.WriteLine("                 __currentAccessRights |= secRight != null ? (Kistl.API.AccessRights)secRight.Right : Kistl.API.AccessRights.None; ");
                this.WriteLine("             } ");
                this.WriteLine("             return __currentAccessRights.Value; }");
                this.WriteLine("        }");
                this.WriteLine();
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetPersistentInitialisations()
        {
            var orps = this.ObjectClass
                .Properties
                .OfType<ObjectReferenceProperty>()
                .Where(orp => orp.IsList())
                .Select(orp =>
                {
                    var type = orp.GetElementTypeString();
                    Relation rel = RelationExtensions.Lookup(ctx, orp);
                    if (rel.Storage == StorageType.Separate)
                    {
                        type = rel.GetRelationFullName() + ImplementationSuffix + "." + rel.GetRelationClassName() + "Proxy";
                    }
                    else
                    {
                        type += ImplementationSuffix + "." + orp.GetReferencedObjectClass().Name + "Proxy";
                    }
                    return new KeyValuePair<string, string>(orp.Name, String.Format("new Collection<{0}>()", type));
                });

            var cops = this.ObjectClass
                .Properties
                .OfType<CompoundObjectProperty>()
                .Where(cop => cop.IsList)
                .Select(cop => new KeyValuePair<string, string>(cop.Name, String.Format("new Collection<{0}.{1}{2}.{1}Proxy>()", cop.Module.Namespace, cop.GetCollectionEntryClassName(), ImplementationSuffix)));

            var vtps = this.ObjectClass
                .Properties
                .OfType<ValueTypeProperty>()
                .Where(vtp => vtp.IsList)
                .Select(vtp => new KeyValuePair<string, string>(vtp.Name, String.Format("new Collection<{0}.{1}{2}.{1}Proxy>()", vtp.Module.Namespace, vtp.GetCollectionEntryClassName(), ImplementationSuffix)));

            return orps.Concat(cops).Concat(vtps).OrderBy(pair => pair.Key);
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
            //var relationProperties = ctx.GetQuery<Relation>()
            //    .Where(rel => rel.A.Multiplicity == Multiplicity.One && rel.B.Multiplicity == Multiplicity.One
            //        && ((rel.A.Type == this.ObjectClass && rel.B.Navigator == null && rel.Storage == StorageType.MergeIntoB)
            //            || (rel.B.Type == this.ObjectClass && rel.A.Navigator == null && rel.Storage == StorageType.MergeIntoA)))
            //    .ToList()
            //    .Select(rel => rel.Storage == StorageType.MergeIntoA
            //        ? new KeyValuePair<string, string>(rel.B.Type.Module.Namespace + "." + rel.B.Type, rel.A.RoleName + ImplementationPropertySuffix)
            //        : new KeyValuePair<string, string>(rel.A.Type.Module.Namespace + "." + rel.A.Type, rel.B.RoleName + ImplementationPropertySuffix));

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

            var securityProperties = NeedsRightsTable()
                ? new[] { new KeyValuePair<string, string>("ICollection<" + Construct.SecurityRulesClassName(this.ObjectClass as ObjectClass) + ImplementationSuffix + ">", "SecurityRightsCollectionImpl") }
                : new KeyValuePair<string, string>[0];

            return this.ObjectClass
                .Properties
                .Select(prop =>
                {
                    var type = prop.GetElementTypeString();
                    var orp = prop as ObjectReferenceProperty;

                    // object references have to be translated to internal proxy interfaces
                    if (orp != null)
                    {
                        Relation rel = RelationExtensions.Lookup(ctx, orp);
                        if (rel.Storage == StorageType.Separate)
                        {
                            type = rel.GetRelationFullName() + ImplementationSuffix + "." + rel.GetRelationClassName() + "Proxy";
                        }
                        else
                        {
                            type += ImplementationSuffix + "." + orp.GetReferencedObjectClass().Name + "Proxy";
                        }
                        if (orp.IsList())
                        {
                            // always hold as collection, the wrapper has to translate/order the elements
                            type = "ICollection<" + type + ">";
                        }
                    }
                    else
                    {
                        var ceClassName = prop.GetCollectionEntryClassName();
                        var ceCollectionType = String.Format("ICollection<{0}.{1}{2}.{1}Proxy>",
                            prop.GetCollectionEntryNamespace(),
                            ceClassName,
                            ImplementationSuffix);
                        var cop = prop as CompoundObjectProperty;
                        if (cop != null)
                        {
                            if (cop.IsList)
                                type = ceCollectionType;
                            else
                                type += ImplementationSuffix;
                        }
                        else
                        {
                            var vtp = prop as ValueTypeProperty;
                            if (vtp != null && vtp.IsList)
                            {
                                type = ceCollectionType;
                            }
                        }
                    }
                    return new KeyValuePair<string, string>(type, prop.Name);
                })
                .Concat(relationPosProperties)
                //.Concat(relationProperties)
                .Concat(valuePosProperties)
                .Concat(compoundPosProperties)
                .Concat(enumPosProperties)
                .OrderBy(kv => kv.Value)
                // always add at the end
                .Concat(securityProperties)
                .Where(kv => kv.Key != null);
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }

        protected override void ApplyNamespaceTailTemplate()
        {
            base.ApplyNamespaceTailTemplate();

            if (NeedsRightsTable())
            {
                RightsClass.Call(Host, ctx, Construct.SecurityRulesClassName(this.DataType as ObjectClass));
            }
        }

        #region Property Templates

        protected override void ApplyNotifyingValueProperty(
            Property prop,
            Templates.Serialization.SerializationMembersList serList)
        {
            Properties.ProxyProperty.Call(Host, ctx,
                serList, prop.Module.Namespace, prop.GetElementTypeString(), prop.Name, false, true,
                prop.DefaultValue != null, prop.ObjectClass.GetDataTypeString(), prop.GetClassName(), prop.IsNullable(), "_is" + prop.Name + "Set", prop.ExportGuid, prop.GetElementTypeString(), "Proxy." + prop.Name);
        }

        protected override void ApplyCollectionEntryListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            //var otherEnd = rel.GetOtherEnd(relEnd);

            Properties.CollectionEntryListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 rel, relEnd.GetRole());
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            // use local template
            this.WriteLine("        // CompoundObject list property");
            Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop);
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
            ApplyNotifyingValueProperty(prop, null);
            Templates.Serialization.EnumBinarySerialization.AddToSerializers(MembersToSerialize, Templates.Serialization.SerializerType.All,
                prop.Module.Namespace,
                prop.Name,
                "Proxy." + prop.Name,
                prop.GetElementTypeString());
        }

        protected override void ApplyListProperty(Property prop, Templates.Serialization.SerializationMembersList serList)
        {
            base.ApplyListProperty(prop, serList);
        }

        protected override void ApplyObjectListPropertyTemplate(ObjectReferenceProperty prop)
        {
            Properties.ObjectListProperty.Call(Host, ctx,
                 this.MembersToSerialize,
                 prop);
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
            // use local template
            this.WriteLine("        // value list property");
            Properties.ValueCollectionProperty.Call(Host, ctx, MembersToSerialize, prop);
        }

        protected override void ApplyValueTypePropertyTemplate(ValueTypeProperty prop)
        {
            ApplyNotifyingValueProperty(prop, this.MembersToSerialize);
        }

        #endregion
    }
}
