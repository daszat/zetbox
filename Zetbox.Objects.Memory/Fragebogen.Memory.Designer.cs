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

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Fragebogen")]
    public class FragebogenMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, Fragebogen
    {
        private static readonly Guid _objectClassID = new Guid("a78ff235-4511-431b-8437-939f7fecded4");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public FragebogenMemoryImpl()
            : base(null)
        {
        }

        public FragebogenMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // object list property
        // Zetbox.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Zetbox.App.Test.Antwort> Antworten
        {
            get
            {
                if (_Antworten == null)
                {
                    TriggerFetchAntwortenAsync().Wait();
                }
                return _Antworten;
            }
        }

        Zetbox.API.Async.ZbTask _triggerFetchAntwortenTask;
        public Zetbox.API.Async.ZbTask TriggerFetchAntwortenAsync()
        {
            if (_triggerFetchAntwortenTask != null) return _triggerFetchAntwortenTask;

            List<Zetbox.App.Test.Antwort> serverList = null;
            if (Helper.IsPersistedObject(this))
            {
                if (AntwortenIds != null)
                {
                    _triggerFetchAntwortenTask = new Zetbox.API.Async.ZbTask(Zetbox.API.Async.ZbTask.Synchron, () =>
                    {
                        serverList = AntwortenIds.Select(id => Context.Find<Zetbox.App.Test.Antwort>(id)).ToList();
                        AntwortenIds = null; // allow id list to be garbage collected
                    });
                }
                else
                {
                    _triggerFetchAntwortenTask = Context.GetListOfAsync<Zetbox.App.Test.Antwort>(this, "Antworten")
                        .OnResult(t =>
                        {
                            serverList = t.Result;
                        });
                }
            }
            else
            {
                _triggerFetchAntwortenTask = new Zetbox.API.Async.ZbTask(Zetbox.API.Async.ZbTask.Synchron, () =>
                {
                    serverList = new List<Zetbox.App.Test.Antwort>();
                });
            }

            _triggerFetchAntwortenTask.OnResult(t =>
            {
                _Antworten = new OneNRelationList<Zetbox.App.Test.Antwort>(
                    "Fragebogen",
                    "gute_Antworten_pos",
                    this,
                    OnAntwortenCollectionChanged,
                    serverList);
            });
            return _triggerFetchAntwortenTask;
        }

        internal void OnAntwortenCollectionChanged()
        {
            NotifyPropertyChanged("Antworten", null, null);
            if (OnAntworten_PostSetter != null && IsAttached)
                OnAntworten_PostSetter(this);
        }

        private OneNRelationList<Zetbox.App.Test.Antwort> _Antworten;
        private List<int> AntwortenIds;
        private bool Antworten_was_eagerLoaded = false;
public static event PropertyListChangedHandler<Zetbox.App.Test.Fragebogen> OnAntworten_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnAntworten_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public int? BogenNummer
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _BogenNummer;
                if (OnBogenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnBogenNummer_Getter(this, __e);
                    __result = _BogenNummer = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_BogenNummer != value)
                {
                    var __oldValue = _BogenNummer;
                    var __newValue = value;
                    if (OnBogenNummer_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("BogenNummer", __oldValue, __newValue);
                    _BogenNummer = __newValue;
                    NotifyPropertyChanged("BogenNummer", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnBogenNummer_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("BogenNummer");
                }
            }
        }
        private int? _BogenNummer;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.Fragebogen, int?> OnBogenNummer_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnBogenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
        // BEGIN Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Student
        public ICollection<Zetbox.App.Test.TestStudent> Student
        {
            get
            {
                if (_Student == null)
                {
                    TriggerFetchStudentAsync().Wait();
                }
                return (ICollection<Zetbox.App.Test.TestStudent>)_Student;
            }
        }

        Zetbox.API.Async.ZbTask _triggerFetchStudentTask;
        public Zetbox.API.Async.ZbTask TriggerFetchStudentAsync()
        {
            if (_triggerFetchStudentTask != null) return _triggerFetchStudentTask;
            _triggerFetchStudentTask = Context.FetchRelationAsync<Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl>(new Guid("6819ca86-571c-4d59-bc30-cc1fb0decc9e"), RelationEndRole.B, this);
            _triggerFetchStudentTask.OnResult(r =>
            {
                _Student
                    = new ObservableASideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl>>(
                        this,
                        new RelationshipFilterBSideCollection<Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl>(this.Context, this));
                        // _Student.CollectionChanged is managed by OnStudentCollectionChanged() and called from the RelationEntry
            });
            return _triggerFetchStudentTask;
        }

        internal void OnStudentCollectionChanged()
        {
            NotifyPropertyChanged("Student", null, null);
            if (OnStudent_PostSetter != null && IsAttached)
                OnStudent_PostSetter(this);
        }

        private ObservableASideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryMemoryImpl>> _Student;
        // END Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Student
public static event PropertyListChangedHandler<Zetbox.App.Test.Fragebogen> OnStudent_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.Fragebogen> OnStudent_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Fragebogen);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Fragebogen)obj;
            var otherImpl = (FragebogenMemoryImpl)obj;
            var me = (Fragebogen)this;

            me.BogenNummer = other.BogenNummer;
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "BogenNummer":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Antworten":
                case "Student":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override Zetbox.API.Async.ZbTask TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Antworten":
                return TriggerFetchAntwortenAsync();
            case "Student":
                return TriggerFetchStudentAsync();
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
            // fix cached lists references
            _triggerFetchAntwortenTask = null;
            _Antworten = null;
            _triggerFetchStudentTask = null;
            _Student = null;
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Fragebogen, IList<Zetbox.App.Test.Antwort>>(
                        lazyCtx,
                        new Guid("e8f20c02-abea-4c91-850f-c321adfd46f0"),
                        "Antworten",
                        null,
                        obj => obj.Antworten,
                        null, // lists are read-only properties
                        obj => OnAntworten_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<Fragebogen, int?>(
                        lazyCtx,
                        new Guid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc"),
                        "BogenNummer",
                        null,
                        obj => obj.BogenNummer,
                        (obj, val) => obj.BogenNummer = val,
						obj => OnBogenNummer_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<Fragebogen, ICollection<Zetbox.App.Test.TestStudent>>(
                        lazyCtx,
                        new Guid("3a91e745-0dd2-4f31-864e-eaf657ddb577"),
                        "Student",
                        null,
                        obj => obj.Student,
                        null, // lists are read-only properties
                        obj => OnStudent_IsValid), 
                    // position columns
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
        [EventBasedMethod("OnToString_Fragebogen")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Fragebogen != null)
            {
                OnToString_Fragebogen(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Fragebogen> OnToString_Fragebogen;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_Fragebogen")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_Fragebogen != null)
            {
                OnObjectIsValid_Fragebogen(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<Fragebogen> OnObjectIsValid_Fragebogen;

        [EventBasedMethod("OnNotifyPreSave_Fragebogen")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_Fragebogen != null) OnNotifyPreSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyPreSave_Fragebogen;

        [EventBasedMethod("OnNotifyPostSave_Fragebogen")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_Fragebogen != null) OnNotifyPostSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyPostSave_Fragebogen;

        [EventBasedMethod("OnNotifyCreated_Fragebogen")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("BogenNummer");
            base.NotifyCreated();
            if (OnNotifyCreated_Fragebogen != null) OnNotifyCreated_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyCreated_Fragebogen;

        [EventBasedMethod("OnNotifyDeleting_Fragebogen")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_Fragebogen != null) OnNotifyDeleting_Fragebogen(this);
            Antworten.Clear();
            Student.Clear();
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyDeleting_Fragebogen;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;

            binStream.Write(eagerLoadLists);
            if (eagerLoadLists && auxObjects != null)
            {
                binStream.Write(true);
                binStream.Write(Antworten.Count);
                foreach(var obj in Antworten)
                {
                    auxObjects.Add(obj);
                    binStream.Write(obj.ID);
                }
            }
            else
            {
                binStream.Write(false);
            }
            binStream.Write(this._BogenNummer);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {

            Antworten_was_eagerLoaded = binStream.ReadBoolean();
            {
                bool containsList = binStream.ReadBoolean();
                if (containsList)
                {
                    int numElements = binStream.ReadInt32();
                    AntwortenIds = new List<int>(numElements);
                    while (numElements-- > 0) 
                    {
                        int id = binStream.ReadInt32();
                        AntwortenIds.Add(id);
                    }
                }
            }
            this._BogenNummer = binStream.ReadNullableInt32();
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