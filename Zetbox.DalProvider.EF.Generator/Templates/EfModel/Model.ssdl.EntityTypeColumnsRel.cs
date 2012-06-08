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

namespace Zetbox.DalProvider.Ef.Generator.Templates.EfModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;

    public partial class ModelSsdlEntityTypeColumnsRel
    {
        private void ProcessRelation(Relation rel)
        {
            if (rel.A.Type == rel.B.Type)
            {
                if (rel.A.Type != cls)
                {
                    throw new ArgumentException(String.Format("contains self-Relation {0} which doesn't match the specified ObjectClass {1}", rel, cls), "rel");
                }

                if (rel.HasStorage(RelationEndRole.A))
                {
                    ProcessRelationEnd(rel, rel.A);
                }

                if (rel.HasStorage(RelationEndRole.B))
                {
                    ProcessRelationEnd(rel, rel.B);
                }
            }
            else if (rel.A.Type == cls)
            {
                if (!rel.HasStorage(RelationEndRole.A))
                {
                    throw new ArgumentException(String.Format("contains Relation {0} which doesn't need storage on the A-side", rel, cls), "rel");
                }

                ProcessRelationEnd(rel, rel.A);
            }
            else if (rel.B.Type == cls)
            {
                if (!rel.HasStorage(RelationEndRole.B))
                {
                    throw new ArgumentException(String.Format("contains Relation {0} which doesn't need storage on the B-side", rel, cls), "rel");
                }

                ProcessRelationEnd(rel, rel.B);
            }
            else
            {
                throw new ArgumentException(String.Format("contains Relation {0} which doesn't match the specified ObjectClass {1}", rel, cls), "rel");
            }
        }

        private void ProcessRelationEnd(Relation rel, RelationEnd relEnd)
        {
            var otherEnd = rel.GetOtherEnd(relEnd);

            string propertyName = rel.GetRelationFkNameToEnd(otherEnd);
            bool needPositionStorage = rel.NeedsPositionStorage(relEnd.GetRole());
            string positionColumnName = Construct.ListPositionColumnName(otherEnd, prefix);

            GenerateProperty(
                propertyName,
                needPositionStorage,
                positionColumnName);
        }
    }
}