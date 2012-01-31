
namespace Kistl.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Threading;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Client.PerfCounter;
    using Kistl.API.Configuration;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.WPF.Toolkit;
    using Microsoft.Win32;

    public class WpfModelFactory
        : ViewModelFactory
    {
        private readonly IUiThreadManager uiThread;

        public WpfModelFactory(Autofac.ILifetimeScope container, IUiThreadManager uiThread, IFrozenContext frozenCtx, KistlConfig cfg, IPerfCounter perfCounter)
            : base(container, frozenCtx, cfg, perfCounter)
        {
            this.uiThread = uiThread;
        }

        /// <inheritdoc/>
        public override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.WPF; }
        }

        /// <summary>
        /// Explicit shutdown counter
        /// </summary>
        private int windowCounter = 0;

        private List<Window> _windowList = new List<Window>();

        /// <inheritdoc/>
        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
            uiThread.Verify();

            var window = view as Window;

            if (window != null)
            {
                if (_windowList.Contains(window))
                {
                    window.Activate();
                }
                else
                {
                    window.DataContext = mdl;
                    window.ShowActivated = activate;
                    if (asDialog)
                    {
                        window.ShowDialog();
                    }
                    else
                    {
                        _windowList.Add(window);
                        window.Closed += new EventHandler(window_Closed);

                        window.Show();
                        windowCounter++;
                    }
                }
            }
            else
            {
                // TODO: what should be done here, really?
                throw new NotImplementedException(String.Format("Cannot show view of type {0}, it's not a Window", view == null ? "(null)" : view.GetType().ToString()));
            }
        }

        /// <summary>
        /// Implemented explicit application shutdown
        /// </summary>
        /// <remarks>
        /// In BasicAuth scenarios the Password Dialog is shown first and closed before the 
        /// Main Application Window is created. This will let WPF think that the Application
        /// should shut down.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void window_Closed(object sender, EventArgs e)
        {
            _windowList.Remove(sender);
            if (--windowCounter == 0)
            {
                Application.Current.Shutdown();
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

        protected override object CreateDefaultView(ViewModel mdl)
        {
            System.Windows.Controls.Control view = (System.Windows.Controls.Control)base.CreateDefaultView(mdl);
            if (view != null) view.DataContext = mdl;
            return view;
        }

        protected override object CreateSpecificView(ViewModel mdl, Kistl.App.GUI.ControlKind kind)
        {
            System.Windows.Controls.Control view = (System.Windows.Controls.Control)base.CreateSpecificView(mdl, kind);
            if (view != null) view.DataContext = mdl;
            return view;
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
        /// <inheritdoc/>
        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            var dialog = new SaveFileDialog()
            {
                CheckFileExists = false,
                CheckPathExists = false,
                DereferenceLinks = true,
                Filter = String.Join("|", filter),
                ValidateNames = true,
                FileName = filename
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

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public override void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        public override IDelayedTask CreateDelayedTask(ViewModel displayer, Action loadAction)
        {
            return new WpfDelayedTask(displayer, loadAction);
        }
    }
}
