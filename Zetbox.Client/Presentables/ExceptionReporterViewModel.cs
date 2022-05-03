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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Utils;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    public interface IUIExceptionReporter
    {
        void BeginInit();
        void EndInit();
        void Show(Exception ex);
    }

    public class UIExceptionReporter : IUIExceptionReporter
    {
        public static readonly object SyncRoot = new object();

        private bool _uiIsInitialized = true;
        private readonly ILifetimeScope _scope;
        private IZetboxContext _ctx;
        private IViewModelFactory _vmf;
        private IScreenshotTool _screenshot;
        private ExceptionReporterViewModel _mdl;

        public UIExceptionReporter(ILifetimeScope scope)
        {
            if (scope == null) throw new ArgumentNullException("scope");

            _scope = scope;
        }

        public void Show(Exception ex)
        {
            lock (SyncRoot)
            {
                try
                {
                    Setup();

                    var inner = ex.StripTargetInvocationExceptions();
                    Logging.Client.Error("Unhandled Exception", inner);
                    if (inner is InvalidZetboxGeneratedVersionException)
                    {
                        _vmf.ShowMessage(
                            ExceptionReporterViewModelResources.InvalidZetboxGeneratedVersionException_Message,
                            ExceptionReporterViewModelResources.InvalidZetboxGeneratedVersionException_Title
                        );
                    }
                    else if (inner is ZetboxServerIOException)
                    {
                        _vmf.ShowMessage(
                            string.Format(ExceptionReporterViewModelResources.CommunicationError_MessageFormat, inner.Message),
                            ExceptionReporterViewModelResources.CommunicationError_Title
                        );
                    }
                    else if (_uiIsInitialized)
                    {
                        var screenShot = _screenshot.GetScreenshot();
                        if (_mdl == null)
                        {
                            _mdl = _vmf.CreateViewModel<ExceptionReporterViewModel.Factory>().Invoke(_ctx, null);
                            _mdl.AddException(ex, screenShot);
                            _vmf.ShowDialog(_mdl);
                        }
                        else if (_mdl.Show == false)
                        {
                            _mdl.AddException(ex, screenShot);
                            _vmf.ShowDialog(_mdl);
                        }
                        else
                        {
                            _mdl.AddException(ex, screenShot);
                        }
                    }
                    else
                    {
                        _vmf.ShowMessage(ex.ToString(), "Unexpected Error");
                    }
                }
                catch (Exception ex2)
                {
                    // uh oh!
                    Logging.Client.Error("Error while handling unhandled Exception", ex2);
                    _vmf.ShowMessage(ex.ToString(), "Unexpected Error");
                }
            }
        }

        private void Setup()
        {
            if (_ctx == null)
            {
                _ctx = _scope.Resolve<IZetboxContext>();
            }
            if (_vmf == null)
            {
                _vmf = _scope.Resolve<IViewModelFactory>();
            }
            if (_screenshot == null)
            {
                _screenshot = _scope.Resolve<IScreenshotTool>();
            }
        }

        public void BeginInit()
        {
            lock (SyncRoot) _uiIsInitialized = false;
        }
        public void EndInit()
        {
            lock (SyncRoot) _uiIsInitialized = true;
        }
    }

    [ViewModelDescriptor]
    public class ExceptionReporterViewModel
        : WindowViewModel
    {
        public new delegate ExceptionReporterViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        private List<Tuple<Exception, Bitmap>> _exceptions;

        private readonly IProblemReporter problemReporter;

        public ExceptionReporterViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, IProblemReporter problemReporter)
            : base(appCtx, dataCtx, parent)
        {
            if (problemReporter == null) throw new ArgumentNullException("problemReporter");

            this.problemReporter = problemReporter;
            this._exceptions = new List<Tuple<Exception, Bitmap>>();
        }

        #region Properties
        public override string Name
        {
            get
            {
                lock (UIExceptionReporter.SyncRoot)
                {
                    return string.Format(ExceptionReporterViewModelResources.Name, _exceptions.Count);
                }
            }
        }

        public string Title
        {
            get
            {
                return this.Name;
            }
        }

        public string HelpUsText
        {
            get { return ExceptionReporterViewModelResources.HelpUsText; }
        }

        public string ExceptionDetailsLabel
        {
            get { return ExceptionReporterViewModelResources.ExceptionDetailsLabel; }
        }

        public string ExceptionText
        {
            get
            {
                lock (UIExceptionReporter.SyncRoot)
                {
                    if (_exceptions.Count > 0)
                    {
                        return string.Join("\n\n", _exceptions.Select(e => e.ToString()));
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string ExceptionMessage
        {
            get
            {
                lock (UIExceptionReporter.SyncRoot)
                {
                    return _exceptions.Count > 0 ? _exceptions.First().Item1.GetInnerMessage() : string.Empty;
                }
            }
        }

        public Bitmap Screenshot
        {
            get
            {
                lock (UIExceptionReporter.SyncRoot)
                {
                    return _exceptions.Count > 0 ? _exceptions.First().Item2 : null;
                }
            }
        }

        private ClassValueModel<string> _subjectTextModel = new ClassValueModel<string>(ExceptionReporterViewModelResources.SummaryLabel, ExceptionReporterViewModelResources.SummaryDescription, false, false);
        private StringValueViewModel _subjectTextViewModel;
        public StringValueViewModel SubjectText
        {
            get
            {
                if (_subjectTextViewModel == null)
                {
                    _subjectTextViewModel = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, _subjectTextModel);
                }
                return _subjectTextViewModel;
            }
        }

        private ClassValueModel<string> _AdditionalTextModel = new ClassValueModel<string>(ExceptionReporterViewModelResources.AdditionalTextLabel, ExceptionReporterViewModelResources.AdditionalTextDescription, false, false);
        private MultiLineStringValueViewModel _AdditionalTextViewModel;
        public MultiLineStringValueViewModel AdditionalText
        {
            get
            {
                if (_AdditionalTextViewModel == null)
                {
                    _AdditionalTextViewModel = ViewModelFactory.CreateViewModel<MultiLineStringValueViewModel.Factory>().Invoke(DataContext, this, _AdditionalTextModel);
                }
                return _AdditionalTextViewModel;
            }
        }
        #endregion

        #region MultipleException

        [Serializable]
        public class MultipleException : Exception
        {
            IEnumerable<Exception> _exceptions;

            public MultipleException() { }
            public MultipleException(string message) : base(message) { }
            public MultipleException(string message, Exception inner) : base(message, inner) { }
            protected MultipleException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }

            public MultipleException(IEnumerable<Exception> exceptions)
            {
                _exceptions = exceptions;
                if (_exceptions == null)
                {
                    _exceptions = new List<Exception>();
                }
            }

            public IEnumerable<Exception> Exceptions
            {
                get
                {
                    return _exceptions;
                }
            }

            public override string Message
            {
                get
                {
                    return "Multiple exceptions has occured";
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine(Message + ":");
                sb.Append(string.Join("\n------------- Next -------------\n", _exceptions.Select(e => e.ToString())));
                return sb.ToString();
            }
        }
        #endregion

        #region Commands
        private ICommandViewModel _ReportCommand = null;
        public ICommandViewModel ReportCommand
        {
            get
            {
                if (_ReportCommand == null)
                {
                    _ReportCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        this,
                        ExceptionReporterViewModelResources.Report,
                        ExceptionReporterViewModelResources.Report_Tooltip,
                        Report, null, null);
                    Task.Run(async () => _ReportCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.todo_png.Find(FrozenContext)));
                }
                return _ReportCommand;
            }
        }

        public Task Report()
        {
            lock (UIExceptionReporter.SyncRoot)
            {
                if (_exceptions.Count > 0)
                {
                    Exception finalException;
                    if (_exceptions.Count == 1)
                    {
                        finalException = _exceptions.First().Item1;
                    }
                    else
                    {
                        finalException = new MultipleException(_exceptions.Select(e => e.Item1));
                    }
                    problemReporter.Report(
                        string.IsNullOrEmpty(this.SubjectText.Value) ? this.Title : this.SubjectText.Value,
                        this.AdditionalText.Value,
                        _exceptions.First().Item2,
                        finalException);
                    Clear();
                }
            }
            this.Show = false;

            return Task.CompletedTask;
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext,
                        null,
                        ExceptionReporterViewModelResources.Cancel,
                        ExceptionReporterViewModelResources.Cancel_Tooltip,
                        Cancel, null, null);
                    Task.Run(async () => _CancelCommand.Icon = await IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.no_png.Find(FrozenContext)));
                }
                return _CancelCommand;
            }
        }

        public Task Cancel()
        {
            this.Show = false;
            Clear();

            return Task.CompletedTask;
        }
        #endregion

        #region Management
        public void AddException(Exception ex, Bitmap bitmap)
        {
            lock (UIExceptionReporter.SyncRoot)
            {
                _exceptions.Add(new Tuple<Exception, Bitmap>(ex, bitmap));
            }
            OnPropertyChanged("Title");
            OnPropertyChanged("Name");
            OnPropertyChanged("ExceptionText");
            OnPropertyChanged("ExceptionMessage");
        }

        private void Clear()
        {
            lock (UIExceptionReporter.SyncRoot)
            {
                foreach (var ex in _exceptions.Where(e => e.Item2 != null))
                {
                    ex.Item2.Dispose();
                }
                _exceptions.Clear();
            }
        }

        protected override void OnClose()
        {
            base.OnClose();
            Clear();
        }
        #endregion
    }
}
