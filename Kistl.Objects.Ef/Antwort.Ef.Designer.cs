// <autogenerated/>

namespace Kistl.App.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Antwort")]
    [System.Diagnostics.DebuggerDisplay("Antwort")]
    public class AntwortEfImpl : BaseServerDataObject_EntityFramework, Antwort
    {
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
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public string Frage
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(string);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Frage;
                if (OnFrage_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnFrage_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnFrage_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnFrage_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Frage;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Antwort, string> OnFrage_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Antwort, string> OnFrage_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Antwort, string> OnFrage_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Antwort> OnFrage_IsValid;

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
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
        // fkBackingName=_fk_Fragebogen; fkGuidBackingName=_fk_guid_Fragebogen;
        // referencedInterface=Kistl.App.Test.Fragebogen; moduleNamespace=Kistl.App.Test;
        // inverse Navigator=Antworten; is list;
        // PositionStorage=gute_Antworten_pos;
        // Target not exportable

        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Test.Fragebogen Fragebogen
        {
            get { return FragebogenImpl; }
            set { FragebogenImpl = (Kistl.App.Test.FragebogenEfImpl)value; }
        }

        private int? _fk_Fragebogen;


        // internal implementation, EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Ein_Fragebogen_enthaelt_gute_Antworten", "Ein_Fragebogen")]
        public Kistl.App.Test.FragebogenEfImpl FragebogenImpl
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return null;
                Kistl.App.Test.FragebogenEfImpl __value;
                EntityReference<Kistl.App.Test.FragebogenEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Test.FragebogenEfImpl>(
                        "Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten",
                        "Ein_Fragebogen");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                if (r.Value != null) r.Value.AttachToContext(this.Context);
                __value = r.Value;
                if (OnFragebogen_Getter != null)
                {
                    var e = new PropertyGetterEventArgs<Kistl.App.Test.Fragebogen>(__value);
                    OnFragebogen_Getter(this, e);
                    __value = (Kistl.App.Test.FragebogenEfImpl)e.Result;
                }
                return __value;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (value != null && value.Context != this.Context) throw new WrongKistlContextException();

                EntityReference<Kistl.App.Test.FragebogenEfImpl> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Test.FragebogenEfImpl>(
                        "Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten",
                        "Ein_Fragebogen");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load();
                }
                Kistl.App.Test.FragebogenEfImpl __oldValue = (Kistl.App.Test.FragebogenEfImpl)r.Value;
                Kistl.App.Test.FragebogenEfImpl __newValue = (Kistl.App.Test.FragebogenEfImpl)value;

                // Changing Event fires before anything is touched
                // navigators may not be notified to entity framework
                NotifyPropertyChanging("Fragebogen", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanging("Antworten", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanging("Antworten", null, null, null);
                }

                if (OnFragebogen_PreSetter != null)
                {
                    var e = new PropertyPreSetterEventArgs<Kistl.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PreSetter(this, e);
                    __newValue = (Kistl.App.Test.FragebogenEfImpl)e.Result;
                }

                r.Value = (Kistl.App.Test.FragebogenEfImpl)__newValue;

                if (OnFragebogen_PostSetter != null)
                {
                    var e = new PropertyPostSetterEventArgs<Kistl.App.Test.Fragebogen>(__oldValue, __newValue);
                    OnFragebogen_PostSetter(this, e);
                }

                // everything is done. fire the Changed event
                // navigators may not be notified to entity framework
                NotifyPropertyChanged("Fragebogen", null, __oldValue, __newValue);
                if (__oldValue != null) {
                    __oldValue.NotifyPropertyChanged("Antworten", null, null, null);
                }
                if (__newValue != null) {
                    __newValue.NotifyPropertyChanged("Antworten", null, null, null);
                }
            }
        }

        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? gute_Antworten_pos
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
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
                }
            }
        }
        private int? _gute_Antworten_pos;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingValueProperty
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectReferencePropertyTemplate for Fragebogen
		public static event PropertyGetterHandler<Kistl.App.Test.Antwort, Kistl.App.Test.Fragebogen> OnFragebogen_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Antwort, Kistl.App.Test.Fragebogen> OnFragebogen_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Antwort, Kistl.App.Test.Fragebogen> OnFragebogen_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Antwort> OnFragebogen_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int FragenNummer
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _FragenNummer;
                if (OnFragenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int>(__result);
                    OnFragenNummer_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnFragenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int>(__oldValue, __newValue);
                        OnFragenNummer_PostSetter(this, __e);
                    }
                }
            }
        }
        private int _FragenNummer;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Antwort, int> OnFragenNummer_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Antwort, int> OnFragenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Antwort, int> OnFragenNummer_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Antwort> OnFragenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? GegebeneAntwort
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _GegebeneAntwort;
                if (OnGegebeneAntwort_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnGegebeneAntwort_Getter(this, __e);
                    __result = __e.Result;
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
                    if (OnGegebeneAntwort_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnGegebeneAntwort_PostSetter(this, __e);
                    }
                }
            }
        }
        private int? _GegebeneAntwort;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Antwort, int?> OnGegebeneAntwort_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Antwort, int?> OnGegebeneAntwort_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Antwort, int?> OnGegebeneAntwort_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Antwort> OnGegebeneAntwort_IsValid;

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

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references

            if (_fk_Fragebogen.HasValue)
                FragebogenImpl = (Kistl.App.Test.FragebogenEfImpl)Context.Find<Kistl.App.Test.Fragebogen>(_fk_Fragebogen.Value);
            else
                FragebogenImpl = null;
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
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
                    new PropertyDescriptorEfImpl<Antwort, Kistl.App.Test.Fragebogen>(
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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
            e.IsValid = b.IsValid;
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
        }
        public static event ObjectEventHandler<Antwort> OnNotifyDeleting_Antwort;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty
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
                }
            }
        }
        private int _ID;
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.IdProperty

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            BinarySerializer.ToStream(this._Frage, binStream);
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Test.FragebogenEfImpl>("Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten", "Ein_Fragebogen").EntityKey;
                BinarySerializer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, binStream);
            }
            BinarySerializer.ToStream(this._gute_Antworten_pos, binStream);
            BinarySerializer.ToStream(this._FragenNummer, binStream);
            BinarySerializer.ToStream(this._GegebeneAntwort, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            BinarySerializer.FromStream(out this._Frage, binStream);
            BinarySerializer.FromStream(out this._fk_Fragebogen, binStream);
            BinarySerializer.FromStream(out this._gute_Antworten_pos, binStream);
            BinarySerializer.FromStream(out this._FragenNummer, binStream);
            BinarySerializer.FromStream(out this._GegebeneAntwort, binStream);
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            XmlStreamer.ToStream(this._Frage, xml, "Frage", "Kistl.App.Test");
            {
                var key = this.RelationshipManager.GetRelatedReference<Kistl.App.Test.FragebogenEfImpl>("Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten", "Ein_Fragebogen").EntityKey;
                XmlStreamer.ToStream(key != null ? (int?)key.EntityKeyValues.Single().Value : (int?)null, xml, "Fragebogen", "Kistl.App.Test");
            }
            XmlStreamer.ToStream(this._gute_Antworten_pos, xml, "gute_Antworten_pos", "Kistl.App.Test");
            XmlStreamer.ToStream(this._FragenNummer, xml, "FragenNummer", "Kistl.App.Test");
            XmlStreamer.ToStream(this._GegebeneAntwort, xml, "GegebeneAntwort", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._Frage, xml, "Frage", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._fk_Fragebogen, xml, "Fragebogen", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._gute_Antworten_pos, xml, "gute_Antworten_pos", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._FragenNummer, xml, "FragenNummer", "Kistl.App.Test");
            XmlStreamer.FromStream(ref this._GegebeneAntwort, xml, "GegebeneAntwort", "Kistl.App.Test");
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}