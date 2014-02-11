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
    using at.dasz.DocumentManagement;
    using Zetbox.API;
    using Zetbox.API.Configuration;

    [ViewModelDescriptor]
    public class FileViewModel : DataObjectViewModel
    {
        public new delegate FileViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public FileViewModel(
            IViewModelDependencies appCtx, ZetboxConfig config, IZetboxContext dataCtx, ViewModel parent,
            File obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.File = obj;
            
            // When the context was saved, no more changes are allowed.
            this.DataContext.IsModifiedChanged += new EventHandler(DataContext_IsModifiedChanged);
        }

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();
            // Changes should be possible when the object was not saved yet.
            UpdateIsReadonly();
        }

        void DataContext_IsModifiedChanged(object sender, EventArgs e)
        {
            if (DataContext.IsModified == false)
            {
                UpdateIsReadonly();
            }
        }

        protected void UpdateIsReadonly()
        {
            // Lock bool property
            base.PropertyModelsByName["IsFileReadonly"].IsReadOnly = File.IsFileReadonly;
        }

        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            return base.CreateCommands();
        }

        public File File { get; private set; }

        public bool CanUpload
        {
            get
            {
                return ActionViewModelsByName["Upload"].CanExecute(null);
            }
        }

        public void Upload(string path)
        {
            if (!string.IsNullOrEmpty(path) && CanUpload)
            {
                var fi = new System.IO.FileInfo(path);
                int id = DataContext.CreateBlob(fi, fi.GetMimeType());

                File.Blob = DataContext.Find<Zetbox.App.Base.Blob>(id);
                File.Name = File.Blob.OriginalName;
            }
        }
    }
}
