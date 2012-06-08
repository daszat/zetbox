using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Zetbox.Client.Presentables;

namespace Zetbox.Client.Forms
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
