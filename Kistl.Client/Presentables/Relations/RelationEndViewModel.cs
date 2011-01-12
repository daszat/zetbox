
namespace Kistl.Client.Presentables.Relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.ValueViewModels;

    public class RelationEndViewModel
        : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate RelationEndViewModel Factory(IKistlContext dataCtx, RelationEnd relEnd);
#else
        public new delegate RelationEndViewModel Factory(IKistlContext dataCtx, RelationEnd relEnd);
#endif

        public RelationEndViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            RelationEnd relEnd)
            : base(appCtx, dataCtx, relEnd)
        {
        }

        #region Public interface

        public ValueViewModel<string, string> RoleName
        {
            get
            {
                return (ValueViewModel<string, string>)this.PropertyModelsByName["RoleName"];
            }
        }

        public DataObjectViewModel Navigator { get; private set; }

        public ICommandViewModel CreateNavigatorCommand { get; private set; }

        public ObjectReferenceViewModel RelatedClass { get; private set; }

        public NullableStructValueViewModel<bool> HasOrderPersisted { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }
}
