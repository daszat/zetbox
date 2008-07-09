using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Kistl.API;
using Kistl.API.Client;
using Kistl.GUI.DB;
using Kistl.Client;
using Kistl.Client.WPF.Dialogs;

namespace Kistl.GUI.Renderer.WPF
{
    public class Renderer : BasicRenderer<Control, Control, ContentControl>
    {
        public override Toolkit Platform { get { return Toolkit.WPF; } }

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
            WorkspaceWindow w = WorkspaceWindow.FindWindow(obj.Context);
            if (w == null)
            {
                w = new WorkspaceWindow();
                w.SetContext(obj.Context);
            }
            w.Objects.Add(obj);
            w.Show();

            /*
            var template = obj.FindTemplate(TemplateUsage.EditControl);
            var widget = (System.Windows.Window)Manager.Renderer.CreateControl(obj, template.VisualTree);
            widget.Show();
             */
        }

        public override void ShowMessage(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }

        public override T ChooseObject<T>(IKistlContext ctx)
        {
            return (T)ChooseObject(ctx, typeof(T));
        }

        public override IDataObject ChooseObject(IKistlContext ctx, Type klass)
        {
            ChooseObjectDialog chooseDlg = new ChooseObjectDialog();
            chooseDlg.ObjectType = klass;
            chooseDlg.Context = ctx;
            if (chooseDlg.ShowDialog() == true)
            {
                return chooseDlg.Result;
            }
            else
            {
                return null;
            }
        }
    }
}
