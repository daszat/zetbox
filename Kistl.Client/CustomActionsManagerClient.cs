
namespace Kistl.Client
{
    using System;
    using System.Linq;

    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// Implementation of the client-side CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerClient
        : BaseCustomActionsManager
    {
        /// <summary>
        /// Displays the warnings to the user.
        /// </summary>
        /// <param name="warnings">the warnings to display</param>
        protected override void ProcessWarnings(string warnings)
        {
            System.Diagnostics.Debug.WriteLine(warnings);
            if (GuiApplicationContext.Current.Renderer != null)
            {
                GuiApplicationContext.Current.Renderer.ShowMessage(warnings);
            }
        }

        /// <summary>
        /// Decides whether or no a deployment restriction is acceptable
        /// </summary>
        /// <param name="r">the restriction to check</param>
        /// <returns>true if the restriction is None or ClientOnly</returns>
        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ClientOnly || r == (int)DeploymentRestriction.None;
        }
    }
}
