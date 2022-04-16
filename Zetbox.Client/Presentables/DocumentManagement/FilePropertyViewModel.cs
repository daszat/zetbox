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

namespace Zetbox.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using at.dasz.DocumentManagement;
    using Zetbox.App.Extensions;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class FilePropertyViewModel : ObjectReferenceViewModel
    {
        public new delegate FilePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public FilePropertyViewModel(
           IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
           IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        #region DragDrop
        public override async Task<bool> OnDrop(object data)
        {
            if (data is string)
            {
                var str = (string)data;
                if (System.IO.File.Exists(str))
                {
                    await UploadFile(str);
                    return true;
                }
            }
            else if (data is string[])
            {
                var str = ((string[])data).FirstOrDefault();
                if (str != null && System.IO.File.Exists(str))
                {
                    await UploadFile(str);
                    return true;
                }
            }
            return await base.OnDrop(data);
        }

        public async Task UploadFile(string path)
        {
            var vmdl = (FileViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this.GetWorkspace(), DataContext.Create<File>());
            await vmdl.Upload(path);
            Value = vmdl;
        }
        #endregion
    }
}
