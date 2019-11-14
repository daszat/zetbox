// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using Zetbox.API.Server;
    using Zetbox.DalProvider.Ef;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="AntwortEfImpl")]
    [System.Diagnostics.DebuggerDisplay("Antwort")]
    public class AntwortEfImpl : BaseServerDataObject_EntityFramework, Antwort
    {
        private static readonly Guid _objectClassID = new Guid("e5577f89-58d1-4983-ab35-60e6624780df");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public AntwortEfImpl()
            : base(null)
        {
        }

        public AntwortEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Frage
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Frage;
                if (OnFrage_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnFrage_Getter(this, __e);
                    __result = _Frage = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Frage != value)
                {
                    var __oldValue = _Frage;
                    var __newValue = value;
                    if (OnFrage_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnFrage_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Frage", __oldValue, __newValue);
                    _Frage = __newValue;
                    NotifyPropertyChanged("Frage", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnFrage_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnFrage_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Frage");
                }
            }
        }
        private string _Frage_store;
        private string _Frage {
            get { return _Frage_store; }
            set {
                ReportEfPropertyChanging("Frage");
                _Frage_store = value;
                ReportEfPropertyChanged("Frage");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, string> OnFrage_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFrage_IsValid;

        /// <summary>
        /// Workaround for Case 1376
        /// </summary>
    /*
    Relation: FK_Ein_Fragebogen_enthaelt_gute_Antworten
    A: One Fragebogen as Ein_Fragebogen
    B: ZeroOrMore Antwort as gute_Antworten
    Preferred Storage: MergeIntoB
    */
        // object reference property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
        // fkBackingName=_fk_Fragebogen; fkGuidBackingName=_fk_guid_Fragebogen;
        // referencedInterface=Zetbox.App.Test.Fragebogen; moduleNamespace=Zetbox.App.Test;
        // inverse Navigator=Antworten; is list;
        // PositionStorage=gute_Antworten_pos;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
		[System.Runtime.Serialization.IgnoreDataMember]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Zetbox.App.Test.Fragebogen Fragebogen
        {
            get { return FragebogenImpl; }
            set { FragebogenImpl = (Zetbox.App.Test.FragebogenEfImpl)value; }
        }

        private int? _fk_Fragebogen;

        /// <summary>ForeignKey Property for Fragebogen's id, used on APIs only</summary>
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int? FK_Fragebogen
		{
			get { return Fragebogen != null ? Fragebogen.ID : (int?)null; }
			set { _fk_Fragebogen = value; }
		}


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Ein_Fragebogen_enthaelt_gute_Antworten", "Ein_Fragebogen")]
        public Zetbox.App.Test.FragebogenEfImpl FragebogenImpl
        {
            get
            {
                Zetbox.App.Test.FragebogenEfImpl __value;
                EntityReference<Zetbox.App.Test.FragebogenEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Test.FragebogenEfImpl>(
                        "Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten",
                        "Ein_Fragebogen");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                __value = r.Value;
                if (OnFragebogen_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Zetbox.App.Test.Fragebogen>(__value);
                    OnFragebogen_Getter(this, e);
                    __value = (Zetbox.App.Test.FragebogenEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongZetboxContextException();

                EntityReference<Zetbox.App.Test.FragebogenEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Zetbox.App.Test.FragebogenEfImpl>(
                        "Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten",
                        "Ein_Fragebogen");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Zetbox.App.Test.FragebogenEfImpl __oldValue = (Zetbox.App.Test.FragebogenEfImpl)r.Value;
                Zetbox.App.Test.FragebogenEfImpl __newValue = (Zetbox.App.Test.FragebogenEfImpl)value;

                // Changing Event fires before anything is touched
                NotifyPropertyChanging("Fragebogen", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Antworten", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Antworten", null, null);
                }

                if (OnFragebogen_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Zetbox.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PreSetter(this, e);
                    __newValue = (Zetbox.App.Test.FragebogenEfImpl)e.Result;
                }

                r.Value = (Zetbox.App.Test.FragebogenEfImpl)__newValue;

                if (OnFragebogen_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Zetbox.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                NotifyPropertyChanged("Fragebogen", __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("Antworten", null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("Antworten", null, null);
                }
                if(IsAttached) UpdateChangedInfo = true;
            }
        }

        public Zetbox.API.Async.ZbTask TriggerFetchFragebogenAsync()
        {
            return new Zetbox.API.Async.ZbTask<Zetbox.App.Test.Fragebogen>(this.Fragebogen);
        }

        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? gute_Antworten_pos
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _gute_Antworten_pos;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_gute_Antworten_pos != value)
                {
                    var __oldValue = _gute_Antworten_pos;
                    var __newValue = value;
                    NotifyPropertyChanging("gute_Antworten_pos", __oldValue, __newValue);
                    _gute_Antworten_pos = __newValue;
                    NotifyPropertyChanged("gute_Antworten_pos", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("gute_Antworten_pos");
                }
            }
        }
        private int? _gute_Antworten_pos_store;
        private int? _gute_Antworten_pos {
            get { return _gute_Antworten_pos_store; }
            set {
                ReportEfPropertyChanging("gute_Antworten_pos");
                _gute_Antworten_pos_store = value;
                ReportEfPropertyChanged("gute_Antworten_pos");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, Zetbox.App.Test.Fragebogen> OnFragebogen_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFragebogen_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int FragenNummer
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _FragenNummer;
                if (OnFragenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnFragenNummer_Getter(this, __e);
                    __result = _FragenNummer = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_FragenNummer != value)
                {
                    var __oldValue = _FragenNummer;
                    var __newValue = value;
                    if (OnFragenNummer_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int>(__oldValue, __newValue);
                        OnFragenNummer_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("FragenNummer", __oldValue, __newValue);
                    _FragenNummer = __newValue;
                    NotifyPropertyChanged("FragenNummer", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnFragenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnFragenNummer_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("FragenNummer");
                }
            }
        }
        private int _FragenNummer_store;
        private int _FragenNummer {
            get { return _FragenNummer_store; }
            set {
                ReportEfPropertyChanging("FragenNummer");
                _FragenNummer_store = value;
                ReportEfPropertyChanged("FragenNummer");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, int> OnFragenNummer_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnFragenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? GegebeneAntwort
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _GegebeneAntwort;
                if (OnGegebeneAntwort_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnGegebeneAntwort_Getter(this, __e);
                    __result = _GegebeneAntwort = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_GegebeneAntwort != value)
                {
                    var __oldValue = _GegebeneAntwort;
                    var __newValue = value;
                    if (OnGegebeneAntwort_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnGegebeneAntwort_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("GegebeneAntwort", __oldValue, __newValue);
                    _GegebeneAntwort = __newValue;
                    NotifyPropertyChanged("GegebeneAntwort", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnGegebeneAntwort_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnGegebeneAntwort_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("GegebeneAntwort");
                }
            }
        }
        private int? _GegebeneAntwort_store;
        private int? _GegebeneAntwort {
            get { return _GegebeneAntwort_store; }
            set {
                ReportEfPropertyChanging("GegebeneAntwort");
                _GegebeneAntwort_store = value;
                ReportEfPropertyChanged("GegebeneAntwort");
            }
        }
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Antwort, int?> OnGegebeneAntwort_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Antwort> OnGegebeneAntwort_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Antwort);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Antwort)obj;
            var otherImpl = (AntwortEfImpl)obj;
            var me = (Antwort)this;

            me.Frage = other.Frage;
            me.FragenNummer = other.FragenNummer;
            me.GegebeneAntwort = other.GegebeneAntwort;
            this.gute_Antworten_pos = otherImpl.gute_Antworten_pos;
            this._fk_Fragebogen = otherImpl._fk_Fragebogen;
        }
        public override void SetNew()
        {
            base.SetNew();
        }
        #region Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Frage":
                case "Fragebogen":
                case "FragenNummer":
                case "GegebeneAntwort":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }
        #endregion // Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Fragebogen":
                return TriggerFetchFragebogenAsync();
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Fragebogen.HasValue)
                FragebogenImpl = (Zetbox.App.Test.FragebogenEfImpl)Context.Find<Zetbox.App.Test.Fragebogen>(_fk_Fragebogen.Value);
            else
                FragebogenImpl = null;
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // else
                    new PropertyDescriptorEfImpl<Antwort, string>(
                        lazyCtx,
                        new Guid("311cb474-be7d-4e6b-b803-379e6523720c"),
                        "Frage",
                        null,
                        obj => obj.Frage,
                        (obj, val) => obj.Frage = val,
						obj => OnFrage_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<Antwort, Zetbox.App.Test.Fragebogen>(
                        lazyCtx,
                        new Guid("ae20c23b-0cfa-422a-9f8d-797e9f70bf82"),
                        "Fragebogen",
                        null,
                        obj => obj.Fragebogen,
                        (obj, val) => obj.Fragebogen = val,
						obj => OnFragebogen_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<Antwort, int>(
                        lazyCtx,
                        new Guid("87a005fa-6249-4aab-b90e-b50c97487c09"),
                        "FragenNummer",
                        null,
                        obj => obj.FragenNummer,
                        (obj, val) => obj.FragenNummer = val,
						obj => OnFragenNummer_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<Antwort, int?>(
                        lazyCtx,
                        new Guid("bbddff1f-943e-48cb-b097-377040280f0a"),
                        "GegebeneAntwort",
                        null,
                        obj => obj.GegebeneAntwort,
                        (obj, val) => obj.GegebeneAntwort = val,
						obj => OnGegebeneAntwort_IsValid), 
                    // position columns
                    // rel: Ein_Fragebogen enthaelt gute_Antworten (0f425937-0d1e-4887-ae65-a162b45fc93e)
                    // rel.B.Type == cls && rel.B.HasPersistentOrder
                    new PropertyDescriptorEfImpl<AntwortEfImpl, int?>(
                        lazyCtx,
                        null,
                        "gute_Antworten_pos",
                        null,
                        obj => obj.gute_Antworten_pos,
                        (obj, val) => obj.gute_Antworten_pos = val,
						null),
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Antwort")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Antwort != null)
            {
                OnToString_Antwort(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Antwort> OnToString_Antwort;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Antwort")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Antwort != null)
            {
                OnObjectIsValid_Antwort(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Antwort> OnObjectIsValid_Antwort;

        [EventBasedMethod("OnNotifyPreSave_Antwort")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Antwort != null) OnNotifyPreSave_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyPreSave_Antwort;

        [EventBasedMethod("OnNotifyPostSave_Antwort")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Antwort != null) OnNotifyPostSave_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyPostSave_Antwort;

        [EventBasedMethod("OnNotifyCreated_Antwort")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Frage");
            SetNotInitializedProperty("Fragebogen");
            SetNotInitializedProperty("FragenNummer");
            SetNotInitializedProperty("GegebeneAntwort");
            base.NotifyCreated();
            if (OnNotifyCreated_Antwort != null) OnNotifyCreated_Antwort(this);
        }
        public static event ObjectEventHandler<Antwort> OnNotifyCreated_Antwort;

        [EventBasedMethod("OnNotifyDeleting_Antwort")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Antwort != null) OnNotifyDeleting_Antwort(this);
            Fragebogen = null;
        }
        public static event ObjectEventHandler<Antwort> OnNotifyDeleting_Antwort;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty
        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _ID;
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var __oldValue = _ID;
                    var __newValue = value;
                    NotifyPropertyChanging("ID", __oldValue, __newValue);
                    _ID = __newValue;
                    NotifyPropertyChanged("ID", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                }
                else
                {
                    SetInitializedProperty("ID");
                }
            }
        }
        private int _ID;
        // END Zetbox.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Frage);
            {
                var r = this.RelationshipManager.GetRelatedReference<Zetbox.App.Test.FragebogenEfImpl>("Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten", "Ein_Fragebogen");
                var key = r.EntityKey;
                binStream.Write(r.Value != null ? r.Value.ID : (key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null));
            }
            binStream.Write(this._gute_Antworten_pos);
            binStream.Write(this._FragenNummer);
            binStream.Write(this._GegebeneAntwort);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Frage = binStream.ReadString();
            binStream.Read(out this._fk_Fragebogen);
            this._gute_Antworten_pos = binStream.ReadNullableInt32();
            this._FragenNummer = binStream.ReadInt32();
            this._GegebeneAntwort = binStream.ReadNullableInt32();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}