using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class ObjectCollectionEntry
        : Template
    {
        protected Relation rel { get; private set; }

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx)
        {
            this.rel = rel;
        }

        protected override void ApplyRelationIdPropertyTemplate()
        {
            this.WriteLine("        public override int RelationID {{ get {{ return {0}; }} }}", rel.ID);
        }

        protected override string GetCeClassName()
        {
            return rel.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetCeInterface()
        {
            return rel.GetCollectionEntryClassName();
        }

        protected override bool IsOrdered()
        {
            return rel.NeedsPositionStorage(RelationEndRole.A) || rel.NeedsPositionStorage(RelationEndRole.B);
        }

        protected override void ApplyChangesFromBody()
        {
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                this.WriteLine("            me.AIndex = other.AIndex;");
            }
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                this.WriteLine("            me.BIndex = other.BIndex;");
            }
        }

    }
}
