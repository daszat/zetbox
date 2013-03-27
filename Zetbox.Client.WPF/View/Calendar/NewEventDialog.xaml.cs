using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.Presentables.Calendar;
using Zetbox.Client.GUI;

namespace Zetbox.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for EventDialog.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NewEventDialog : WindowView, IHasViewModel<NewEventDialogViewModel>
    {
        public NewEventDialog()
        {
            InitializeComponent();
        }

        public NewEventDialogViewModel ViewModel
        {
            get { return (NewEventDialogViewModel)DataContext; }
        }
    }
}
