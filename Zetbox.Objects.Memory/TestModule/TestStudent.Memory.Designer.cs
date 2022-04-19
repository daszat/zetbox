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
    [System.Diagnostics.DebuggerDisplay("TestStudent")]
    public class TestStudentMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, TestStudent
    {
        private static readonly Guid _objectClassID = new Guid("9efc763c-9cdf-41e3-930c-7505fc4ac840");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public TestStudentMemoryImpl()
            : base(null)
        {
        }

        public TestStudentMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = _Name = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    var __oldValue = _Name;
                    var __newValue = value;
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(IsAttached) UpdateChangedInfo = true;

                    if (OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
                else
                {
                    SetInitializedProperty("Name");
                }
            }
        }
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.TestStudent, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.TestStudent, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.TestStudent, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestStudent> OnName_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
        // BEGIN Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Testbogen
        public ICollection<Zetbox.App.Test.Fragebogen> Testbogen
        {
            get
            {
                if (_Testbogen == null)
                {
                    var task = TriggerFetchTestbogenAsync();
                    task.TryRunSynchronously();
                    task.Wait();
                }
                return (ICollection<Zetbox.App.Test.Fragebogen>)_Testbogen;
            }
        }

        System.Threading.Tasks.Task _triggerFetchTestbogenTask;
        public System.Threading.Tasks.Task TriggerFetchTestbogenAsync()
        {
            if (_triggerFetchTestbogenTask != null) return _triggerFetchTestbogenTask;
            _triggerFetchTestbogenTask = Context.FetchRelationAsync<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl>(new Guid("6819ca86-571c-4d59-bc30-cc1fb0decc9e"), RelationEndRole.A, this);
            _triggerFetchTestbogenTask = _triggerFetchTestbogenTask.OnResult(r =>
            {
                _Testbogen
                    = new ObservableBSideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl>>(
                        this,
                        new RelationshipFilterASideCollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl>(this.Context, this));
                        // _Testbogen.CollectionChanged is managed by OnTestbogenCollectionChanged() and called from the RelationEntry
            });
            return _triggerFetchTestbogenTask;
        }

        internal void OnTestbogenCollectionChanged()
        {
            NotifyPropertyChanged("Testbogen", null, null);
            if (OnTestbogen_PostSetter != null && IsAttached)
                OnTestbogen_PostSetter(this);
        }

        private ObservableBSideCollectionWrapper<Zetbox.App.Test.TestStudent, Zetbox.App.Test.Fragebogen, Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.Student_füllt_aus_Testbogen_RelationEntryMemoryImpl>> _Testbogen;
        // END Zetbox.Generator.Templates.Properties.CollectionEntryListProperty for Testbogen
public static event PropertyListChangedHandler<Zetbox.App.Test.TestStudent> OnTestbogen_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.TestStudent> OnTestbogen_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(TestStudent);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (TestStudent)obj;
            var otherImpl = (TestStudentMemoryImpl)obj;
            var me = (TestStudent)this;

            me.Name = other.Name;
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
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "Testbogen":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            case "Testbogen":
                return TriggerFetchTestbogenAsync();
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
                    new PropertyDescriptorMemoryImpl<TestStudent, string>(
                        lazyCtx,
                        new Guid("190b4492-c1cb-40a2-8941-84b8ff3ac141"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<TestStudent, ICollection<Zetbox.App.Test.Fragebogen>>(
                        lazyCtx,
                        new Guid("f330d95b-372d-4302-b4d1-73afc5fa71de"),
                        "Testbogen",
                        null,
                        obj => obj.Testbogen,
                        null, // lists are read-only properties
                        obj => OnTestbogen_IsValid), 
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
        [EventBasedMethod("OnToString_TestStudent")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TestStudent != null)
            {
                OnToString_TestStudent(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<TestStudent> OnToString_TestStudent;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_TestStudent")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_TestStudent != null)
            {
                OnObjectIsValid_TestStudent(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<TestStudent> OnObjectIsValid_TestStudent;

        [EventBasedMethod("OnNotifyPreSave_TestStudent")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_TestStudent != null) OnNotifyPreSave_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyPreSave_TestStudent;

        [EventBasedMethod("OnNotifyPostSave_TestStudent")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_TestStudent != null) OnNotifyPostSave_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyPostSave_TestStudent;

        [EventBasedMethod("OnNotifyCreated_TestStudent")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_TestStudent != null) OnNotifyCreated_TestStudent(this);
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyCreated_TestStudent;

        [EventBasedMethod("OnNotifyDeleting_TestStudent")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_TestStudent != null) OnNotifyDeleting_TestStudent(this);
            Testbogen.Clear();
        }
        public static event ObjectEventHandler<TestStudent> OnNotifyDeleting_TestStudent;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Name = binStream.ReadString();
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