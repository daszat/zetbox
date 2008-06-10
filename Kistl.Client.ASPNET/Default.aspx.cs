using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Kistl.API;
using Kistl.API.Client;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTo();
        }
    }

    private void BindTo()
    {
        using (IKistlContext ctx = KistlContext.GetContext())
        {
            repProjects.DataSource = ctx.GetQuery<Kistl.App.Projekte.Projekt>();
            repProjects.DataBind();
        }
    }
}
