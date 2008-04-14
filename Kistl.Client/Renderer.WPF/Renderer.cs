using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Kistl.API;
using Kistl.GUI.DB;
using Kistl.API.Client;

namespace Kistl.GUI.Renderer.WPF
{
    public class Renderer : Kistl.GUI.Renderer.BasicRenderer<Control, Control, ContentControl>
    {
        private static Renderer _WPF = null;
        /// <summary>
        /// The currently active WPF renderer
        /// </summary>
        public static Renderer WPF
        {
            get
            {
                if (_WPF == null)
                    _WPF = new Renderer();
                return _WPF;
            }
        }

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
            var widget = (System.Windows.Window)Renderer.WPF.CreateControl(obj, template.VisualTree);
            widget.Show();
        }
    }
}
