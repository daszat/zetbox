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
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Relation rel)
        {
            host.CallTemplate("Implementation.CollectionEntries.ObjectCollectionEntry", ctx, rel);
        }

        protected Relation rel { get; private set; }

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx)
        {
            this.rel = rel;
        }

        protected override void ApplyRelationIdPropertyTemplate()
        {
            this.WriteLine("        public Guid RelationID {{ get {{ return new Guid(\"{0}\"); }} }}", rel.ExportGuid);
        }

        protected override void ApplyObjectGetterTemplate()
        {
            this.WriteLine("        public IDataObject AObject {{ get {{ return A; }} set {{ A = ({0})value; }} }}", rel.A.Type.GetDataTypeString());
            this.WriteLine("        public IDataObject BObject {{ get {{ return B; }} set {{ B = ({0})value; }} }}", rel.B.Type.GetDataTypeString());
        }

        protected override string GetCeClassName()
        {
            return rel.GetRelationClassName() + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetCeInterface()
        {
            return rel.GetRelationClassName();
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

        protected override bool ImplementsIExportable()
        {
            return rel.A.Type.ImplementsIExportable(ctx) && rel.B.Type.ImplementsIExportable(ctx);
        }

    }
}
