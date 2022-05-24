// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class AnyReferencePropertyViewModel : CompoundObjectPropertyViewModel, IOpenCommandParameter
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
                var obj = Object != null ? Object.GetObject(DataContext).Result : null;
                if (obj == null) return null;
                return DataObjectViewModel.Fetch(ViewModelFactory, DataContext, GetWorkspace(), obj);
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
        protected override async Task<System.Collections.ObjectModel.ObservableCollection<ICommandViewModel>> CreateCommands()
        {
            var cmds = await base.CreateCommands();
            cmds.Add(OpenReferenceCommand);
            cmds.Add(SelectValueCommand);
            cmds.Add(ClearValueCommand);
            return cmds;
        }

        public void OpenReference()
        {
            if (OpenReferenceCommand.CanExecute(null))
                OpenReferenceCommand.Execute(null);
        }

        private OpenDataObjectCommand _openReferenceCommand;
        public ICommandViewModel OpenReferenceCommand
        {
            get
            {
                if (_openReferenceCommand == null)
                {
                    _openReferenceCommand = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _openReferenceCommand;
            }
        }

        public Task SelectValue()
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
                        selectionTask.ListViewModel.AllowOpen = false;
                        selectionTask.ListViewModel.AllowAddNew = true;
                        OnDataObjectSelectionTaskCreated(selectionTask);

                        ViewModelFactory.ShowDialog(selectionTask);
                    }
                },
                null);
            ViewModelFactory.ShowDialog(selectClass);
            return Task.CompletedTask;
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
                        SelectValue,
                        () => Task.FromResult(AllowSelectValue && !IsReadOnly),
                        null);
                    Task.Run(async () => _SelectValueCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.search_png.Find(FrozenContext)));
                }
                return _SelectValueCommand;
            }
        }
        #endregion

        #region Name
        public override string ToString()
        {
            return Name;
        }

        protected override string FormatValue(CompoundObjectViewModel value)
        {
            return Name;
        }
        #endregion

        #region IOpenCommandParameter members
        bool IOpenCommandParameter.AllowOpen { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return ReferencedObject == null ? null : new[] { ReferencedObject }; } }
        #endregion

        #region DragDrop
        public virtual bool CanDrop
        {
            get
            {
                return !IsReadOnly;
            }
        }

        public virtual Task<bool> OnDrop(object data)
        {
            if (data is IDataObject[])
            {
                var lst = (IDataObject[])data;
                var obj = lst.First();
                if (obj.Context != DataContext)
                {
                    if (obj.ObjectState == DataObjectState.New) return Task.FromResult(false);
                    obj = DataContext.Find(DataContext.GetInterfaceType(obj), obj.ID);
                }
                Object.SetObject(obj);
                NotifyValueChanged();
            }
            return Task.FromResult(false);
        }

        public virtual async Task<object> DoDragDrop()
        {
            var obj = await Object.GetObject(DataContext);
            if (obj != null && obj.ObjectState.In(DataObjectState.Unmodified, DataObjectState.Modified, DataObjectState.New))
            {
                return new IDataObject[] { obj };
            }
            return null;
        }
        #endregion
    }
}
