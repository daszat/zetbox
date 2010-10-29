using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables.GUI
{
    [ViewModelDescriptor]
    public class MultiLineEditorDialogViewModel
        : WindowViewModel
    {
        private Action<string> _callback;

        public new delegate MultiLineEditorDialogViewModel Factory(IKistlContext dataCtx,
            string value,
            Action<string> callback);

        public MultiLineEditorDialogViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string value,
            Action<string> callback)
            : base(appCtx, dataCtx)
        {
            this._callback = callback;
            this._value = value;
        }

        public override string Name
        {
            get { return "MultiLineEditorDialogViewModel"; }
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        private ICommandViewModel _OKCommand = null;
        public ICommandViewModel OKCommand
        {
            get
            {
                if (_OKCommand == null)
                {
                    _OKCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "OK", "OK", Ok, null);
                }
                return _OKCommand;
            }
        }

        public void Ok()
        {
            _callback(_value);
            Show = false;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, "Cancel", "Cancel", Cancel, null);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            Show = false;
        }
    }
}
