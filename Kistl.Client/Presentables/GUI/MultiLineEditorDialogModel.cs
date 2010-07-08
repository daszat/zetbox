using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.Client.Presentables.GUI
{
    [ViewModelDescriptor("GUI", DefaultKind = "Kistl.App.GUI.MultiLineEditorDialog", Description = "ViewModel for displaying a multiline string editor dialog")]
    public class MultiLineEditorDialogModel
        : ViewModel
    {
        private Action<string> _callback;

        public new delegate MultiLineEditorDialogModel Factory(IKistlContext dataCtx,
            string value,
            Action<string> callback);

        public MultiLineEditorDialogModel(
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
            get { return "MultiLineEditorDialogModel"; }
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

        private ICommand _OKCommand = null;
        public ICommand OKCommand
        {
            get
            {
                if (_OKCommand == null)
                {
                    _OKCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "OK", "OK", () => Ok(), null);
                }
                return _OKCommand;
            }
        }

        public void Ok()
        {
            _callback(_value);
            Show = false;
        }

        private ICommand _CancelCommand = null;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ModelFactory.CreateViewModel<SimpleCommandModel.Factory>().Invoke(DataContext, "Cancel", "Cancel", () => Cancel(), null);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            Show = false;
        }

        private bool _show = true;
        public bool Show
        {
            get { return _show; }
            private set { _show = value; OnPropertyChanged("Show"); }
        }
    }
}
