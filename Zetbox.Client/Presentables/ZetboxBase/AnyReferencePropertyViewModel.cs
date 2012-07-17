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

        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();
            cmds.Add(OpenReferenceCommand);
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
                    _openReferenceCommand.Icon = Zetbox.NamedObjects.Gui.Icons.ZetboxBase.fileopen_png.Find(FrozenContext);
                }
                return _openReferenceCommand;
            }
        }

        public override string Name
        {
            get { return ReferencedObject != null ? ReferencedObject.Name : string.Empty; }
        }
    }
}
