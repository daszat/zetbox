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
using System.Linq;
using System.Text;
using Zetbox.API;

namespace Zetbox.Client.Presentables.GUI
{
    [ViewModelDescriptor]
    public class MultiLineEditorDialogViewModel
        : WindowViewModel
    {
        private Action<string> _callback;

        public new delegate MultiLineEditorDialogViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            string value,
            Action<string> callback);

        public MultiLineEditorDialogViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            string value,
            Action<string> callback)
            : base(appCtx, dataCtx, parent)
        {
            this._callback = callback;
            this._value = value;
        }

        public override string Name
        {
            get { return MultiLineEditorDialogViewModelResources.Name; }
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
                    _OKCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this,
                        MultiLineEditorDialogViewModelResources.OKCommand_Name,
                        MultiLineEditorDialogViewModelResources.OKCommand_Tooltip, 
                        Ok,
                        null, null);
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
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        MultiLineEditorDialogViewModelResources.CancelCommand_Name,
                        MultiLineEditorDialogViewModelResources.CancelCommand_Tooltip,
                        Cancel,
                        null, 
                        null);
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
