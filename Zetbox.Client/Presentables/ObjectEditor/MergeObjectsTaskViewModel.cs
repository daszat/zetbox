namespace Zetbox.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [ViewModelDescriptor]
    public class MergeObjectsTaskViewModel : ViewModel
    {
        public new delegate MergeObjectsTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject target, IDataObject source);

        public MergeObjectsTaskViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, IDataObject target, IDataObject source)
            : base(appCtx, dataCtx, parent)
        {
            if (target == null && source == null) throw new ArgumentException("Either source or target must not be null");

            ObjectClass = (source ?? target).GetObjectClass(FrozenContext);

            _targetMdl = new ObjectReferenceValueModel(MergeObjectsTaskViewModelResources.Target, MergeObjectsTaskViewModelResources.Target_Tooltip, false, false, ObjectClass);
            _targetMdl.Value = target;
            _targetMdl.PropertyChanged += _mdl_PropertyChanged;

            _sourceMdl = new ObjectReferenceValueModel(MergeObjectsTaskViewModelResources.Source, MergeObjectsTaskViewModelResources.Source_Tooltip, false, false, ObjectClass);
            _sourceMdl.Value = source;

            var ws = GetWorkspace() as IContextViewModel;
            if(ws == null)
            {
                throw new InvalidOperationException("A MergeObjectsTaskViewModel must be bound to a IContextViewModel workspace");
            }

            ws.Saving += OnSaving;
            ws.Saved += OnSaved;
        }

        private ObjectReferenceValueModel _targetMdl = null;
        private ObjectReferenceValueModel _sourceMdl = null;

        public ObjectClass ObjectClass { get; private set; }

        public override string Name
        {
            get { return MergeObjectsTaskViewModelResources.Name; }
        }

        void _mdl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Value")
            {
                ClearProperties();
            }
        }

        void OnSaving(object sender, EventArgs e)
        {
            // optional additional merge tasks
            var mergeable = _targetMdl.Value as IMergeable;
            if (mergeable != null)
            {
                mergeable.MergeFrom(_sourceMdl.Value);
            }

            // save the workspace
            // this will submit all local changes 
            // and prevent a re-save due to "ReplaceObject"
            DataContext.SubmitChanges();

            // Send replace request to the server
            // The replace task will run in a elevated context
            var objClass = DataContext.FindPersistenceObject<ObjectClass>(ObjectClass.ExportGuid); // call from our context
            objClass.ReplaceObject(_targetMdl.Value, _sourceMdl.Value);

            // As ReplaceObject is changing objects, those changes are serialized back.
            // But ReplaceObject is also submitting those changes, so we have to fix that here to prevent a re-save
            foreach (var obj in DataContext.AttachedObjects.Where(i => i.ObjectState == DataObjectState.Modified))
            {
                ((BasePersistenceObject)obj).SetUnmodified();
            }
        }

        void OnSaved(object sender, EventArgs e)
        {
            // Cleanup UI
            _sourceMdl.Value = null;
            ClearProperties();
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon ?? (base.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext)));
            }
            set
            {
                base.Icon = value;
            }
        }

        private ObjectReferenceViewModel _target = null;
        public ObjectReferenceViewModel Target
        {
            get
            {
                if(_target == null)
                {
                    _target = ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(DataContext, this, _targetMdl);
                }
                return _target;
            }
        }

        private ObjectReferenceViewModel _source = null;
        public ObjectReferenceViewModel Source
        {
            get
            {
                if (_source == null)
                {
                    _source = ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(DataContext, this, _sourceMdl);
                }
                return _source;
            }
        }

        private void ClearProperties()
        {
            if(_properties != null)
            {
                foreach(var p in _properties)
                {
                    p.Dispose();
                }
            }
            _properties = null;
            OnPropertyChanged("Properties");
        }

        private List<MergePropertyViewModel> _properties;
        public IEnumerable<MergePropertyViewModel> Properties
        {
            get
            {
                if (_properties == null && Target.Value != null && Source.Value != null)
                {
                    _properties = new List<MergePropertyViewModel>();

                    var target = Target.Value;
                    var source = Source.Value;
                    foreach(var p in target.PropertyModelsByName.Where(i => !i.Value.IsReadOnly))
                    {
                        var targetProp = p.Value;
                        var sourceProp = source.PropertyModelsByName[p.Key];

                        _properties.Add(ViewModelFactory.CreateViewModel<MergePropertyViewModel.Factory>().Invoke(DataContext, this, targetProp, sourceProp));
                    }

                    // optional add more properties/tasks
                    var mergeable = _targetMdl.Value as IMergeable;
                    if (mergeable != null)
                    {
                        mergeable.GetMergeableProperties(_properties);
                    }
                }
                return _properties;
            }
        }

        #region Swap command
        private ICommandViewModel _SwapCommand = null;
        public ICommandViewModel SwapCommand
        {
            get
            {
                if (_SwapCommand == null)
                {
                    _SwapCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        this, 
                        MergeObjectsTaskViewModelResources.SwapCommand, 
                        MergeObjectsTaskViewModelResources.SwapCommand_Tooltip, 
                        Swap, null, null);
                    _SwapCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.reload_png.Find(FrozenContext));
                }
                return _SwapCommand;
            }
        }

        public void Swap()
        {
            var tmp = _targetMdl.Value;
            _targetMdl.Value = _sourceMdl.Value;
            _sourceMdl.Value = tmp;

            ClearProperties();
        }
        #endregion

        #region Text
        public string TargetHelpText
        {
            get
            {
                return MergeObjectsTaskViewModelResources.Target_Tooltip;
            }
        }
        
        public string SourceHelpText
        {
            get
            {
                return MergeObjectsTaskViewModelResources.Source_Tooltip;
            }
        }
        #endregion
    }
}
