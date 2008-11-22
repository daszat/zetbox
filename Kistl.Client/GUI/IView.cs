using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.GUI
{
    public interface IView
    {
        void SetModel(Presentables.PresentableModel mdl);
    }
}
