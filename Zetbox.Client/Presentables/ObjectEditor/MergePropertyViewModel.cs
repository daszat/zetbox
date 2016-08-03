namespace Zetbox.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

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

            if (disposing)
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
                if (_usingSource != value)
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
            if (Target is ObjectListViewModel)
            {
                var t = (ObjectListViewModel)Target;
                var s = (ObjectListViewModel)Source;
                var lst = s.Value.ToList(); // Duplicate list

                ClearList(t);
                ClearList(s); // Clear now - a later clear would destroy the others end pointer

                foreach (var obj in lst)
                {
                    t.Add(obj);
                }
            }
            else if (Target is ObjectCollectionViewModel)
            {
                var t = (ObjectCollectionViewModel)Target;
                var s = (ObjectCollectionViewModel)Source;
                var lst = s.Value.ToList();

                ClearCollection(t);
                ClearCollection(s);

                foreach (var obj in lst)
                {
                    t.Add(obj);
                }
            }
            else
            {
                Target.ValueModel.SetUntypedValue(Source.ValueModel.GetUntypedValue());
            }
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
            // Clear source lists to prevent merging during replace
            if (Source is ObjectListViewModel)
            {
                var s = (ObjectListViewModel)Source;
                ClearList(s);
            }
            if (Source is ObjectCollectionViewModel)
            {
                var s = (ObjectCollectionViewModel)Source;
                ClearCollection(s);
            }

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
            return Target is StringValueViewModel
                || Target is ObjectListViewModel
                || Target is ObjectCollectionViewModel;
        }

        private string CanMergeValuesReason()
        {
            return "Only strings and lists can be merged";
        }

        public void MergeValues()
        {
            if (!CanMergeValues()) return;

            if (Target is StringValueViewModel)
            {
                ((StringValueViewModel)Target).Value += ", " + ((StringValueViewModel)Source).Value;
            }
            else if (Target is ObjectListViewModel)
            {
                var t = (ObjectListViewModel)Target;
                var s = (ObjectListViewModel)Source;
                var lst = s.Value.ToList();

                ClearList(s);
                foreach (var obj in lst)
                {
                    t.Add(obj);
                }
                
            }
            else if (Target is ObjectCollectionViewModel)
            {
                var t = (ObjectCollectionViewModel)Target;
                var s = (ObjectCollectionViewModel)Source;
                var lst = s.Value.ToList();

                ClearCollection(s);
                foreach (var obj in lst)
                {
                    t.Add(obj);
                }
            }

            UsingSource = true;
            UsingTarget = true;
        }
        #endregion

        private void ClearCollection(ObjectCollectionViewModel s)
        {
            var mdl = s.ObjectCollectionModel;
            var rel = mdl.RelEnd.Parent;
            var otherEnd = rel.GetOtherEnd(mdl.RelEnd);
            if (otherEnd != null && otherEnd.Multiplicity.UpperBound() > 1 && rel.Containment != ContainmentSpecification.Independent)
            {
                foreach (var obj in mdl.Value.ToList())
                {
                    DataContext.Delete(obj);
                }
            }
            else
            {
                s.ClearValue();
            }
        }

        private void ClearList(ObjectListViewModel s)
        {
            var mdl = s.ObjectCollectionModel;
            var rel = mdl.RelEnd.Parent;
            var otherEnd = rel.GetOtherEnd(mdl.RelEnd);
            if (otherEnd != null && otherEnd.Multiplicity.UpperBound() > 1 && rel.Containment != ContainmentSpecification.Independent)
            {
                foreach (var obj in mdl.Value.ToList())
                {
                    DataContext.Delete(obj);
                }
            }
            else
            {
                s.ClearValue();
            }
        }

        public override string Name
        {
            get { return Target.Label; }
        }
    }
}
