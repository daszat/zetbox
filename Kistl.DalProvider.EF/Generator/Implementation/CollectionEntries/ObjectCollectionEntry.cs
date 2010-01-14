using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.DalProvider.EF.Generator.Implementation.CollectionEntries
{
    public partial class ObjectCollectionEntry
        : Kistl.Server.Generators.Templates.Implementation.CollectionEntries.ObjectCollectionEntry
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


    }
}
