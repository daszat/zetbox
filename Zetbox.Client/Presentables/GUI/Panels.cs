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

namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using System.Collections.ObjectModel;
    using Zetbox.Client.Presentables.ValueViewModels;

    public abstract class PanelViewModel : ViewModel
    {
        public new delegate PanelViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> children);

        private string _title;
        private readonly ObservableCollection<ViewModel> _children;

        protected PanelViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent)
        {
            this._children = new ObservableCollection<ViewModel>(children);
            _title = title;
        }

        public override string Name
        {
            get { return _title; }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Name");
                    OnPropertyChanged("Title");
                }
            }
        }

        public ObservableCollection<ViewModel> Children
        {
            get
            {
                return _children;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            foreach(var c in _children)
            {
                c.Dispose();
            }
        }
    }


    [ViewModelDescriptor]
    public class StackPanelViewModel : PanelViewModel
    {
        public new delegate StackPanelViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public StackPanelViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class DockPanelViewModel : PanelViewModel
    {
        public new delegate DockPanelViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Cell> children);

        public enum Dock
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3,
            Fill = 4,
        }

        public class Cell
        {
            public Cell()
            {
            }

            public Cell(Dock dock, ViewModel vmdl)
                : this()
            {
                this.Dock = dock;
                this.ViewModel = vmdl;
            }

            public Dock Dock { get; set; }
            public ViewModel ViewModel { get; set; }
        }

        private readonly ObservableCollection<Cell> _elements;

        public DockPanelViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Cell> children)
            : base(dependencies, dataCtx, parent, name, children == null ? null : children.Select(c => c.ViewModel))
        {
            _elements = new ObservableCollection<Cell>(children);
        }

        public ObservableCollection<Cell> Elements
        {
            get
            {
                return _elements;
            }
        }
    }

    [ViewModelDescriptor]
    public class GridPanelViewModel : PanelViewModel
    {
        public new delegate GridPanelViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Cell> children);

        public class Cell
        {
            public Cell()
            {
                RowSpan = 1;
                ColumnSpan = 1;
            }

            public Cell(int row, int col, ViewModel vmdl)
                : this()
            {
                this.Row = row;
                this.Column = col;
                this.ViewModel = vmdl;
            }

            public int Row { get; set; }
            public int Column { get; set; }
            public int RowSpan { get; set; }
            public int ColumnSpan { get; set; }
            public ViewModel ViewModel { get; set; }
        }

        public class Row
        {
            public int Index { get; set; }
        }

        public class Column
        {
            public int Index { get; set; }
        }

        private readonly ObservableCollection<Cell> _cells;

        public GridPanelViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<Cell> children)
            : base(dependencies, dataCtx, parent, name, children == null ? null : children.Select(c => c.ViewModel))
        {
            _cells = new ObservableCollection<Cell>(children);
        }

        public ObservableCollection<Cell> Cells
        {
            get
            {
                return _cells;
            }
        }

        public IEnumerable<Row> Rows
        {
            get
            {
                List<Row> result = new List<Row>();
                var max = _cells.Max(c => c.Row);
                for (int i = 0; i <= max; i++)
                {
                    result.Add(new Row() { Index = i });
                }
                return result;
            }
        }

        public IEnumerable<Column> Columns
        {
            get
            {
                List<Column> result = new List<Column>();
                var max = _cells.Max(c => c.Column);
                for (int i = 0; i <= max; i++)
                {
                    result.Add(new Column() { Index = i });
                }
                return result;
            }
        }
    }

    [ViewModelDescriptor]
    public class GroupBoxViewModel : PanelViewModel
    {
        public new delegate GroupBoxViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public GroupBoxViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class TabControlViewModel : PanelViewModel
    {
        public new delegate TabControlViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public TabControlViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class TabItemViewModel : PanelViewModel
    {
        public new delegate TabItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children);

        public TabItemViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, IEnumerable<ViewModel> children)
            : base(dependencies, dataCtx, parent, name, children)
        {
        }
    }

    [ViewModelDescriptor]
    public class PresenterViewModel : ViewModel
    {
        private ObjectReferenceViewModel _objRefVM;

        public new delegate PresenterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ViewModel viewModel, App.GUI.ControlKind controlKind);
        public PresenterViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, ViewModel viewModel, App.GUI.ControlKind controlKind) 
            : base(dependencies, dataCtx, parent)
        {
            ControlKind = controlKind;

            if(ViewModel is ObjectReferenceViewModel)
            {
                _objRefVM = (ObjectReferenceViewModel)viewModel;
                _objRefVM.PropertyChanged += ObjectReferenceViewModel_PropertyChanged;

                ViewModel = _objRefVM.Value;
            }
            else
            {
                ViewModel = viewModel;
            }
        }

        private void ObjectReferenceViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Value")
            {
                ViewModel = _objRefVM.Value;
                OnPropertyChanged("ViewModel");
            }
        }

        public override string Name => ViewModel.Name;

        public ViewModel ViewModel { get; private set; }
        public App.GUI.ControlKind ControlKind { get; }
    }
}
