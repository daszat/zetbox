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

namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Zetbox.API.Client;
    using Zetbox.App.GUI;

    public class TreeItemIconInstanceListViewModel : TreeItemInstanceListViewModel
    {
        public new delegate TreeItemIconInstanceListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ObjectClass type, Func<IQueryable> qry, Module currentModule);

        public TreeItemIconInstanceListViewModel(
            IViewModelDependencies appCtx,
            ZetboxConfig config,
            IFileOpener fileOpener,
            ITempFileService tmpService,
            Lazy<IUIExceptionReporter> errorReporter,
            IZetboxContext dataCtx, ViewModel parent,
            ObjectClass type,
            Func<IQueryable> qry,
            Module currentModule)
            : base(appCtx, config, fileOpener, tmpService, errorReporter, dataCtx, parent, type, qry)
        {
            CurrentModule = currentModule;
        }

        public Module CurrentModule
        {
            get;
            private set;
        }

        public override bool CanDrop
        {
            get
            {
                return true;
            }
        }

        public override bool OnDrop(object data)
        {
            var files = data as string[];
            if (files == null) return base.OnDrop(data);

            var newScope = ViewModelFactory.CreateNewScope();
            var newCtx = newScope.ViewModelFactory.CreateNewContext();
            var module = newCtx.Find<Module>(CurrentModule.ID);
            var objects = new List<IDataObject>();
            
            foreach (var file in files)
            {
                try
                {
                    var ext = System.IO.Path.GetExtension(file).ToLower();
                    switch(ext)
                    {
                        case ".png":
                        case ".jpg":
                        case ".bmp":
                        case ".tiff":
                        case ".gif":
                        case ".ico":
                            var obj = newCtx.Create<Icon>();
                            var fi = new System.IO.FileInfo(file);
                            int id = newCtx.CreateBlob(fi, fi.GetMimeType());
                            obj.Blob = newCtx.Find<Zetbox.App.Base.Blob>(id);
                            obj.IconFile = obj.Blob.OriginalName;
                            obj.Module = module;
                            objects.Add(obj);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // not an xml...
                    Zetbox.API.Utils.Logging.Client.Error("Unable to import icon.", ex);
                }
            }

            if (objects.Count > 0)
            {
                var newWorkspace = ObjectEditor.WorkspaceViewModel.Create(newScope.Scope, newCtx);
                newScope.ViewModelFactory.ShowModel(newWorkspace, true);

                foreach (var obj in objects)
                {
                    newWorkspace.ShowObject(obj, activate: false);
                }

                newScope.ViewModelFactory.CreateDelayedTask(newWorkspace, () =>
                {
                    newWorkspace.SelectedItem = newWorkspace.Items.FirstOrDefault();
                }).Trigger();
            }
            else
            {
                newScope.Dispose();
            }

            return true;
        }
    }
}
