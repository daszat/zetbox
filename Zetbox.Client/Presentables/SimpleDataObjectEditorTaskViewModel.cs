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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class SimpleDataObjectEditorTaskViewModel
        : WindowViewModel
    {
        public new delegate SimpleDataObjectEditorTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            ViewModel obj);

        public SimpleDataObjectEditorTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ViewModel obj)
            : base(appCtx, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            this.Object = obj;
        }

        #region Commands
        private ICommandViewModel _CloseCommand = null;
        public ICommandViewModel CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, SimpleDataObjectEditorTaskViewModelResources.Close, SimpleDataObjectEditorTaskViewModelResources.Close_Tooltip, Close, null, null);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            base.Show = false;
        }
        #endregion

        #region Public interface

        public ViewModel Object
        {
            get; private set;
        }

        #endregion

        public override string Name
        {
            get { return string.Format(SimpleDataObjectEditorTaskViewModelResources.Name, Object.Name); }
        }
    }
}
