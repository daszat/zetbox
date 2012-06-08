using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.Client.Presentables;

namespace Kistl.Client.Forms
{
    internal interface IFormsView
    {
        void SetRenderer(Renderer r);
        void SetDataContext(INotifyPropertyChanged mdl);
        void Activate();
        void Show();
        void ShowDialog();
    }
}
