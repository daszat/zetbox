using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.GUI.DB;

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
            UI.Verify();
            // fetch old SelectedItem to reestablish selection after modifying RecentObjects
            var item = SelectedItem;
            if (RecentObjects.Contains(mdl))
            {
                RecentObjects.Remove(mdl);
            }
            RecentObjects.Add(mdl);
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
                UI.Verify();
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<ModuleModel>();
                    State = ModelState.Loading;
                    Async.Queue(DataContext, () =>
                    {
                        AsyncLoadModules();
                        UI.Queue(UI, () => this.State = ModelState.Active);
                    });
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
                UI.Verify();
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
                UI.Verify();
                return _selectedItem;
            }
            set
            {
                UI.Verify();
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
            UI.Verify();
            var obj = SelectedItem;
            Async.Queue(DataContext, () =>
            {
                var objClass = (ObjectClass)obj.Object;
                var created = (IDataObject)DataContext.Create(objClass.GetDataType());
                UI.Queue(UI, () => SelectedItem = (DataObjectModel)Factory.CreateDefaultModel(DataContext, created));
            });
        }

        #endregion

        #region Async handlers and UI callbacks

        private void AsyncLoadModules()
        {
            Async.Verify();
            var modules = DataContext.GetQuery<Module>().ToList();
            UI.Queue(UI, () =>
            {
                foreach (var m in modules)
                {
                    Modules.Add((ModuleModel)Factory
                        .CreateModel(
                            DataMocks.LookupDefaultModelDescriptor(m),
                            DataContext, new object[] { m }));
                }
                State = ModelState.Active;
            });
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
            UI.Verify();
            return data == null;
        }

        public override void Execute(object data)
        {
            UI.Verify();
            Executing = true;
            Async.Queue(DataContext, () =>
            {
                DataContext.SubmitChanges();
                UI.Queue(UI, () => Executing = false);
            });
        }

    }

}
