using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.EntityFramework.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
        : Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override void ApplyClassAttributeTemplate()
        {
            base.ApplyClassAttributeTemplate();
            this.WriteObjects(@"    [EdmEntityType(NamespaceName=""Model"", Name=""", rel.GetRelationClassName(), @""")]");
            this.WriteLine();
        }

        protected override string GetCeBaseClassName()
        {
            return "BaseServerCollectionEntry_EntityFramework";
        }

        protected override void ApplyChangesFromBody()
        {
            base.ApplyChangesFromBody();
            this.WriteLine("            me._fk_A = other._fk_A;");
            this.WriteLine("            me._fk_B = other._fk_B;");
        }

        protected override bool ImplementsIExportable()
        {
            return rel.A.Type.ImplementsIExportable(ctx) && rel.B.Type.ImplementsIExportable(ctx);
        }
    }
}
