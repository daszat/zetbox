// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Zetbox.App.Base;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.Forms.View
{
    public partial class NullablePropertyTextBoxView : NullablePropertyTextBoxViewDesignerProxy
    {
        public NullablePropertyTextBoxView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged()
        {
            base.OnDataContextChanged();
            ConfigureControl();
            FetchValue();
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnModelPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case "Label":
                    ConfigureControl();
                    break;
                case "Value":
                    FetchValue();
                    break;
            }
        }

        private void ConfigureControl()
        {
            _label.Text = DataContext.Label;
        }

        private void FetchValue()
        {
            _valueBox.Text = DataContext.Value;
        }

        private void PushValue()
        {
            _valueBox.Text = DataContext.Value;
        }

        private void _valueBox_TextChanged(object sender, EventArgs e)
        {
            PushValue();
        }
    }

    public class NullablePropertyTextBoxViewDesignerProxy : FormsUserControl<IValueViewModel<String>> { }
}
