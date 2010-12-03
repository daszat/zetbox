
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    public class ErrorDescriptor
    {
        private readonly DataObjectViewModel _item;
        private readonly IList<string> _errors;
        public ErrorDescriptor(DataObjectViewModel item, IList<string> errors)
        {
            this._item = item;
            this._errors = errors;
        }

        public DataObjectViewModel Item { get { return _item; } }
        public IList<string> Errors { get { return _errors; } }
    }

    /// <summary>
    /// A simple model presenting a list of errors from constraints of the specified DataContext.
    /// </summary>
    public class ErrorListViewModel
        : ViewModel
    {
        public new delegate ErrorListViewModel Factory(IKistlContext dataCtx);

        public ErrorListViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            _errors = new ObservableCollection<ErrorDescriptor>();
            _roErrors = new ReadOnlyObservableCollection<ErrorDescriptor>(_errors);
        }

        public override string Name
        {
            get { return "Error List"; }
        }

        private readonly ObservableCollection<ErrorDescriptor> _errors;
        private readonly ReadOnlyObservableCollection<ErrorDescriptor> _roErrors;
        public ReadOnlyObservableCollection<ErrorDescriptor> Errors
        {
            get { return _roErrors; }
        }

        public void RefreshErrors()
        {
            _errors.Clear();
            foreach (var error in DataContext.AttachedObjects
                .OfType<IDataObject>()
                .Where(o => o.ObjectState == DataObjectState.Modified || o.ObjectState == DataObjectState.New)
                .Select(o => new { obj = o, err = o.Error })
                .Where(tmp => !String.IsNullOrEmpty(tmp.err)))
            {
                _errors.Add(new ErrorDescriptor(
                    DataObjectViewModel.Fetch(ViewModelFactory, DataContext, error.obj),
                    new List<string>() { error.err }));
            }
        }
    }
}
