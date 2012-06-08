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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\DocumentInfo.cst")]
    public partial class DocumentInfo : Zetbox.App.Projekte.Client.Projekte.Reporting.ReportTemplate
    {
		protected string Subject;
		protected string Author;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, string Subject, string Author)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Common.DocumentInfo", Subject, Author);
        }

        public DocumentInfo(Arebis.CodeGeneration.IGenerationHost _host, string Subject, string Author)
            : base(_host)
        {
			this.Subject = Subject;
			this.Author = Author;

        }

        public override void Generate()
        {
#line 10 "P:\Zetbox\Zetbox.App.Projekte.Client\Projekte\Reporting\Common\DocumentInfo.cst"
this.WriteObjects("Info\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("	Title = \"",  Subject , "\"\r\n");
this.WriteObjects("	Subject = \"",  Subject , "\"\r\n");
this.WriteObjects("	Author = \"",  Author , "\"\r\n");
this.WriteObjects("}\r\n");

        }

    }
}