
namespace Kistl.Server
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// Implementierung des Serverseitigen CustomActionsManager
    /// </summary>
    internal class CustomActionsManagerServer
        : BaseCustomActionsManager
    {
        /// <summary>
        /// Replace the Base functionality, since the server can be used in situations where the FrozenContext is not correct (e.g. before generating).
        /// Additionally the server is much less performance sensitive at this point, because it only seldom starts up in comparison to clients.
        /// </summary>
        protected override void InitCore()
        {
            using (var ctx = KistlContext.GetContext())
            {
                try
                {
                    InitializeProvider(ctx, String.Empty, ServerApplicationContext.Current.ImplementationAssembly);
                }
                catch { 
                    // Avoid errors while bootstrapping
                    // TODO: avoid errors ONLY while bootstrapping
                    Trace.TraceError("Error while trying to initialise CustomServerActions");
                }
            }
        }

        /// <inheritdoc/>
        protected override void InitFrozen()
        {
            try
            {
                base.InitFrozen();
            }
            catch (TypeLoadException)
            {
                // ignore missing FrozenContext
            }
        }

        ///// <summary>
        ///// Decides whether or no a deployment restriction is acceptable
        ///// </summary>
        ///// <param name="r">the restriction to check</param>
        ///// <returns>true if the restriction is None or ServerOnly</returns>
        //protected override bool IsAcceptableDeploymentRestriction(DeploymentRestriction r)
        //{
        //    return r == DeploymentRestriction.ServerOnly || r == DeploymentRestriction.None;
        //}

        protected override bool IsAcceptableDeploymentRestriction(int r)
        {
            return r == (int)DeploymentRestriction.ServerOnly || r == (int)DeploymentRestriction.None;
        }

        /// <summary>
        /// writes warnings into the trace.
        /// </summary>
        /// <param name="warnings">the warnings to process</param>
        protected override void ProcessWarnings(string warnings)
        {
            Trace.TraceWarning(warnings);
        }
    }
}
