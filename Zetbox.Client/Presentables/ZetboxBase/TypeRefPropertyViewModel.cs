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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;

    public class TypeRefPropertyViewModel
           : ObjectReferenceViewModel
    {
        public new delegate TypeRefPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public TypeRefPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            // TODO: use a static reference here
            //if (prop.GetReferencedObjectClass().Name != "TypeRef")
            //{
            //    throw new ArgumentOutOfRangeException("prop", "Can only handle TypeRef References");
            //}
        }

        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            var cmds = base.CreateCommands();
            cmds.Add(LoadTypeRefFromAssemblyRefCommand);
            cmds.Add(LoadTypeRefFromAssemblyFileCommand);
            return cmds;
        }

        private LoadTypeRefFromAssemblyFileCommand _loadTypeRefFromAssemblyFileCommand = null;
        public ICommandViewModel LoadTypeRefFromAssemblyFileCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyFileCommand == null)
                {
                    _loadTypeRefFromAssemblyFileCommand = ViewModelFactory.CreateViewModel<LoadTypeRefFromAssemblyFileCommand.Factory>().Invoke(DataContext, this, this);
                }
                return _loadTypeRefFromAssemblyFileCommand;
            }
        }

        private LoadTypeRefFromAssemblyRefCommand _loadTypeRefFromAssemblyRefCommand = null;
        public ICommandViewModel LoadTypeRefFromAssemblyRefCommand
        {
            get
            {
                if (_loadTypeRefFromAssemblyRefCommand == null)
                {
                    _loadTypeRefFromAssemblyRefCommand = ViewModelFactory.CreateViewModel<LoadTypeRefFromAssemblyRefCommand.Factory>().Invoke(DataContext, this, this);
                }
                return _loadTypeRefFromAssemblyRefCommand;
            }
        }
    }
}