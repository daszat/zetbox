using Autofac;
using Autofac.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zetbox.API.Server;
using Zetbox.API.Utils;
using Zetbox.App.Base;

namespace Zetbox.Server.HttpService
{
    public partial class Check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            output.Text = "NO query yet";

            IContainerProviderAccessor cpa = null;
            try
            {
                cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
                var scope = cpa.ContainerProvider.RequestLifetime;

                var ctx = scope.Resolve<IZetboxServerContext>();
                if (ctx.GetQuery<Group>().Count() > 0)
                {
                    output.Text = "OK";
                }
                else
                {
                    output.Text = "No groups found";
                }
            }
            catch (Exception ex)
            {
                output.Text = "Error";
                Logging.Server.Error("Error while checking status", ex);
            }
            finally
            {
                if (cpa != null && cpa.ContainerProvider != null)
                {
                    cpa.ContainerProvider.EndRequestLifetime();
                }
            }
        }
    }
}