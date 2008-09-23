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
using System.Collections.ObjectModel;
using Kistl.API;
using System.ComponentModel;

namespace Kistl.Client.WPF.Debugger
{
    /// <summary>
    /// Interaktionslogik für KistlContextDebuggerWPF.xaml
    /// </summary>
    public partial class KistlContextDebuggerWPF : Window, IKistlContextDebugger
    {
        public class ContextNode : INotifyPropertyChanged
        {
            public ContextNode(IKistlContext ctx)
            {
                Context = ctx;
                Objects = new ObservableCollection<IPersistenceObject>();
            }

            public IKistlContext Context { get; set; }
            public ObservableCollection<IPersistenceObject> Objects { get; private set; }

            public string Text
            {
                get
                {
                    return string.Format("Context: {0} Objects", Objects.Count);
                }
            }

            public override string ToString()
            {
                return Text;
            }

            public void NotifyChange()
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        private ObservableCollection<ContextNode> _contextList = new ObservableCollection<ContextNode>();

        public KistlContextDebuggerWPF()
        {
            InitializeComponent();
            this.DataContext = _contextList;
        }

        public static void ShowDebugger()
        {
            KistlContextDebuggerWPF debugger = new KistlContextDebuggerWPF();
            KistlContextDebugger.SetDebugger(debugger);
            debugger.Show();
        }

        private ContextNode ContainsContext(IKistlContext ctx)
        {
            return _contextList.SingleOrDefault(n => n.Context == ctx);
        }

        #region IKistlContextDebugger Members

        public void Created(IKistlContext ctx)
        {
            if (ContainsContext(ctx) == null)
            {
                _contextList.Add(new ContextNode(ctx));
            }
        }

        public void Disposed(IKistlContext ctx)
        {
            ContextNode n = ContainsContext(ctx);
            if (n != null)
            {
                _contextList.Remove(n);
            }
        }

        public void Changed(IKistlContext ctx)
        {
            //ContextNode n = ContainsContext(ctx);
            //if (n == null)
            //{
            //    n = new ContextNode(ctx);
            //    _contextList.Add(n);
            //}
            //var added = ctx.AttachedObjects.Except(n.Objects).ToList();
            //var deleted = n.Objects.Except(ctx.AttachedObjects).ToList();

            //deleted.ForEach(d => n.Objects.Remove(d));
            //added.ForEach(a => n.Objects.Add(a));

            //n.NotifyChange();
        }

        #endregion

        public void Dispose()
        {
            this.Close();
        }
    }
}
