
namespace Kistl.DalProvider.NHibernate.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
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

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();
            ObjectClasses.Constructors.Call(Host, ctx,
                new CompoundObjectProperty[0],
                GetCeInterface(),
                GetCeClassName(),
                null);
        }


        protected override void ApplyBIndexPropertyTemplate()
        {
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "ValueIndex", "int?", "this.Proxy.Value" + Kistl.API.Helper.PositionSuffix, "int?");
        }
        
        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = GetCeInterface();

            List<KeyValuePair<string, string>> typeAndNameList = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetInterfaceTypeReference(prop.ObjectClass as ObjectClass, this.Settings), "Parent"),
                new KeyValuePair<string, string>(prop.GetPropertyTypeString(), "Value"),
            };

            if (IsOrdered())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "Value" + Kistl.API.Helper.PositionSuffix));
            }

            if (IsExportable())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("Guid", "ExportGuid"));
            }


            ObjectClasses.ProxyClass.Call(Host, ctx, interfaceName, typeAndNameList);
        }

        protected override string GetExportGuidBackingStoreReference()
        {
            return "this.Proxy.ExportGuid";
        }

        protected override void ApplyReloadReferenceBody()
        {
            string referencedImplementation = Mappings.ObjectClassHbm.GetImplementationTypeReference(prop.ObjectClass as ObjectClass, this.Settings);
            ObjectClasses.ReloadOneReference.Call(Host, ctx, null, referencedImplementation, "Parent", "Parent", "_fk_Parent", "_fk_guid_Parent", IsExportable());
        }
    }
}
