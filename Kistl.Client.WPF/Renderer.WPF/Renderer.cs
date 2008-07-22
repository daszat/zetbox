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

        protected override void ShowObject(Kistl.API.IDataObject obj, ContentControl ctrl)
        {
            WorkspaceWindow w = WorkspaceWindow.FindOrCreateWindow(obj.Context);
            w.ShowObject(obj, (ObjectTabItem)ctrl);
            w.Show();
        }

        public override void ShowMessage(string msg)
        {
            System.Windows.MessageBox.Show(msg);
        }

        public override IDataObject ChooseObject(IKistlContext ctx, Type klass, string prompt)
        {
            ChooseObjectDialog chooseDlg = new ChooseObjectDialog();
            chooseDlg.ObjectType = klass;
            chooseDlg.Context = ctx;
            chooseDlg.Title = prompt ?? "Choose object";
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
