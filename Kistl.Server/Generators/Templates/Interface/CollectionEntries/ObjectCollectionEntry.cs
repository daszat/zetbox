using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.CollectionEntries
{
    public partial class ObjectCollectionEntry
        : Template
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Relation rel)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Interface.CollectionEntries.ObjectCollectionEntry", ctx, rel);
        }

        protected Relation rel { get; private set; }

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, (rel.A.Navigator ?? rel.B.Navigator).Module)
        {
            this.rel = rel;
        }

        protected override string GetCeClassName()
        {
            return rel.GetRelationClassName();
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "IRelationListEntry" : "IRelationCollectionEntry",
                rel.A.Type.ClassName,
                rel.B.Type.ClassName);
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
            var additionalImports = new HashSet<string>();

            // don't forget to import referenced referenced objectclasses' namespaces
            additionalImports.Add(rel.A.Type.Module.Namespace);
            additionalImports.Add(rel.B.Type.Module.Namespace);

            return base.GetAdditionalImports().Concat(additionalImports);
        }
    }
}
