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

    public class CollectionSerialization
        : Templates.Serialization.CollectionSerialization
    {
        public CollectionSerialization(
            Arebis.CodeGeneration.IGenerationHost _host,
            IZetboxContext ctx,
            Templates.Serialization.SerializerDirection direction,
            string streamName,
            string xmlnamespace,
            string xmlname,
            string collectionName,
            bool orderByValue)
            : base(_host, ctx, direction, streamName, xmlnamespace, xmlname, collectionName, orderByValue)
        {
        }

        public override bool ShouldSerialize()
        {
            // Do not deserialize colletion entries from client to server
            // they will be send by the Client ZetboxContext as seperate objects
            // from server to client the will be serialized - some kind of eager loading
            return direction != Templates.Serialization.SerializerDirection.FromStream;
        }
    }
}
