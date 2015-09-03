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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.API.Configuration;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables.ZetboxBase;

namespace Zetbox.Client.Presentables.ObjectBrowser
{
    [ViewModelDescriptor]
    public class WorkspaceViewModel
        : WindowViewModel
    {
        public new delegate WorkspaceViewModel Factory(IZetboxContext dataCtx, ViewModel parent);
        private ZetboxConfig _cfg;

        public WorkspaceViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, ZetboxConfig cfg)
            : base(appCtx, dataCtx, parent)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");
            _cfg = cfg;
        }

        #region Data

        /// <summary>
        /// A collection of all <see cref="Module"/>s, to display as entry 
        /// point into the object hierarchy
        /// </summary>
        public ObservableCollection<ModuleViewModel> Modules
        {
            get
            {
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<ModuleViewModel>();
                    LoadModules();
                }
                return _modulesCache;
            }
        }
        private ObservableCollection<ModuleViewModel> _modulesCache;

        /// <summary>
        /// A collection of all applications, to display as entry 
        /// points.
        /// </summary>
        public ObservableCollection<ApplicationViewModel> Applications
        {
            get
            {
                if (_applicationsCache == null)
                {
                    _applicationsCache = new ObservableCollection<ApplicationViewModel>();
                    foreach (var app in FrozenContext.GetQuery<Zetbox.App.GUI.Application>())
                    {
                        _applicationsCache.Add(ViewModelFactory.CreateViewModel<ApplicationViewModel.Factory>().Invoke(DataContext, null, app));
                    }
                }
                return _applicationsCache;
            }
        }
        private ObservableCollection<ApplicationViewModel> _applicationsCache;

        private ViewModel _selectedItem;
        /// <summary>
        /// The last selected ViewModel.
        /// </summary>
        public ViewModel SelectedItem
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
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        #endregion

        #region Utilities and UI callbacks

        private void LoadModules()
        {
            var modules = DataContext.GetQuery<Module>().ToList();
            foreach (var m in modules.OrderBy(i => i.Name))
            {
                Modules.Add(ViewModelFactory.CreateViewModel<ModuleViewModel.Factory>(m).Invoke(DataContext, this, m));
            }
        }

        public string AppsMenuItemString
        {
            get
            {
                return WorkspaceViewModelResources.Apps;
            }
        }

        public string RootNodeNameAsString
        {
            get
            {
                return WorkspaceViewModelResources.Browse;
            }
        }

        #endregion

        public override string Name
        {
            get
            {
                return string.Format("{0} - {1}", WorkspaceViewModelResources.Name, _cfg.ConfigName);
            }
        }

        #region Import command
        private ICommandViewModel _ImportCommand = null;
        public ICommandViewModel ImportCommand
        {
            get
            {
                if (_ImportCommand == null)
                {
                    _ImportCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Import", "Import zetbox objects", Import, CanImport, CanImportReason);
                }
                return _ImportCommand;
            }
        }

        public bool CanImport()
        {
            return true;
        }

        public string CanImportReason()
        {
            return "";
        }

        public void Import()
        {
            if (!CanImport()) return;
            var filename = ViewModelFactory.GetSourceFileNameFromUser("XML|*.xml", "All files|*.*");
            if (!string.IsNullOrWhiteSpace(filename))
            {
                var newScope = ViewModelFactory.CreateNewScope();
                var newCtx = newScope.ViewModelFactory.CreateNewContext();
                var objects = new List<IDataObject>();

                try
                {
                    objects.AddRange(Zetbox.App.Packaging.Importer.LoadFromXml(newCtx, filename).OfType<IDataObject>());
                }
                catch (Exception ex)
                {
                    // not an xml...
                    Zetbox.API.Utils.Logging.Client.Error("Unable to import file.", ex);
                    ViewModelFactory.ShowMessage("Unable to import file: " + ex.Message, "Error");
                }

                if (objects.Count > 0)
                {
                    var newWorkspace = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
                    newScope.ViewModelFactory.ShowModel(newWorkspace, true);

                    foreach (var obj in objects)
                    {
                        newWorkspace.ShowObject(obj, activate: false);
                    }

                    newScope.ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
                    {
                        newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                    }).Trigger();
                }
                else
                {
                    newScope.Dispose();
                }

            }
        }
        #endregion

        public bool CanDrop { get { return true; } }

        public bool OnDrop(object data)
        {
            var files = data as string[];
            if (files == null) return false;

            var newScope = ViewModelFactory.CreateNewScope();
            var newCtx = newScope.ViewModelFactory.CreateNewContext();
            var objects = new List<IDataObject>();

            foreach (var file in files)
            {
                try
                {
                    var xml = new System.Xml.XmlDocument();
                    xml.Load(file);
                    if (xml.DocumentElement.LocalName == "ZetboxPackaging")
                    {
                        objects.AddRange(Zetbox.App.Packaging.Importer.LoadFromXml(newCtx, file).OfType<IDataObject>());
                    }
                }
                catch (Exception ex)
                {
                    // not an xml...
                    Zetbox.API.Utils.Logging.Client.Error("Unable to import file.", ex);
                }
            }

            if (objects.Count > 0)
            {
                var newWorkspace = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
                newScope.ViewModelFactory.ShowModel(newWorkspace, true);

                foreach (var obj in objects)
                {
                    newWorkspace.ShowObject(obj, activate: false);
                }

                newScope.ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
                {
                    newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                }).Trigger();
            }
            else
            {
                newScope.Dispose();
            }
            return true;
        }
    }
}
