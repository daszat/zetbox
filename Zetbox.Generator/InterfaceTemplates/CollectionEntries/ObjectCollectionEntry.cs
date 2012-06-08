// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.InterfaceTemplates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    public partial class ObjectCollectionEntry
        : Template
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IZetboxContext ctx, Relation rel)
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

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Relation rel)
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
