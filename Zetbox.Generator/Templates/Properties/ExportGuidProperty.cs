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

namespace Zetbox.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;

    public class ExportGuidProperty
        : NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
          IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string backingName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.ExportGuidProperty",
                ctx, serializationList, backingName);
        }

        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx,
            Serialization.SerializationMembersList serializationList, string backingName)
            : base(_host, ctx, serializationList, "Guid", "ExportGuid", String.Empty, backingName, false, false) // TODO: use proper namespace
        {
        }

        protected override void ApplyOnGetTemplate()
        {
            base.ApplyOnGetTemplate();

            this.WriteObjects("                if (", backingName, " == Guid.Empty) {\r\n");
            this.WriteObjects("                    __result = ", backingName, " = Guid.NewGuid();\r\n");
            this.WriteObjects("                }\r\n");
        }
    }
}
