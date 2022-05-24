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
    using System.Drawing;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
    public class ImageViewModel : FileViewModel
    {
        public new delegate ImageViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj);

        public ImageViewModel(
            IViewModelDependencies appCtx, ZetboxConfig config, IZetboxContext dataCtx, ViewModel parent,
            File obj)
            : base(appCtx, config, dataCtx, parent, obj)
        {

        }

        private Image _image = null;
        public Image Image
        {
            get
            {
                Task.Run(async () => await GetImage());
                return _image;
            }
        }

        public async Task<Image> GetImage()
        {
            if (_image == null && File.Blob != null)
            {
                _image = Image.FromStream(await File.Blob.GetStream());
            }
            OnPropertyChanged(nameof(Image));
            return _image;
        }
    }
}
