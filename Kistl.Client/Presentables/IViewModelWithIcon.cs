using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.Presentables
{
    public interface IViewModelWithIcon
    {
        Kistl.App.GUI.Icon Icon { get; }
    }
}
