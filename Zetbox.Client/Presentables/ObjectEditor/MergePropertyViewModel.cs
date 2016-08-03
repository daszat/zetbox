namespace Zetbox.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class MergePropertyViewModel : ViewModel
    {
        public new delegate MergePropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, BaseValueViewModel targetProp, BaseValueViewModel sourceProp);

        public MergePropertyViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, BaseValueViewModel targetProp, BaseValueViewModel sourceProp)
            : base(appCtx, dataCtx, parent)
        {
            if (targetProp == null) throw new ArgumentNullException("targetProp");
            if (sourceProp == null) throw new ArgumentNullException("sourceProp");

            Target = targetProp;
            Source = sourceProp;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
            {
                // Reminder: if listener where implemented, remove them here
                Target = null;
                Source = null;
            }
        }

        public BaseValueViewModel Target { get; private set; }
        public BaseValueViewModel Source { get; private set; }

        private bool _usingSource = false;
        public bool UsingSource
        {
            get
            {
                return _usingSource;
            }
            set
            {
                if(_usingSource != value)
                {
                    _usingSource = value;
                    OnPropertyChanged("UsingSource");
                }
            }
        }

        private bool _usingTarget = false;
        public bool UsingTarget
        {
            get
            {
                return _usingTarget;
            }
            set
            {
                if (_usingTarget != value)
                {
                    _usingTarget = value;
                    OnPropertyChanged("UsingTarget");
                }
            }
        }

        #region UseSource command
        private ICommandViewModel _UseSourceCommand = null;
        public ICommandViewModel UseSourceCommand
        {
            get
            {
                if (_UseSourceCommand == null)
                {
                    _UseSourceCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Use source", "Use the source value", UseSource, null, null);
                    _UseSourceCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.back_png.Find(FrozenContext));
                }
                return _UseSourceCommand;
            }
        }

        public void UseSource()
        {
            Target.ValueModel.SetUntypedValue(Source.ValueModel.GetUntypedValue());
            UsingSource = true;
            UsingTarget = false;
        }
        #endregion

        #region UseTarget command
        private ICommandViewModel _UseTargetCommand = null;
        public ICommandViewModel UseTargetCommand
        {
            get
            {
                if (_UseTargetCommand == null)
                {
                    _UseTargetCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Use target", "Use the target value", UseTarget, null, null);
                    _UseTargetCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.ok_png.Find(FrozenContext));
                }
                return _UseTargetCommand;
            }
        }

        public void UseTarget()
        {
            // Nothing to do

            UsingSource = false;
            UsingTarget = true;
        }
        #endregion

        #region MergeValues command
        private ICommandViewModel _MergeValuesCommand = null;
        public ICommandViewModel MergeValuesCommand
        {
            get
            {
                if (_MergeValuesCommand == null)
                {
                    _MergeValuesCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, "Merge values", "Merges source and target values", MergeValues, 
                        CanMergeValues,
                        CanMergeValuesReason);
                    _MergeValuesCommand.Icon = IconConverter.ToImage(NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext));
                }
                return _MergeValuesCommand;
            }
        }

        private bool CanMergeValues()
        {
            return Target is StringValueViewModel;
        }

        private string CanMergeValuesReason()
        {
            return "Only strings can be merged";
        }

        public void MergeValues()
        {
            if (!CanMergeValues()) return;

            ((StringValueViewModel)Target).Value += ", " + ((StringValueViewModel)Source).Value;

            UsingSource = true;
            UsingTarget = true;
        }
        #endregion

        public override string Name
        {
            get { return Target.Label; }
        }
    }
}
