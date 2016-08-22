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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public class CollectionEntries
        : Zetbox.Server.Generators.Templates.Implementation.ObjectClasses.CollectionEntries
    {
        public CollectionEntries(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx)
            : base(_host,ctx)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Zetbox.API.Server",
                "Zetbox.DalProvider.EF",
                "System.Data.Objects",
                "System.Data.Objects.DataClasses"
            });
        }
    }
}
