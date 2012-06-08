
namespace Kistl.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Dtos;

    public abstract class DtoBaseViewModel : Kistl.Client.Presentables.ViewModel
    {
        private readonly IFileOpener _fileOpener;
        private readonly ITempFileService _tmpService;

        public DtoBaseViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService, object debugInfo)
            : base(dependencies, dataCtx, parent)
        {
            var dtoParent = parent as DtoBaseViewModel;
            if (dtoParent != null)
            {
                _background = dtoParent.Background;
            }
            _fileOpener = fileOpener;
            _tmpService = tmpService;
            this._debugInfo = debugInfo;
        }

        private string _title = String.Empty;
        public override string Name
        {
            get { return Title; }
        }
        public string Title
        {
            get
            {
                return _title;
            }
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

        private string _description = String.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _background = String.Empty;
        public string Background
        {
            get
            {
                return string.IsNullOrEmpty(_background)
                    ? "#00000000" // thank you, Wpf
                    : _background;
            }
            set
            {
                if (_background != value)
                {
                    _background = value;
                    OnPropertyChanged("Background");
                }
            }
        }

        private Formatting _formatting;
        public Formatting Formatting
        {
            get
            {
                return _formatting;
            }
            set
            {
                if (_formatting != value)
                {
                    _formatting = value;
                    OnPropertyChanged("Formatting");
                }
            }
        }

        private ICommandViewModel _printCommand;
        public ICommandViewModel PrintCommand
        {
            get
            {
                if (_printCommand == null)
                {
                    _printCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, this,
                        "Drucken", "Drucken",
                        Print,
                        () => this.IsPrintableRoot,
                        null);

                }
                return _printCommand;
            }
        }

        public void Print()
        {
            var printer = new DtoPrinter(_fileOpener, _tmpService);
            printer.PrintAsList(this);
        }

        private bool _isPrintableRoot;
        public bool IsPrintableRoot
        {
            get
            {
                return _isPrintableRoot;
            }
            set
            {
                if (_isPrintableRoot != value)
                {
                    _isPrintableRoot = value;
                    OnPropertyChanged("IsPrintableRoot");
                }
            }
        }

        private object _debugInfo;
        /// <summary>
        /// A opaque object carrying debug information.
        /// </summary>
        public object DebugInfo
        {
            get
            {
                return _debugInfo;
            }
            set
            {
                if (_debugInfo != value)
                {
                    _debugInfo = value;
                    OnPropertyChanged("DebugInfo");
                }
            }
        }

        private object _root;
        public object Root
        {
            get
            {
                return _root;
            }
            set
            {
                if (_root != value)
                {
                    _root = value;
                    OnPropertyChanged("Root");
                }
            }
        }

        private object _data;
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        protected internal virtual void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            if (otherBase == null) throw new ArgumentNullException("otherBase");

            this.Background = otherBase.Background;
            this.Data = otherBase.Data;
            this.DebugInfo = otherBase.DebugInfo;
            this.Description = otherBase.Description;
            this.Formatting = otherBase.Formatting;
            this.IsPrintableRoot = otherBase.IsPrintableRoot;
            this.Root = otherBase.Root;
            this.Title = otherBase.Title;
        }
    }
}
