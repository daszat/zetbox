using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Zetbox.Server.HttpService.Controls
{
    [ToolboxData("<{0}:ClickOnceApplicationLink runat=server></{0}:ClickOnceApplicationLink>")]
    public class ClickOnceApplicationLink : Control
    {
        protected override void Render(HtmlTextWriter output)
        {
            var basePath = this.MapPathSecure("~/");
            var applications = Directory.GetFiles(basePath, "*.application", SearchOption.AllDirectories);

            var links = string.Join("<br/>", applications.Select(path => 
            {
                var name = Path.GetFileName(path);
                var relPath = path.Substring(basePath.Length);
                var url = ResolveClientUrl("~/" + relPath.Replace('\\', '/'));
                return string.Format("<a href=\"{0}\">{1}</a>", url, name);
            }));
            
            output.Write(links);
        }
    }
}
