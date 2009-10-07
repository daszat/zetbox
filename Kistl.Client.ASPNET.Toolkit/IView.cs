using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Client.ASPNET.Toolkit
{
    public interface IView
    {
        void SetModel(Presentables.PresentableModel mdl);
    }
}
