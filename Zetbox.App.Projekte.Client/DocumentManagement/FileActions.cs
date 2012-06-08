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
namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    
    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class FileActions
    {
        private static IViewModelFactory _factory;

        public FileActions(IViewModelFactory factory)
        {
            _factory = factory;
        }

        [Invocation]
        public static void Open(File obj)
        {
            if (obj.Blob != null)
            {
                obj.Blob.Open();
            }
        }

        [Invocation]
        public static void Upload(File obj)
        {
            // UI Code in Custom Actions!
            // ASP.NET would have a big Problem with that function
            string path = _factory.GetSourceFileNameFromUser();
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new System.IO.FileInfo(path);
                int id = obj.Context.CreateBlob(fi, fi.GetMimeType());
                obj.Blob = obj.Context.Find<Zetbox.App.Base.Blob>(id);
                obj.Name = obj.Blob.OriginalName;
            }
        }
    }
}
