using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.ComponentModel;

namespace Kistl.Server.HttpService.Controls
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