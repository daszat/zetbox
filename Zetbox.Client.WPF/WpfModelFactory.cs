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

namespace Zetbox.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.WPF.Toolkit;
    using Microsoft.Win32;
    using System.Threading.Tasks;

    public class WpfModelFactory
        : ViewModelFactory
    {
        public WpfModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory)
            : base(scopeFactory, scope, frozenCtx, cfg, perfCounter, dialogFactory)
        {
        }

        /// <inheritdoc/>
        public override Zetbox.App.GUI.Toolkit Toolkit
        {
            get { return Zetbox.App.GUI.Toolkit.WPF; }
        }

        /// <summary>
        /// Explicit shutdown counter
        /// </summary>
        private static int windowCounter = 0;

        private Dictionary<ViewModel, Window> _windowList = new Dictionary<ViewModel, Window>();

        /// <inheritdoc/>
        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog, ViewModel ownerMdl)
        {
            var window = view as Window;

            if (window != null)
            {
                if (_windowList.ContainsValue(window))
                {
                    window.Activate();
                }
                else
                {
                    window.DataContext = mdl;
                    window.ShowActivated = activate;
                    if (asDialog)
                    {
                        if (ownerMdl != null && _windowList.ContainsKey(ownerMdl))
                        {
                            var owner = _windowList[ownerMdl];
                            if (owner != window)
                            {
                                window.Owner = owner;
                            }
                        }
                        try
                        {
                            _windowToActivate.Push(window);
                            window.Activated += new EventHandler(window_Activated);
                            window.ShowDialog();
                        }
                        finally
                        {
                            _windowToActivate.Pop();
                        }
                    }
                    else
                    {
                        _windowList.Add(mdl, window);
                        window.Closed += new EventHandler(window_Closed);
                        window.Activated += new EventHandler(window_Activated);

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

        Stack<Window> _windowToActivate = new Stack<Window>();
        void window_Activated(object sender, EventArgs e)
        {
            if (_windowToActivate.Count > 0 && _windowToActivate.Peek() != sender)
            {
                var wnd = _windowToActivate.Peek();
                if (wnd.WindowState == WindowState.Minimized)
                    wnd.WindowState = WindowState.Normal; // WTF??? http://stackoverflow.com/a/6837421/178517
                wnd.Activate();
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
            var wnd = (Window)sender;
            _windowList.Remove(_windowList.Single(kv => kv.Value == wnd).Key);
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
            System.Windows.Controls.Control view = (System.Windows.Controls.Control)(base.CreateDefaultView(mdl));
            if (view != null) view.DataContext = mdl;
            return view;
        }

        protected override object CreateSpecificView(ViewModel mdl, Zetbox.App.GUI.ControlKind kind)
        {
            System.Windows.Controls.Control view = (System.Windows.Controls.Control)(base.CreateSpecificView(mdl, kind));
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

        public override TaskScheduler UITaskScheduler => App.Current.TaskScheduler;
    }
}
