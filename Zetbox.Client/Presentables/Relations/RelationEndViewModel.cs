
namespace Zetbox.Client.Presentables.Relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ValueViewModels;

    public class RelationEndViewModel
        : DataObjectViewModel
    {
        public new delegate RelationEndViewModel Factory(IZetboxContext dataCtx, ViewModel parent, RelationEnd relEnd);

        public RelationEndViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            RelationEnd relEnd)
            : base(appCtx, dataCtx, parent, relEnd)
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

        public NullableBoolPropertyViewModel HasOrderPersisted { get; private set; }

        #endregion

        #region Utilities and UI callbacks
        #endregion

        #region Event handlers
        #endregion

    }
}
