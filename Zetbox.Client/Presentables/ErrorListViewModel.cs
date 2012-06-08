
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using System.ComponentModel;
    using Zetbox.Client.Presentables.ObjectEditor;

    public class ErrorDescriptor : ViewModel
    {
        public new delegate ErrorDescriptor Factory(IZetboxContext dataCtx, ViewModel parent, DataObjectViewModel item, IList<string> errors);

        private readonly DataObjectViewModel _item;
        private readonly IList<string> _errors;

        public ErrorDescriptor(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, DataObjectViewModel item, IList<string> errors)
            : base(dependencies, dataCtx, parent)
        {
            this._item = item;
            this._errors = errors;
        }

        public DataObjectViewModel Item { get { return _item; } }
        public IList<string> Errors { get { return _errors; } }

        private ICommandViewModel _GotoObjectCommand = null;
        public ICommandViewModel GotoObjectCommand
        {
            get
            {
                if (_GotoObjectCommand == null)
                {
                    _GotoObjectCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        ErrorListViewModelResources.GotoObjectCommand_Name,
                        ErrorListViewModelResources.GotoObjectCommand_Tooltip,
                        GotoObject, null, null);
                }
                return _GotoObjectCommand;
            }
        }

        public void GotoObject()
        {
            ViewModelFactory.ShowModel(Item, true);
        }

        public override string Name
        {
            get { return Item.Name; }
        }
    }

    /// <summary>
    /// A simple model presenting a list of errors from constraints of the specified DataContext.
    /// </summary>
    public class ErrorListViewModel
        : WindowViewModel
    {
        public new delegate ErrorListViewModel Factory(IZetboxContext dataCtx, WorkspaceViewModel parent);

        public ErrorListViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, WorkspaceViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            _errors = new ObservableCollection<ErrorDescriptor>();
            _roErrors = new ReadOnlyObservableCollection<ErrorDescriptor>(_errors);
            DisplayErrors();
        }

        public new WorkspaceViewModel Parent
        {
            get
            {
                return (WorkspaceViewModel)base.Parent;
            }
        }

        public override string Name
        {
            get { return ErrorListViewModelResources.Name; }
        }

        private readonly ObservableCollection<ErrorDescriptor> _errors;
        private readonly ReadOnlyObservableCollection<ErrorDescriptor> _roErrors;
        public ReadOnlyObservableCollection<ErrorDescriptor> Errors
        {
            get { return _roErrors; }
        }

        private ICommandViewModel _CloseCommand = null;
        public ICommandViewModel CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        null,
                        ErrorListViewModelResources.CloseCommand_Name,
                        ErrorListViewModelResources.CloseCommand_Tooltip, Close, null, null);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            this.Show = false;
        }

        private ICommandViewModel _RefreshErrorsCommand = null;
        public ICommandViewModel RefreshErrorsCommand
        {
            get
            {
                if (_RefreshErrorsCommand == null)
                {
                    _RefreshErrorsCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this,
                        ErrorListViewModelResources.RefreshCommand_Name,
                        ErrorListViewModelResources.RefreshCommand_Tooltip, 
                        RefreshErrors,
                        null, 
                        null);
                }
                return _RefreshErrorsCommand;
            }
        }

        public void RefreshErrors()
        {
            Parent.UpdateErrors();
            DisplayErrors();
        }

        private void DisplayErrors()
        {
            _errors.Clear();
            foreach (var error in Parent.GetErrors())
            {
                IDataObject obj = error as IDataObject;
                if (obj == null && error is ViewModel)
                {
                    var vmdl = (ViewModel)error;
                    if(vmdl.Parent is DataObjectViewModel)
                    {
                        obj = ((DataObjectViewModel)vmdl.Parent).Object;
                    }
                }

                _errors.Add(ViewModelFactory.CreateViewModel<ErrorDescriptor.Factory>().Invoke(
                    DataContext, this,
                    obj == null ? null : DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj),
                    new List<string>() { error.Error }));
            }
        }
    }
}
