
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// Add more locig for helping defining a MethodInvocation
    /// </summary>
    [ViewModelDescriptor]
    public class MethodInvocationViewModel
        : DataObjectViewModel
    {
        public new delegate MethodInvocationViewModel Factory(IKistlContext dataCtx, MethodInvocation mdl);

        private MethodInvocation _invocation;

        public MethodInvocationViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            MethodInvocation mdl)
            : base(appCtx, config, dataCtx, mdl)
        {
            _invocation = mdl;
        }

        #region Utilities and UI callbacks
        #endregion

        #region PropertyChanged event handlers
        #endregion
    }
}
