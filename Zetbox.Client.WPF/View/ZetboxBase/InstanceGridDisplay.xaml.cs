
namespace Zetbox.Client.WPF.View.ZetboxBase
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
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ZetboxBase;
    using Microsoft.Windows.Controls;
    using Zetbox.Client.WPF.CustomControls;

    /// <summary>
    /// Shows all instances of a given DataTypeViewModel
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class InstanceGridDisplay
        : InstanceGridBaseDisplay
    {
        /// <summary>
        /// Initializes a new instance of the ObjectClassDisplay class.
        /// </summary>
        public InstanceGridDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public override ZetboxDataGrid DataGrid
        {
            get { return lst; }
        }
    }
}
