
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
        public new delegate ExceptionReporterViewModel Factory(IKistlContext dataCtx, Exception ex, Bitmap screenShot);

        private readonly Exception exeption;
        private readonly Bitmap screenShot;
        private readonly IProblemReporter problemReporter;

        public ExceptionReporterViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, Exception ex, Bitmap screenShot, IProblemReporter problemReporter)
            : base(appCtx, dataCtx)
        {
            if (problemReporter == null) throw new ArgumentNullException("problemReporter");

            this.exeption = ex;
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
                if (exeption != null)
                {
                    return exeption.ToString();
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
                if (exeption != null)
                {
                    return exeption.GetInnerMessage();
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

        private ClassValueModel<string> _AdditionalTextModel = new ClassValueModel<string>(ExceptionReporterViewModelResources.AdditionalTextLabel, ExceptionReporterViewModelResources.AdditionalTextDescription, true, false);
        private MultiLineStringValueViewModel _AdditionalTextViewModel;
        public MultiLineStringValueViewModel AdditionalText
        {
            get
            {
                if(_AdditionalTextViewModel == null)
                {
                    _AdditionalTextViewModel = ViewModelFactory.CreateViewModel<MultiLineStringValueViewModel.Factory>().Invoke(DataContext, _AdditionalTextModel);
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
                        Report, null);
                    _ReportCommand.Icon = FrozenContext.FindPersistenceObject<Kistl.App.GUI.Icon>(NamedObjects.Icon_todo_png);
                }
                return _ReportCommand;
            }
        }

        public void Report()
        {
            problemReporter.Report(this.Title, this.AdditionalText.Value, screenShot, exeption);
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
                        Cancel, null);
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
