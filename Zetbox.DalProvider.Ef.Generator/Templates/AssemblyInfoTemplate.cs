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
using System.Diagnostics;
using System.Linq;
using System.Text;

using Zetbox.Generator.Extensions;

namespace Zetbox.DalProvider.Ef.Generator.Templates
{
    public class AssemblyInfoTemplate : Zetbox.Generator.Templates.AssemblyInfoTemplate
    {

        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx)
            : base(_host, ctx)
        {
        }

        public override string GetAssemblyTitle()
        {
            return "Zetbox.Server.Objects";
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            WriteLine("[assembly: System.Data.Objects.DataClasses.EdmSchema]");
        }

    }
}


