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

namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Templates = Zetbox.Generator.Templates;
    using Zetbox.API;

    public class OnPropertyChange : Templates.ObjectClasses.OnPropertyChange
    {
        public OnPropertyChange(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host, ctx, dt)
        {
        }

        protected override List<Property> GetNonModifyingProperties()
        {
            var result = base.GetNonModifyingProperties();

            result.AddRange(dt.Properties.OfType<Property>()
                .Where(p => p.IsCalculated())
                .OrderBy(p => p.Name));

            return result.Distinct().ToList();
        }
    }
}
