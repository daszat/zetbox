namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.App.Calendar;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class NewEventDialogViewModel : WindowViewModel
    {
        public new delegate NewEventDialogViewModel Factory(IZetboxContext dataCtx, ViewModel parent, CalendarBook calendar);

        public NewEventDialogViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, CalendarBook calendar)
            : base(appCtx, dataCtx, parent)
        {
            Result = false;
            Calendar = calendar;
        }

        public override string Name
        {
            get { return "New event"; }
        }

        public CalendarBook Calendar { get; private set; }
        public DataObjectViewModel CalendarViewModel
        {
            get
            {
                return DataObjectViewModel.Fetch(ViewModelFactory, DataContext, Parent, Calendar);
            }
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
            if (_selectedInputViewModel != null)
            {
                return _selectedInputViewModel.CreateNew();
            }
            return null;
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
                    Task.Run(async () => _NewCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.new_png.Find(FrozenContext)));

                }
                return _NewCommand;
            }
        }

        public bool CanNew()
        {
            if(SelectedInputViewModel == null) return false;
            if (!SelectedInputViewModel.IsValid)
            {
                SelectedInputViewModel.Validate();
            }
            return SelectedInputViewModel.IsValid;
        }

        public string CanNewReason()
        {
            if(SelectedInputViewModel == null) return CommonCommandsResources.DataObjectCommand_NothingSelected;
            if (!SelectedInputViewModel.IsValid) return SelectedInputViewModel.ValidationError.ToString();

            return CommonCommandsResources.DataObjectCommand_ProgrammerError;
        }

        public void New()
        {
            if (SelectedInputViewModel != null) SelectedInputViewModel.Validate();
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
