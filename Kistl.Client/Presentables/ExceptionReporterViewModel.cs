
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
using System.Drawing;
using Kistl.Client.Models;
using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class ExceptionReporterViewModel
        : WindowViewModel
    {
        public new delegate ExceptionReporterViewModel Factory(IKistlContext dataCtx, ViewModel parent, Exception ex, Bitmap screenShot);

        private readonly Exception exception;
        private readonly Bitmap screenShot;
        private readonly IProblemReporter problemReporter;

        public ExceptionReporterViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, Exception ex, Bitmap screenShot, IProblemReporter problemReporter)
            : base(appCtx, dataCtx, parent)
        {
            if (problemReporter == null) throw new ArgumentNullException("problemReporter");

            this.exception = ex;
            this.screenShot = screenShot;
            this.problemReporter = problemReporter;
        }

        public override string Name
        {
            get { return ExceptionReporterViewModelResources.Name; }
        }

        public string Title
        {
            get { return ExceptionReporterViewModelResources.Name; }
        }

        public string HelpText
        {
            get { return ExceptionReporterViewModelResources.HelpText; }
        }

        public string ExceptionDetailsLabel
        {
            get { return ExceptionReporterViewModelResources.ExceptionDetailsLabel; } 
        }

        public string ExceptionText
        {
            get
            {
                if (exception != null)
                {
                    return exception.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string ExceptionMessage
        {
            get
            {
                if (exception != null)
                {
                    return exception.GetInnerMessage();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public Bitmap Screenshot
        {
            get
            {
                return screenShot;
            }
        }

        private ClassValueModel<string> _subjectTextModel = new ClassValueModel<string>(ExceptionReporterViewModelResources.SummaryLabel, ExceptionReporterViewModelResources.SummaryDescription, false, false);
        private ClassValueViewModel<string> _subjectTextViewModel;
        public ClassValueViewModel<string> SubjectText
        {
            get
            {
                if (_subjectTextViewModel == null)
                {
                    _subjectTextViewModel = ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(DataContext, this, _subjectTextModel);
                    _subjectTextModel.Value = exception != null ? exception.GetInnerMessage() : String.Empty;
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
                if(_AdditionalTextViewModel == null)
                {
                    _AdditionalTextViewModel = ViewModelFactory.CreateViewModel<MultiLineStringValueViewModel.Factory>().Invoke(DataContext, this, _AdditionalTextModel);
                }
                return _AdditionalTextViewModel;
            }
        }

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
                    _ReportCommand.Icon = FrozenContext.FindPersistenceObject<Kistl.App.GUI.Icon>(NamedObjects.Icon_todo_png);
                }
                return _ReportCommand;
            }
        }

        public void Report()
        {
            problemReporter.Report(
                string.IsNullOrEmpty(this.SubjectText.Value) ? this.Title : this.SubjectText.Value, 
                this.AdditionalText.Value, 
                screenShot, 
                exception);
            this.Show = false;
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
                    _CancelCommand.Icon = FrozenContext.FindPersistenceObject<Kistl.App.GUI.Icon>(NamedObjects.Icon_no_png);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            this.Show = false;
        }
        #endregion
    }
}
