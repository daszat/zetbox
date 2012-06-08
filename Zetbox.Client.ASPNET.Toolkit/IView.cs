using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Toolkit
{
    public interface IView
    {
        void SetModel(ViewModel mdl);
        ViewModel GetModel();
    }
}
