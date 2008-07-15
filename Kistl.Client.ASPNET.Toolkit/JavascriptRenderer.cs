using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kistl.Client.ASPNET.Toolkit
{
    [ToolboxData("<{0}:JavascriptRenderer runat=server></{0}:JavascriptRenderer>")]
    public class JavascriptRenderer : ScriptControl
    {
        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            return null;
        }

        protected override IEnumerable<ScriptReference> GetScriptReferences()
        {
            return null;
        }
    }
}
