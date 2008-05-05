using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Kistl.API;
using Kistl.API.Client;
using Kistl.GUI.DB;
using Kistl.Client;

namespace Kistl.GUI.Renderer.WPF
{
    public class Renderer : BasicRenderer<Control, Control, ContentControl>
    {

        protected override Control Setup(Control control)
        {
            return (Control)control;
        }

        protected override ContentControl Setup(ContentControl widget, IList<Control> list)
        {
            StackPanel p = new StackPanel();
            foreach (var c in list)
            {
                p.Children.Add(c);
            }
            widget.Content = p;
            return widget;
        }

        public override void ShowObject(Kistl.API.IDataObject obj)
        {
            var template = obj.FindTemplate(TemplateUsage.EditControl);
            var widget = (System.Windows.Window)Manager.Renderer.CreateControl(obj, template.VisualTree);
            widget.Show();
        }

        public override void ShowMessage(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }
    }
}
