﻿// This file is part of zetbox.
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zetbox.API;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.App.GUI;
using Zetbox.App.Test;
using Zetbox.Client.Models;
using Zetbox.Client.Presentables.GUI;
using Zetbox.Client.Presentables.ZetboxBase;

namespace Zetbox.Client.Presentables.TestModule
{
    [ViewModelDescriptor]
    public class InstanceListTestViewModel : NavigationScreenViewModel
    {
        public new delegate InstanceListTestViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        public InstanceListTestViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen)
            : base(appCtx, dataCtx, parent, screen)
        {
        }

        public enum ListKinds
        {
            List,
            Grid,
            HorizontalList,
            HorizontalGrid,
        }

        public ListKinds[] ListKindsSource
        {
            get
            {
                return new[] { ListKinds.List, ListKinds.Grid, ListKinds.HorizontalList, ListKinds.HorizontalGrid };
            }
        }

        private ListKinds _listKind = ListKinds.Grid;
        public ListKinds ListKind
        {
            get
            {
                return _listKind;
            }
            set
            {
                if (_listKind != value)
                {
                    _listKind = value;
                    _TestList = null;
                    OnPropertyChanged("ListKind");
                    OnPropertyChanged("TestList");
                }
            }
        }

        public InstanceListViewMethod[] ViewMethodsSource
        {
            get
            {
                return new[] { InstanceListViewMethod.Details, InstanceListViewMethod.List };
            }
        }

        private InstanceListViewModel _TestList = null;
        public InstanceListViewModel TestList
        {
            get
            {
                if (_TestList == null)
                {
                    var qry = DataContext
                        .GetQuery<TestCustomObject>()
                        .OrderBy(cls => cls.Birthday);

                    _TestList = ViewModelFactory.CreateViewModel<InstanceListViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        typeof(TestCustomObject).GetObjectClass(FrozenContext),
                        () => qry);
                    _TestList.ViewMethod = InstanceListViewMethod.Details;
                    _TestList.DisplayedColumnsCreated += new InstanceListViewModel.DisplayedColumnsCreatedHandler(_TestList_DisplayedColumnsCreated);
                    _TestList.IsInlineEditable = true;
                    _TestList.AllowAddNew = true;
                    _TestList.AllowDelete = true;

                    switch (ListKind)
                    {
                        case ListKinds.List:
                            _TestList.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceListKind.Find(FrozenContext);
                            break;
                        case ListKinds.Grid:
                            _TestList.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceGridKind.Find(FrozenContext);
                            break;
                        case ListKinds.HorizontalList:
                            _TestList.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceListHorizontalKind.Find(FrozenContext);
                            break;
                        case ListKinds.HorizontalGrid:
                            _TestList.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_InstanceGridHorizontalKind.Find(FrozenContext);
                            break;
                        default:
                            break;
                    }
                }
                return _TestList;
            }
        }

        Task _TestList_DisplayedColumnsCreated(Models.GridDisplayConfiguration cols)
        {
            //    cols.Columns.Add(ColumnDisplayModel.Create(NamedObjects.Base.Classes.Zetbox.App.Base.ObjectClass_Methods.GetInheritedMethods.Find(FrozenContext)));
            //    cols.Columns.Add(ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, NamedObjects.Base.Classes.Zetbox.App.Base.ObjectClass_Properties.IsAbstract.Find(FrozenContext)));
            //    cols.Columns.Add(ColumnDisplayModel.Create("IsFrozenObject ViewModel", NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(FrozenContext), "PropertyModelsByName[IsFrozenObject]"));
            return Task.CompletedTask;
        }
    }
}
