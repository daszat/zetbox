
namespace Kistl.Client.WPF.View.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.Client.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.KistlBase;

    /// <summary>
    /// Shows all instances of a given DataTypeViewModel
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class InstanceListDisplay
        : InstanceListBaseDisplay
    {
        /// <summary>
        /// Initializes a new instance of the ObjectClassDisplay class.
        /// </summary>
        public InstanceListDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public override ListView ListView
        {
            get { return lst; }
        }
    }
}
