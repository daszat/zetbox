
namespace Kistl.DalProvider.NHibernate.Generator.Implementation.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.DalProvider.NHibernate.Generator.Implementation.ObjectClasses;
    using Kistl.Server.Generators.Extensions;
    using Templates = Kistl.Server.Generators.Templates;

    public sealed class ObjectCollectionEntry
         : Templates.Implementation.CollectionEntries.ObjectCollectionEntry
    {
        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, rel)
        {
        }

        protected override string GetCeBaseClassName()
        {
            return "Kistl.DalProvider.NHibernate.BaseNhCollectionEntry";
        }

        protected override void ApplyObjectReferenceProperty(Relation rel, RelationEndRole endRole, string propertyName)
        {
            WriteObjects("        public ", rel.GetEndFromRole(endRole).Type.GetDataTypeString(), " ", endRole, " { get; set; }");
            WriteObjects("        private ", rel.GetEndFromRole(endRole).Type.GetDataTypeString(), " ", endRole, Kistl.API.Helper.ImplementationSuffix, " { get { return ", endRole, "; } set { ", endRole, " = value; } }");
            WriteLine();
        }

        protected override void ApplyIndexPropertyTemplate(Relation rel, RelationEndRole endRole)
        {
            WriteObjects("        public int? ", endRole, "Index { get; set; }");
            WriteLine();
        }
    }
}
