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

    [ViewModelDescriptor]
    public class StaticFilePropertyViewModel : ObjectReferenceViewModel
    {
        public new delegate StaticFilePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public StaticFilePropertyViewModel(
           IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
           IValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
        }

        protected override DataObjectSelectionTaskViewModel CreateDataObjectSelectionTask()
        {
            var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                this,
                typeof(File).GetObjectClass(FrozenContext),
                null,
                (chosen) =>
                {
                    if (chosen != null)
                    {
                        var fileVmdl = chosen.First();
                        if (typeof(StaticFile).IsAssignableFrom(fileVmdl.Object.GetType()))
                        {
                            Value = fileVmdl;
                        }
                        else
                        {
                            var otherFile = (File)fileVmdl.Object;
                            var staticFile = DataContext.Create<StaticFile>();
                            staticFile.Name = otherFile.Name;
                            staticFile.Blob = otherFile.Blob;
                            Value = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this.GetWorkspace(), staticFile);
                        }
                    }
                },
                null);
            selectionTask.ListViewModel.AllowDelete = false;
            selectionTask.ListViewModel.AllowOpen = false;
            selectionTask.ListViewModel.AllowAddNew = AllowCreateNewItemOnSelect;
            return selectionTask;
        }

        #region DragDrop
        public override bool OnDrop(object data)
        {
            if (data is string)
            {
                var str = (string)data;
                if (System.IO.File.Exists(str))
                {
                    UploadFile(str);
                    return true;
                }
            }
            else if (data is string[])
            {
                var str = ((string[])data).FirstOrDefault();
                if (str != null && System.IO.File.Exists(str))
                {
                    UploadFile(str);
                    return true;
                }
            }
            return base.OnDrop(data);
        }

        public void UploadFile(string path)
        {
            var vmdl = (FileViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this.GetWorkspace(), DataContext.Create<StaticFile>());
            vmdl.Upload(path);
            Value = vmdl;
        }
        #endregion
    }
}
