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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using System.Drawing;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class ExceptionReporterViewModel
        : WindowViewModel
    {
        public new delegate ExceptionReporterViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Exception ex, Bitmap screenShot);

        private readonly Exception exception;
        private readonly Bitmap screenShot;
        private readonly IProblemReporter problemReporter;

        public ExceptionReporterViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Exception ex, Bitmap screenShot, IProblemReporter problemReporter)
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
                    _ReportCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.todo_png.Find(FrozenContext));
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
                    _CancelCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.no_png.Find(FrozenContext));
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            this.Show = false;
        }
        #endregion

        public void ShowDialog()
        {
            ViewModelFactory.ShowDialog(this);
        }
    }
}
