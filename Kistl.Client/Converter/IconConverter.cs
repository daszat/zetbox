using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Kistl.API.Client;

namespace Kistl.Client.Converter
{
    [ValueConversion(typeof(BaseClientDataObject), typeof(string))]
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Kistl.App.Base.ObjectClass)
            {
                Kistl.App.Base.ObjectClass objClass = (Kistl.App.Base.ObjectClass)value;
                if (objClass.DefaultIcon != null)
                {
                    string result = Kistl.API.Configuration.KistlConfig.Current.Client.DocumentStore
                        + @"\GUI.Icons\"
                        + objClass.DefaultIcon.IconFile;
                    result = System.IO.Path.IsPathRooted(result) ? result : Environment.CurrentDirectory + "\\" + result;
                    return result;
                }
                else
                    return "";
            }
            else if (value is BaseClientDataObject)
            {
                BaseClientDataObject obj = (BaseClientDataObject)value;
                if (Helper.ObjectClasses[obj.Type].DefaultIcon != null)
                    return Kistl.API.Configuration.KistlConfig.Current.Client.DocumentStore
                        + @"\GUI.Icons\"
                        + Helper.ObjectClasses[obj.Type].DefaultIcon.IconFile;
                else
                    return "";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // not implemented
            return null;
        }
    }
}
