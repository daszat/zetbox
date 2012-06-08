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


namespace Zetbox.App.Projekte.Client.Projekte.Reporting.Common
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\PageSetup.cst")]
    public partial class PageSetup : Zetbox.App.Projekte.Client.Projekte.Reporting.ReportTemplate
    {
		protected string Orientation;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.PageSetup", Orientation);
        }

        public PageSetup(Arebis.CodeGeneration.IGenerationHost _host, string Orientation)
            : base(_host)
        {
			this.Orientation = Orientation;

        }

        public override void Generate()
        {
#line 9 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\PageSetup.cst"
this.WriteObjects("\r\n");
this.WriteObjects("PageSetup\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("	Orientation = ",  Orientation , "\r\n");
this.WriteObjects("	PageFormat = A4\r\n");
this.WriteObjects("	TopMargin = 40\r\n");
this.WriteObjects("	StartingNumber = 1\r\n");
this.WriteObjects("}\r\n");

        }

    }
}