
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

    public class RelationEntry
        : Templates.CollectionEntries.RelationEntry
    {
        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
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

        protected override void ApplyAIndexPropertyTemplate()
        {
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "AIndex", "int?", "this.Proxy.A" + Kistl.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyBIndexPropertyTemplate()
        {
            Templates.Properties.DelegatingProperty.Call(Host, ctx, "BIndex", "int?", "this.Proxy.B" + Kistl.API.Helper.PositionSuffix, "int?");
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();

            string interfaceName = GetCeInterface();

            List<KeyValuePair<string, string>> typeAndNameList = new List<KeyValuePair<string, string>>(){
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetImplementationTypeReference(rel.A.Type, this.Settings), "A"),
                new KeyValuePair<string, string>(Mappings.ObjectClassHbm.GetImplementationTypeReference(rel.B.Type, this.Settings), "B"),
            };

            if (IsOrdered())
            {
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "A" + Kistl.API.Helper.PositionSuffix));
                typeAndNameList.Add(new KeyValuePair<string, string>("int?", "B" + Kistl.API.Helper.PositionSuffix));
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
    }
}
