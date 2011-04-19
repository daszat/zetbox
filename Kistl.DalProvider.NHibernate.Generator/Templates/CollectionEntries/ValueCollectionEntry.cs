
namespace Kistl.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class ValueCollectionEntry
        : Templates.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyAPropertyTemplate()
        {
            string moduleNamespace = prop.ObjectClass.Module.Namespace;
            string ownInterface = prop.GetCollectionEntryClassName();
            string name = "Parent";
            string eventName = "unused";
            string fkBackingName = "_fk_Parent";
            string fkGuidBackingName = "unused";
            string referencedInterface = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            string referencedImplementation = referencedInterface + ImplementationSuffix;
            string positionPropertyName = string.Empty;
            string inverseNavigatorName = prop.Name;

            Properties.ObjectReferencePropertyTemplate.Call(
                Host,
                ctx,
                MembersToSerialize,
                moduleNamespace,
                ownInterface,
                name,
                string.Empty /*implNameUnused*/,
                eventName,
                fkBackingName,
                fkGuidBackingName,
                referencedInterface,
                referencedImplementation,
                string.Empty /*associationNameUnused*/,
                string.Empty /*targetRoleNameUnused*/,
                positionPropertyName,
                inverseNavigatorName,
                true /*inverseNavigatorIsList*/,
                false /*eagerLoading*/,
                false /*relDataTypeExportable*/,
                false /*callGetterSetterEvents*/);

            Templates.Properties.DelegatingProperty.Call(
                Host, ctx,
                "ParentObject", "Kistl.API.IDataObject",
                "Parent", referencedImplementation);
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            var valueInitialisation = prop is CompoundObjectProperty
                ? new ObjectClasses.Constructors.CompoundInitialisationDescriptor[] {
                    new ObjectClasses.Constructors.CompoundInitialisationDescriptor(
                        "Value",
                        "this.Proxy.Value",
                        prop.GetPropertyTypeString(),
                        prop.GetPropertyTypeString() + ImplementationSuffix,
                        prop.IsNullable())
                }
                : new ObjectClasses.Constructors.CompoundInitialisationDescriptor[0];
            ObjectClasses.Constructors.Call(Host, ctx,
                valueInitialisation,
                new string[0],
                GetCeInterface(),
                GetCeClassName(),
                null,
                prop is CompoundObjectProperty);
        }

        protected override void ApplyIdPropertyTemplate()
        {
            // inherited from Base
            //base.ApplyIdPropertyTemplate();
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "ValueIndex", "int?", "this.Proxy.Value" + Kistl.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = GetCeInterface();
            string referencedType = prop.GetPropertyTypeString();
            if (prop is CompoundObjectProperty)
            {
                referencedType += ImplementationSuffix;
            }
            List<KeyValuePair<string, string>> typeAndNameList = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetProxyTypeReference(prop.ObjectClass as ObjectClass, this.Settings), "Parent"),
                new KeyValuePair<string, string>("bool", "ValueIsNull"),
                new KeyValuePair<string, string>(referencedType, "Value"),
            };

            if (IsOrdered())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "Value" + Kistl.API.Helper.PositionSuffix));
            }

            // Even exportable value collection entries do not have an 
            // export guid, since they are serialized in-place in the container
            //if (IsExportable())
            //{
            //    typeAndNameList.Add(new KeyValuePair<string, string>("Guid", "ExportGuid"));
            //}

            this.WriteLine("        public override void SaveOrUpdateTo(NHibernate.ISession session)");
            this.WriteLine("        {");
            this.WriteLine("            // ValueCollectionEntries and CompoundCollectionEntries are saved by cascade");
            this.WriteLine("            //base.SaveOrUpdateTo(session);");
            this.WriteLine("        }");
            this.WriteLine("");

            ObjectClasses.ProxyClass.Call(Host, ctx, interfaceName, new KeyValuePair<string, string>[0], typeAndNameList);
        }

        protected override void ApplyReloadReferenceBody()
        {
            string referencedInterface = prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
            var cls = prop.ObjectClass as ObjectClass;
            if (cls == null)
            {
                if (prop.ObjectClass == null)
                {
                    Logging.Log.ErrorFormat("tried to create ReloadReferenceBody for unattached property: [{0}]", prop.Name);
                }
                else
                {
                    Logging.Log.ErrorFormat("tried to create ReloadReferenceBody for property [{0}] attached to non-ObjectClass: [{1}]", prop.Name, prop.ObjectClass);
                }
            }
            else
            {
                string referencedImplementation = Mappings.ObjectClassHbm.GetWrapperTypeReference(prop.ObjectClass as ObjectClass, this.Settings);
                // value collections are always exported / serialized inline. no need for parent guid
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "Parent", "Parent", "_fk_Parent", null, false);
            }
        }
    }
}
