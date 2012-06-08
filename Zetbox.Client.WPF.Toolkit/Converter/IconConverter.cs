
namespace Zetbox.Client.WPF.Converter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Extensions;
    using System.Windows.Media.Imaging;
    using Zetbox.API.Utils;

    [ValueConversion(typeof(object), typeof(BitmapImage))]
    public class IconConverter : IValueConverter
    {
        private readonly IFrozenContext FrozenContext;
        private readonly Func<IZetboxContext> ctxFactroy;
        private readonly Dictionary<Guid, BitmapImage> _cache = new Dictionary<Guid, BitmapImage>();

        public IconConverter(IFrozenContext frozenCtx, Func<IZetboxContext> ctx)
        {
            this.FrozenContext = frozenCtx;
            this.ctxFactroy = ctx;
        }

        private bool _initialized = false;
        public void Initialized()
        {
            _initialized = true;
        }

        private IZetboxContext _Context;
        private IZetboxContext Context
        {
            get
            {
                if (_Context == null && _initialized)
                {
                    _Context = ctxFactroy();
                }
                return _Context;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!_initialized) return Binding.DoNothing;
            try
            {
                Zetbox.App.GUI.Icon icon = null;
                if (value is Zetbox.App.Base.ObjectClass)
                {
                    Zetbox.App.Base.ObjectClass objClass = (Zetbox.App.Base.ObjectClass)value;
                    icon = objClass.DefaultIcon;
                }
                else if (value is Zetbox.App.GUI.Icon)
                {
                    icon = (Zetbox.App.GUI.Icon)value;
                }
                else if (value is IDataObject)
                {
                    IDataObject obj = (IDataObject)value;
                    icon = obj.GetObjectClass(FrozenContext).DefaultIcon;
                }
                else if (value is Zetbox.Client.Presentables.ViewModel)
                {
                    icon = ((Zetbox.Client.Presentables.ViewModel)value).Icon;
                }

                if (icon == null)
                {
                    return Binding.DoNothing;
                }
                else
                {
                    // Not initialized yet
                    if (icon.ObjectState == DataObjectState.New) return Binding.DoNothing;

                    BitmapImage bmp;
                    if (!_cache.TryGetValue(icon.ExportGuid, out bmp))
                    {
                        var realIcon = Context.FindPersistenceObject<Zetbox.App.GUI.Icon>(icon.ExportGuid);
                        if (realIcon.Blob == null)
                        {
                            Logging.Log.WarnFormat("Icon#{0} has no associated request", realIcon.ID);
                            return Binding.DoNothing;
                        }
                        bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.StreamSource = realIcon.Blob.GetStream();
                        bmp.EndInit();
                        _cache[icon.ExportGuid] = bmp;
                    }
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                Logging.Facade.Info("Error while loading Icon", ex);
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // not implemented
            return Binding.DoNothing;
        }
    }
}
