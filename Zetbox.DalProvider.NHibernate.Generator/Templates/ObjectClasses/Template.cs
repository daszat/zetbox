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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.SchemaManagement;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public class Template
        : Templates.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, Zetbox.App.Base.ObjectClass cls)
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
                ObjectClass.BaseObjectClass == null ? null : ObjectClass.BaseObjectClass.Name);
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
                this.WriteLine("        private Zetbox.API.AccessRights? __currentAccessRights;");
                this.WriteLine("        public override Zetbox.API.AccessRights CurrentAccessRights");
                this.WriteLine("        {");
                this.WriteLine("           get { ");
                this.WriteLine("             if(Context == null) return Zetbox.API.AccessRights.Full;");
                this.WriteLine("             if(__currentAccessRights == null) { ");
                this.WriteLine("                 __currentAccessRights = base.CurrentAccessRights; ");
                this.WriteLine("                 var secRight = this.Proxy.SecurityRightsCollectionImpl != null ? this.Proxy.SecurityRightsCollectionImpl.SingleOrDefault(i => i.Identity == Context.Internals().IdentityID) : null;");
                this.WriteLine("                 __currentAccessRights |= secRight != null ? (Zetbox.API.AccessRights)secRight.Right : Zetbox.API.AccessRights.None; ");
                this.WriteLine("             } ");
                this.WriteLine("             return __currentAccessRights.Value; }");
                this.WriteLine("        }");
                this.WriteLine();
                this.WriteLine("        protected override void ResetCurrentAccessRights()");
                this.WriteLine("        {");
                this.WriteLine("                base.ResetCurrentAccessRights();");
                this.WriteLine("                __currentAccessRights = null;");
                this.WriteLine("        }");
                this.WriteLine();
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetPersistentInitialisations()
        {
            var orps = this.ObjectClass
                .Properties
                .OfType<ObjectReferenceProperty>()
                .Where(orp => orp.IsList().Result)
                .Select(orp =>
                {
                    var type = orp.GetElementTypeString().Result;
                    Relation rel = RelationExtensions.Lookup(ctx, orp);
                    if (rel.Storage == StorageType.Separate)
                    {
                        type = rel.GetRelationFullName() + ImplementationSuffix + "." + rel.GetRelationClassName() + "Proxy";
                    }
                    else
                    {
                        type += ImplementationSuffix + "." + orp.GetReferencedObjectClass().Result.Name + "Proxy";
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
                .Where(relEnd => relEnd.Parent.NeedsPositionStorage(relEnd.GetRole()).Result)
                .Select(relEnd => new KeyValuePair<string, string>("int?", relEnd.RoleName + Zetbox.API.Helper.PositionSuffix));

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
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Zetbox.API.Helper.PositionSuffix));

            var compoundPosProperties = this.ObjectClass
                .Properties
                .OfType<CompoundObjectProperty>()
                .Where(p => p.IsList)
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Zetbox.API.Helper.PositionSuffix));

            var enumPosProperties = this.ObjectClass
                .Properties
                .OfType<EnumerationProperty>()
                .Where(p => p.IsList)
                .Select(p => new KeyValuePair<string, string>("int?", p.Name + Zetbox.API.Helper.PositionSuffix));

            var securityProperties = NeedsRightsTable()
                ? new[] { new KeyValuePair<string, string>("ICollection<" + Construct.SecurityRulesClassName(this.ObjectClass as ObjectClass) + ImplementationSuffix + ">", "SecurityRightsCollectionImpl") }
                : new KeyValuePair<string, string>[0];

            return this.ObjectClass
                .Properties
                .Select(prop =>
                {
                    var type = prop.GetElementTypeString().Result;
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
                            type += ImplementationSuffix + "." + orp.GetReferencedObjectClass().Result.Name + "Proxy";
                        }
                        if (orp.IsList().Result)
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
                serList, prop.Module.Namespace, prop.GetElementTypeString().Result, prop.Name, false, true,
                prop.DefaultValue != null && !prop.IsCalculated(), // No default value for calculated properties, default values are used then for database migration
                prop.ObjectClass.GetDataTypeString().Result,
                prop.GetClassName(),
                prop.IsNullable().Result,
                "_is" + prop.Name + "Set",
                prop.ExportGuid,
                prop.GetElementTypeString().Result,
                "Proxy." + prop.Name,
                prop.IsCalculated(),
                prop.DisableExport == true);
        }

        protected override void ApplyCollectionEntryListTemplate(ObjectReferenceProperty prop)
        {
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop).Result;
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
            if (prop.DefaultValue != null)
            {
                Templates.Serialization.EnumWithDefaultBinarySerialization.AddToSerializers(MembersToSerialize,
                    prop.DisableExport == true ? Templates.Serialization.SerializerType.Binary : Templates.Serialization.SerializerType.All,
                    prop.Module.Namespace,
                    prop.Name,
                    "Proxy." + prop.Name,
                    prop.GetElementTypeString().Result,
                    "_is" + prop.Name + "Set");
            }
            else
            {
                Templates.Serialization.EnumBinarySerialization.AddToSerializers(MembersToSerialize,
                    prop.DisableExport == true ? Templates.Serialization.SerializerType.Binary : Templates.Serialization.SerializerType.All,
                    prop.Module.Namespace,
                    prop.Name,
                    "Proxy." + prop.Name,
                    prop.GetElementTypeString().Result);
            }
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
