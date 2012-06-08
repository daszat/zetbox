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
                if (_image == null && File.Blob != null)
                {
                    _image = Image.FromStream(File.Blob.GetStream());
                }
                return _image;
            }
        }
    }
}
