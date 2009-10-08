using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit
{
    public interface IView
    {
        void SetModel(PresentableModel mdl);
        PresentableModel GetModel();
    }
}
