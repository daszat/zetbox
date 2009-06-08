
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Threading;

    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    
    using Microsoft.Win32;

    public class WpfModelFactory
        : ModelFactory
    {

        public WpfModelFactory(IGuiApplicationContext appCtx)
            : base(appCtx)
        {
        }

        /// <inheritdoc/>
        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.WPF; }
        }

        /// <inheritdoc/>
        protected override void ShowInView(PresentableModel mdl, IView view, bool activate)
        {
            AppContext.UiThread.Verify();

            if (view is Window)
            {
                var viewControl = (Window)view;
                viewControl.DataContext = mdl;
                viewControl.ShowActivated = activate;
                viewControl.Show();
            }
            else
            {
                // TODO: what should be done here, really?
                throw new NotImplementedException(String.Format("Cannot show view of type {0}", view.GetType()));
            }
        }

        /// <inheritdoc/>
        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = tickLength;
            timer.Tick += (obj, args) => action();
            timer.Start();
        }

        /// <inheritdoc/>
        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DereferenceLinks = true,
                Filter = String.Join("|", filter),
                Multiselect = false,
                ShowReadOnly = false,
                ValidateNames = true,
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else
            {
                return String.Empty;
            }
        }
    }

}
