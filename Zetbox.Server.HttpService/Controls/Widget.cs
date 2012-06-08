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
using System.Web;
using System.Web.UI;
using System.ComponentModel;

namespace Zetbox.Server.HttpService.Controls
{
    public class Widget : Control
    {
        [Localizable(true)]
        public string Title { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<div class=\"widget\"><div class=\"top\"><div class=\"bottom\"><div class=\"left\"><div class=\"right\"><div class=\"ro\"><div class=\"lo\"><div class=\"ru\"><div class=\"lu\">\n");
            writer.Write("<h4 class=\"widget-title\">");
            writer.Write(Title);
            writer.Write("</h4>\n");

            writer.Write("<div class=\"widget-content\">\n");
            base.RenderChildren(writer);
            writer.Write("</div>\n");

            writer.Write("</div></div></div></div></div></div></div></div>\n");
        }
    }
}