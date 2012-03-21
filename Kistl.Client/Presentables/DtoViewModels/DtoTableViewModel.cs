
namespace Kistl.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Dtos;

    public class DtoTableViewModel : DtoGroupedViewModel
    {
        public DtoTableViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IFileOpener fileOpener, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, debugInfo)
        {
            Rows = new ObservableCollection<DtoRowViewModel>();
            Columns = new ObservableCollection<DtoColumnViewModel>();
            Cells = new ObservableCollection<DtoCellViewModel>();
        }

        public ObservableCollection<DtoRowViewModel> Rows { get; private set; }
        public ObservableCollection<DtoColumnViewModel> Columns { get; private set; }
        public ObservableCollection<DtoCellViewModel> Cells { get; private set; }

        private bool _isDataTable;
        /// <summary>
        /// Specifies whether this is showing a table of data values or "just" for arranging other view models.
        /// </summary>
        public bool IsDataTable
        {
            get
            {
                return _isDataTable;
            }
            set
            {
                if (_isDataTable != value)
                {
                    _isDataTable = value;
                    OnPropertyChanged("IsDataTable");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoTableViewModel;

            DtoBuilder.Merge(this.Rows, other.Rows);
            DtoBuilder.Merge(this.Columns, other.Columns);
            DtoBuilder.Merge(this.Cells, other.Cells);
        }
    }

    public class DtoRowViewModel : DtoBaseViewModel
    {
        public DtoRowViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, DtoTableViewModel parent, IFileOpener fileOpener, int rowIdx, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, debugInfo)
        {
            this.Parent = parent;
            this._row = rowIdx;
        }

        public new DtoTableViewModel Parent { get; private set; }

        private int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            private set
            {
                if (_row != value)
                {
                    _row = value;
                    OnPropertyChanged("Row");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoRowViewModel;

            //other.Parent is left behind
            //this.Parent == other.Parent;
            this.Row = other.Row;
        }
    }

    public class DtoColumnViewModel : DtoBaseViewModel
    {
        public DtoColumnViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, DtoTableViewModel parent, IFileOpener fileOpener, int columnIdx, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, debugInfo)
        {
            this.Parent = parent;
            this._column = columnIdx;
        }

        public new DtoTableViewModel Parent { get; private set; }
        private int _column;
        public int Column
        {
            get
            {
                return _column;
            }
            private set
            {
                if (_column != value)
                {
                    _column = value;
                    OnPropertyChanged("Column");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoColumnViewModel;

            //other.Parent is left behind
            //this.Parent == other.Parent;
            this.Column = other.Column;
        }
    }

    public class DtoCellViewModel : DtoBaseViewModel
    {
        public DtoCellViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, DtoTableViewModel parent, IFileOpener fileOpener, DtoRowViewModel row, DtoColumnViewModel column, GuiGridLocationAttribute location, ViewModel value, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, debugInfo)
        {
            this.Parent = parent;
            this.Row = row;
            this.Column = column;
            this._location = location;
            this._value = value;
        }

        public new DtoTableViewModel Parent { get; private set; }
        public DtoRowViewModel Row { get; private set; }
        public DtoColumnViewModel Column { get; private set; }

        private GuiGridLocationAttribute _location;
        public GuiGridLocationAttribute Location
        {
            get
            {
                return _location;
            }
            private set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private ViewModel _value;
        public ViewModel Value
        {
            get
            {
                return _value;
            }
            private set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoCellViewModel;

            //other.Parent is left behind
            //this.Parent == other.Parent;
            //other.Row is left behind
            //this.Row == other.Row;
            //other.Column is left behind
            //this.Column == other.Column;
            this.Location = other.Location;
            var thisDtoViewModel = this.Value as DtoBaseViewModel;
            var otherDtoViewModel = other.Value as DtoBaseViewModel;
            if (thisDtoViewModel != null && otherDtoViewModel != null)
            {
                this.Value = DtoBuilder.Combine(thisDtoViewModel, otherDtoViewModel);
            }
            else
            {
                this.Value = other.Value;
            }
        }
    }
}
