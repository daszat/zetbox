namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class NewEventDialogViewModel : WindowViewModel
    {
        public new delegate NewEventDialogViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public NewEventDialogViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
            Result = false;
        }

        public override string Name
        {
            get { return "New event"; }
        }

        public bool Result { get; private set; }

        private List<IEventInputViewModel> _inputViewModels;
        public List<IEventInputViewModel> InputViewModels        
        {
            get
            {
                return _inputViewModels;
            }
            set
            {
                if (_inputViewModels != value)
                {
                    _inputViewModels = value;
                    if (_inputViewModels != null)
                        _selectedInputViewModel = _inputViewModels.FirstOrDefault();
                    OnPropertyChanged("InputViewModels");
                    OnPropertyChanged("SelectedInputViewModel");
                }
            }
        }

        private IEventInputViewModel _selectedInputViewModel;
        public IEventInputViewModel SelectedInputViewModel
        {
            get
            {
                return _selectedInputViewModel;
            }
            set
            {
                if (_selectedInputViewModel != value)
                {
                    _selectedInputViewModel = value;
                    OnPropertyChanged("SelectedInputViewModel");
                }
            }
        }

        public EventViewModel CreateNew()
        {
            if(_selectedInputViewModel == null) return null;
            return _selectedInputViewModel.CreateNew();
        }

        #region Commands
        #region New command
        private ICommandViewModel _NewCommand = null;
        public ICommandViewModel NewCommand
        {
            get
            {
                if (_NewCommand == null)
                {
                    _NewCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this,
                        CommonCommandsResources.NewDataObjectCommand_Name,
                        CommonCommandsResources.NewDataObjectCommand_Tooltip, 
                        New, 
                        CanNew, 
                        CanNewReason);
                    _NewCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext));

                }
                return _NewCommand;
            }
        }

        public bool CanNew()
        {
            return SelectedInputViewModel != null && string.IsNullOrEmpty(SelectedInputViewModel.Error);
        }

        public string CanNewReason()
        {
            if(SelectedInputViewModel == null) return CommonCommandsResources.DataObjectCommand_NothingSelected;
            if (!string.IsNullOrEmpty(SelectedInputViewModel.Error)) return SelectedInputViewModel.Error;

            return CommonCommandsResources.DataObjectCommand_ProgrammerError;
        }

        public void New()
        {
            if (!CanNew()) return;

            Result = true;
            Show = false;
        }
        #endregion

        #region Cancel command
        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Cancel", "", Cancel, CanCancel, CanCancelReason);
                }
                return _CancelCommand;
            }
        }

        public bool CanCancel()
        {
            return true;
        }

        public string CanCancelReason()
        {
            return string.Empty;
        }

        public void Cancel()
        {
            if (!CanCancel()) return;
            Show = false;
        }
        #endregion
        #endregion
    }
}
