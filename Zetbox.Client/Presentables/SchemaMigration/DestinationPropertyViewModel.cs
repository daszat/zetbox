using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.Client.Models;
using Zetbox.App.Base;
using System.Collections.ObjectModel;
using Zetbox.App.SchemaMigration;
using Zetbox.Client.Presentables.ZetboxBase;

namespace Zetbox.Client.Presentables.SchemaMigration
{
    [ViewModelDescriptor]
    public class DestinationPropertyViewModel : Zetbox.Client.Presentables.ValueViewModels.ObjectListViewModel
    {
        public new delegate DestinationPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public DestinationPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IObjectCollectionValueModel<IList<IDataObject>> mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = new ObservableCollection<ICommandViewModel>();

            cmds.Add(SelectCommand);
            cmds.Add(ClearValueCommand);

            return cmds;
        }


        protected ObjectListPropertyValueModel PropertyValueModel
        {
            get
            {
                return base.ObjectCollectionModel as ObjectListPropertyValueModel;
            }
        }

        protected SourceColumn SourceColumn
        {
            get
            {
                return PropertyValueModel != null ? PropertyValueModel.Object as SourceColumn : null;
            }
        }


        public override string Name
        {
            get
            {
                if (Value == null || Value.Count == 0)
                {
                    return "[none]";
                }
                else
                {
                    return string.Join(".", Value.Select(i => ((Property)i.Object).Name).ToArray());
                }
            }
        }

        private ICommandViewModel _SelectCommand = null;
        public ICommandViewModel SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, null, "Select", "Select a destination property", Select, () => SourceColumn != null, null);
                }
                return _SelectCommand;
            }
        }

        public void Select()
        {
            var dlg = ViewModelFactory.CreateViewModel<PropertySelectionTaskViewModel.Factory>().Invoke(DataContext, Parent, SourceColumn.SourceTable.DestinationObjectClass, (result) =>
            {
                if (result != null)
                {
                    ValueModel.Value.Clear();
                    foreach (var i in result)
                    {
                        ValueModel.Value.Add(i);
                    }
                }
            });
            dlg.FollowCompundObjects = true;

            ViewModelFactory.ShowDialog(dlg);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case "Value":
                    OnPropertyChanged("Name");
                    break;
            }
        }
    }
}
