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

namespace Zetbox.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AssemblyInfoTemplate
        : Templates.AssemblyInfoTemplate
    {
        public AssemblyInfoTemplate(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx)
            : base(_host, ctx)
        {
        }

        public override void ApplyAdditionalAssemblyInfo()
        {
            base.ApplyAdditionalAssemblyInfo();
            this.WriteLine("[assembly: System.CLSCompliantAttribute(true)]");
            this.WriteLine("[assembly: Zetbox.API.ZetboxGeneratedVersion(\"" + Guid.NewGuid() + "\")]");
        }
    }
}
