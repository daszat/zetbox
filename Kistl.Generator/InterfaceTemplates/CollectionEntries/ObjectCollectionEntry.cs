
namespace Kistl.Generator.InterfaceTemplates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

    public partial class ObjectCollectionEntry
        : Template
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Relation rel)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.ObjectCollectionEntry", ctx, rel);
        }

        protected Relation rel { get; private set; }

        private static Module CheckNullOrReturnRelationModule(Relation rel)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }
            return rel.Module;
        }

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, CheckNullOrReturnRelationModule(rel))
        {
            this.rel = rel;
        }

        protected override string GetDefinitionGuid()
        {
            return rel.ExportGuid.ToString();
        }

        protected override string GetCeClassName()
        {
            return rel.GetRelationClassName();
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "IRelationListEntry" : "IRelationEntry",
                rel.A.Type.Name,
                rel.B.Type.Name);
        }

        protected override bool IsOrdered()
        {
            return rel.NeedsPositionStorage(RelationEndRole.A) || rel.NeedsPositionStorage(RelationEndRole.B);
        }


        protected override string GetDescription()
        {
            return String.Format("ObjectCollectionEntry for {0}", rel.Description);
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            var additionalImports = new HashSet<string>(base.GetAdditionalImports());

            // import referenced objectclasses' namespaces
            if (!String.IsNullOrEmpty(rel.A.Type.Module.Namespace))
                additionalImports.Add(rel.A.Type.Module.Namespace);
            if (!String.IsNullOrEmpty(rel.B.Type.Module.Namespace))
                additionalImports.Add(rel.B.Type.Module.Namespace);

            return additionalImports;
        }
    }
}
