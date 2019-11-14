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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.App.Test;
    using Zetbox.Client.GUI;
    using System.Collections.ObjectModel;

    [ViewModelDescriptor]
    public class TestObjClassViewModel
        : DataObjectViewModel
    {
        public new delegate TestObjClassViewModel Factory(IZetboxContext dataCtx, ViewModel parent, TestObjClass obj);

        public TestObjClassViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            TestObjClass obj)
            : base(appCtx, dataCtx, parent, obj)
        {
        }

        protected override ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var list = base.CreateCommands();

            list.Add(AddTabCommand);

            return list;
        }


        #region AddTab command
        private ICommandViewModel _AddTabCommand = null;
        public ICommandViewModel AddTabCommand
        {
            get
            {
                if (_AddTabCommand == null)
                {
                    _AddTabCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Add tab", "Add a new Tab", AddTab, CanAddTab, CanAddTabReason);
                }
                return _AddTabCommand;
            }
        }

        public bool CanAddTab()
        {
            return true;
        }

        public string CanAddTabReason()
        {
            return "TODO: Add error string here";
        }

        public void AddTab()
        {
            if (!CanAddTab()) return;

            var key = $"New Tab {PropertyGroups.Count + 1}";
            var tab = UICreator.CustomPropertyGroup(key, key, new[]
            {
                UICreator.StackPanel(new []
                {
                    UICreator.GroupBox("Grp 1", new []
                    {
                        PropertyModelsByName["StringProp"],
                        PropertyModelsByName["MyIntProperty"],
                        PropertyModelsByName["ObjectProp"],
                    }),
                    UICreator.GroupBox("Grp 2", new []
                    {
                        PropertyModelsByName["StringProp"],
                        PropertyModelsByName["TestEnumProp"],
                        PropertyModelsByName["TestEnumWithDefault"],
                    }),
                }),
            });
            PropertyGroups.Add(tab);
            SelectedPropertyGroup = tab;
        }
        #endregion

        protected override List<PropertyGroupViewModel> CreatePropertyGroups()
        {
            var result = base.CreatePropertyGroups();

            result.Add(
                UICreator.CustomPropertyGroup("Stack 1", "Stack 1", new[]
                {
                    UICreator.StackPanel(new []
                    {
                        UICreator.GroupBox("Grp 1", new ViewModel[]
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["MyIntProperty"],
                            PropertyModelsByName["ObjectProp"],
                            UICreator.ObjectReferencePresenter(PropertyModelsByName["ObjectProp"], NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DataObjectKind.Find(FrozenContext)),
                        }),
                        UICreator.GroupBox("Grp 2", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        }),
                    }),
                })
            );

            result.Add(
                UICreator.CustomPropertyGroup("Grid 1", "Grid 1", new[]
                {
                    UICreator.Grid(new []
                    {
                        new GridPanelViewModel.Cell(0, 0, UICreator.GroupBox("Grp 1", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["MyIntProperty"],
                            PropertyModelsByName["ObjectProp"],
                            
                        })),
                        new GridPanelViewModel.Cell(0, 1, UICreator.GroupBox("Grp 2", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        })),
                    }),
                })
            );

            result.Add(
                UICreator.CustomPropertyGroup("Dock 1", "Dock 1", new[] 
                {
                    UICreator.DockPanel(new []
                    {
                        new DockPanelViewModel.Cell(DockPanelViewModel.Dock.Left, UICreator.GroupBox("Left 1", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["MyIntProperty"],
                            PropertyModelsByName["ObjectProp"],
                        })),
                        new DockPanelViewModel.Cell(DockPanelViewModel.Dock.Top, UICreator.GroupBox("Top 2", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        })),
                        new DockPanelViewModel.Cell(DockPanelViewModel.Dock.Fill, UICreator.GroupBox("Fill 3", new []
                        {
                            PropertyModelsByName["StringProp"],
                            PropertyModelsByName["TestEnumProp"],
                            PropertyModelsByName["TestEnumWithDefault"],
                        })),
                    }),
                })
           );

            return result;
        }

        protected override PropertyGroupViewModel CreatePropertyGroup(string tag, string translatedTag, PropertyGroupCollection lst)
        {
            return base.CreatePropertyGroup(tag, translatedTag, lst);
        }
    }
}
