using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            RecentObjects = new ObservableCollection<DataObjectModel>();
        }

        #region Public interface

        /// <summary>
        /// A list of "active" <see cref="IDataObjects"/>
        /// </summary>
        public ObservableCollection<DataObjectModel> RecentObjects { get; private set; }

        /// <summary>
        /// registers a user contact with the mdl in this <see cref="WorkspaceModel"/>'s history
        /// </summary>
        /// <param name="mdl"></param>
        public void HistoryTouch(DataObjectModel mdl)
        {
            // fetch old SelectedItem to reestablish selection after modifying RecentObjects
            var item = SelectedItem;
            if (!RecentObjects.Contains(mdl))
            {
                RecentObjects.Add(mdl);
            }
            // reestablish selection 
            SelectedItem = item;
        }

        /// <summary>
        /// A collection of all <see cref="Module"/>s, to display as entry 
        /// point into the object hierarchy
        /// </summary>
        public ObservableCollection<ModuleModel> Modules
        {
            get
            {
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<ModuleModel>();
                    LoadModules();
                }
                return _modulesCache;
            }
        }
        private ObservableCollection<ModuleModel> _modulesCache;

        private SaveContextCommand _saveCommand;
        /// <summary>
        /// This command submits all outstanding changes of this Workspace to the data store.
        /// The parameter has to be <value>null</value>.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = Factory.CreateSpecificModel<SaveContextCommand>(DataContext);

                return _saveCommand;
            }
        }

        private DataObjectModel _selectedItem;
        public DataObjectModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    if (_selectedItem != null)
                        HistoryTouch(_selectedItem);
                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("CreateNewInstanceEnabled");
                }
            }
        }

        public bool CreateNewInstanceEnabled { get { return SelectedItem != null && SelectedItem.Object is ObjectClass; } }
        public void CreateNewInstance()
        {
            var obj = SelectedItem;
            var objClass = (ObjectClass)obj.Object;
            var created = (IDataObject)DataContext.Create(objClass.GetDescribedInterfaceType());
            SelectedItem = (DataObjectModel)Factory.CreateDefaultModel(DataContext, created);
        }

        #endregion

        #region Utilities and UI callbacks

        private void LoadModules()
        {
            var modules = DataContext.GetQuery<Module>().ToList();
            foreach (var m in modules)
            {
                Modules.Add((ModuleModel)Factory.CreateDefaultModel(DataContext, m));
            }
        }

        #endregion

    }

    public class SaveContextCommand : CommandModel
    {
        public SaveContextCommand(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            Label = "Save";
            ToolTip = "Commits outstanding changes to the data store.";
        }

        public override bool CanExecute(object data)
        {
            return data == null;
        }

        public override void Execute(object data)
        {
            Executing = true;
            DataContext.SubmitChanges();
            Executing = false;
        }

    }

}
