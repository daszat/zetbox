using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.App.GUI;

namespace Kistl.Client.Presentables
{
    public interface ILabeledViewModel : INotifyPropertyChanged
    {
        string Label { get; }
        string ToolTip { get; }
        ControlKind RequestedKind { get; }
        PresentableModel Model { get; }
    }
}
