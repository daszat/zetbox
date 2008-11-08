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
using Kistl.Client;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTree();
        }
    }

    protected void lnkRefresh_OnRefresh(object sender, EventArgs e)
    {
        BindTree();
    }
    
    protected void tree_OnTreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth == 0 && e.Node.ChildNodes.Count == 1 && string.IsNullOrEmpty(e.Node.ChildNodes[0].Value))
        {
            e.Node.ChildNodes.Clear();
            int mID = Convert.ToInt32(e.Node.Value);

            foreach (var @class in FrozenContext.Single.GetQuery<Kistl.App.Base.ObjectClass>().Where(c => c.Module.ID == mID))
            {
                TreeNode tn = new TreeNode(@class.ClassName, @class.ID.ToString());
                e.Node.ChildNodes.Add(tn);
            }
        }
    }

    protected void tree_OnSelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode n = tree.SelectedNode;
        if (n == null) return;
        if (n.Depth == 1)
        {
            int cID = Convert.ToInt32(n.Value);
            var @class = FrozenContext.Single.Find<Kistl.App.Base.ObjectClass>(cID);

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                repItems.DataSource = ctx.GetQuery(@class.GetDataType());
                repItems.DataBind();
            }
        }
    }

    private void BindTree()
    {
        tree.Nodes.Clear();
        foreach (var module in FrozenContext.Single.GetQuery<Kistl.App.Base.Module>())
        {
            TreeNode tn = new TreeNode(module.ModuleName, module.ID.ToString());
            tree.Nodes.Add(tn);

            tn.ChildNodes.Add(new TreeNode());
        }
    }
}
