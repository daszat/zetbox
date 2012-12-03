namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.App.Base;

    [ViewModelDescriptor]
    public class AnyReferencePropertyViewModel : CompoundObjectPropertyViewModel
    {
        public new delegate AnyReferencePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public AnyReferencePropertyViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        protected override void NotifyValueChanged()
        {
            base.NotifyValueChanged();
            OnPropertyChanged("ReferencedObject");
        }

        public AnyReference Object
        {
            get
            {
                return (AnyReference)base.CompoundObjectModel.Value;
            }
        }

        public ViewModel ReferencedObject
        {
            get
            {
                var obj = Object != null ? Object.GetObject(DataContext) : null;
                if (obj == null) return null;
                return DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
            }
        }

        private bool _allowSelectValue = true;
        public bool AllowSelectValue
        {
            get
            {
                return _allowSelectValue;
            }
            set
            {
                if (_allowSelectValue != value)
                {
                    _allowSelectValue = value;
                    OnPropertyChanged("AllowSelectValue");
                }
            }
        }

        #region Commands
        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();
            cmds.Add(OpenReferenceCommand);
            cmds.Add(SelectValueCommand);
            cmds.Add(ClearValueCommand);
            return cmds;
        }

        private bool CanOpen
        {
            get
            {
                return ReferencedObject != null ? ViewModelFactory.CanShowModel(ReferencedObject) : false;
            }
        }

        public void OpenReference()
        {
            if (CanOpen)
                ViewModelFactory.ShowModel(ReferencedObject, true);
        }

        private ICommandViewModel _openReferenceCommand;
        public ICommandViewModel OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ObjectReferenceViewModelResources.OpenReferenceCommand_Name,
                        ObjectReferenceViewModelResources.OpenReferenceCommand_Tooltip,
                        () => OpenReference(),
                        () => CanOpen,
                        null);
                    _openReferenceCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext));
                }
                return _openReferenceCommand;
            }
        }

        public void SelectValue()
        {
            var selectClass = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                this,
                (ObjectClass)NamedObjects.Base.Classes.Zetbox.App.Base.ObjectClass.Find(FrozenContext),
                null,
                (chosenClass) =>
                {
                    if (chosenClass != null)
                    {
                        var cls = (ObjectClass)chosenClass.First().Object;
                        var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                            DataContext,
                            this,
                            cls,
                            null,
                            (chosen) =>
                            {
                                if (chosen != null)
                                {
                                    Object.SetObject(chosen.First().Object);
                                    NotifyValueChanged();
                                }
                            },
                            null);
                        selectionTask.ListViewModel.AllowDelete = false;
                        selectionTask.ListViewModel.ShowOpenCommand = false;
                        selectionTask.ListViewModel.AllowAddNew = true;
                        OnDataObjectSelectionTaskCreated(selectionTask);

                        ViewModelFactory.ShowDialog(selectionTask);
                    }
                },
                null);
            ViewModelFactory.ShowDialog(selectClass);
        }
        
        public event DataObjectSelectionTaskCreatedEventHandler DataObjectSelectionTaskCreated;
        protected virtual void OnDataObjectSelectionTaskCreated(DataObjectSelectionTaskViewModel vmdl)
        {
            var temp = DataObjectSelectionTaskCreated;
            if (temp != null)
            {
                temp(this, new DataObjectSelectionTaskEventArgs(vmdl));
            }
        }
        
        private ICommandViewModel _SelectValueCommand;

        public ICommandViewModel SelectValueCommand
        {
            get
            {
                if (_SelectValueCommand == null)
                {
                    _SelectValueCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ObjectReferenceViewModelResources.SelectValueCommand_Name,
                        ObjectReferenceViewModelResources.SelectValueCommand_Tooltip,
                        () => SelectValue(),
                        () => AllowSelectValue && !DataContext.IsReadonly && !IsReadOnly,
                        null);
                    _SelectValueCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.search_png.Find(FrozenContext));
                }
                return _SelectValueCommand;
            }
        }
        #endregion

        #region Name
        public override string Name
        {
            get { return ReferencedObject != null ? ReferencedObject.Name : string.Empty; }
        }

        public override string ToString()
        {
            return Name;
        }

        protected override string FormatValue(CompoundObjectViewModel value)
        {
            return Name;
        }
        #endregion
    }
}
