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

namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Templates = Zetbox.Generator.Templates;

    public class ExportGuidProperty
        : ProxyProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string interfaceName)
        {
            if (_host == null) { throw new ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ExportGuidProperty", ctx, serializationList, moduleNamespace, interfaceName);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string interfaceName)
            : base(_host, ctx, serializationList, moduleNamespace, "Guid", "ExportGuid", false, false, false, interfaceName, null, false, null, Guid.Empty, "Guid", "ExportGuid", false, false)
        { 
        }

        protected override void ApplyOnGetTemplate()
        {
            this.WriteObjects("                if (this.Proxy.ExportGuid == Guid.Empty) {");
            this.WriteLine();
            this.WriteObjects("                    __result = this.Proxy.ExportGuid = Guid.NewGuid();");
            this.WriteLine();
            this.WriteObjects("                }");
            this.WriteLine();
        }
    }
}
