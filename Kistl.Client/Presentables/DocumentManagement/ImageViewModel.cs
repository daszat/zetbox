namespace Kistl.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using at.dasz.DocumentManagement;
    using Kistl.API;
    using Kistl.API.Configuration;
    using System.Drawing;

    [ViewModelDescriptor]
    public class ImageViewModel : FileViewModel
    {
        public new delegate ImageViewModel Factory(IKistlContext dataCtx, IDataObject obj);

        public ImageViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            File obj)
            : base(appCtx, config, dataCtx, obj)
        {

        }

        private Image _image = null;
        public Image Image
        {
            get
            {
                if (_image == null && File.Blob != null)
                {
                    _image = Image.FromStream(File.Blob.GetStream());
                }
                return _image;
            }
        }
    }
}
