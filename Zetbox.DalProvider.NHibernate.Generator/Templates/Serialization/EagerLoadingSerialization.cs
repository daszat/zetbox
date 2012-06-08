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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public partial class EagerLoadingSerialization
        : Templates.Serialization.EagerLoadingSerialization
    {
        protected string relationEntryCollectionName;

        public EagerLoadingSerialization(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Templates.Serialization.SerializerDirection direction, string streamName, string xmlnamespace, string xmlname, string collectionName, bool serializeIds, bool serializeRelationEntries, string relationEntryCollectionName)
            : base(_host,ctx, direction, streamName, xmlnamespace, xmlname, collectionName, serializeIds, serializeRelationEntries)
        {
            this.relationEntryCollectionName = relationEntryCollectionName;
        }
        
        protected override void ApplyRelationEntrySerialization()
        {
            this.WriteObjects("				foreach(var relEntry in ", relationEntryCollectionName, ")");
            this.WriteLine();
            this.WriteObjects("				{");
            this.WriteLine();
            this.WriteObjects("					auxObjects.Add(OurContext.AttachAndWrap(relEntry));");
            this.WriteLine();
            this.WriteObjects("				}");
            this.WriteLine();
        }
    }
}
