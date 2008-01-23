using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.GUI
{
    public class CustomClientActions_GUI
    {
        public void OnToString_Icon(Kistl.App.GUI.Icon obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.IconFile;
        }
    }
}
