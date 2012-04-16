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
    [EdmEntityType(NamespaceName="Model", Name="Fragebogen")]
    [System.Diagnostics.DebuggerDisplay("Fragebogen")]
    public class FragebogenEfImpl : BaseServerDataObject_EntityFramework, Fragebogen
    {
        private static readonly Guid _objectClassID = new Guid("a78ff235-4511-431b-8437-939f7fecded4");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public FragebogenEfImpl()
            : base(null)
        {
        }

        public FragebogenEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Ein_Fragebogen_enthaelt_gute_Antworten
    A: One Fragebogen as Ein_Fragebogen
    B: ZeroOrMore Antwort as gute_Antworten
    Preferred Storage: MergeIntoB
    */
        // object list property
        // object list property
           // Kistl.DalProvider.Ef.Generator.Templates.Properties.ObjectListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Test.Antwort> Antworten
        {
            get
            {
                if (_Antworten == null)
                {
                    _Antworten = new EntityListWrapper<Kistl.App.Test.Antwort, Kistl.App.Test.AntwortEfImpl>(
                            this.Context, AntwortenImpl,
                            () => this.NotifyPropertyChanging("Antworten", null, null, null),
                            () => { this.NotifyPropertyChanged("Antworten", null, null, null); if(OnAntworten_PostSetter != null && IsAttached) OnAntworten_PostSetter(this); },
                            (item) => item.NotifyPropertyChanging("Fragebogen", null, null, null),
                            (item) => item.NotifyPropertyChanged("Fragebogen", null, null, null), "Ein_Fragebogen", "gute_Antworten_pos");
                }
                return _Antworten;
            }
        }
    
        [EdmRelationshipNavigationProperty("Model", "FK_Ein_Fragebogen_enthaelt_gute_Antworten", "gute_Antworten")]
        public EntityCollection<Kistl.App.Test.AntwortEfImpl> AntwortenImpl
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Test.AntwortEfImpl>(
                        "Model.FK_Ein_Fragebogen_enthaelt_gute_Antworten",
                        "gute_Antworten");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                c.ForEach(i => i.AttachToContext(Context));
                return c;
            }
        }
        private EntityListWrapper<Kistl.App.Test.Antwort, Kistl.App.Test.AntwortEfImpl> _Antworten;

        private List<int> AntwortenIds;
        private bool Antworten_was_eagerLoaded = false;

public static event PropertyListChangedHandler<Kistl.App.Test.Fragebogen> OnAntworten_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Fragebogen> OnAntworten_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public int? BogenNummer
        {
            get
            {
                if (!CurrentAccessRights.HasReadRights()) return default(int?);
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _BogenNummer;
                if (OnBogenNummer_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<int?>(__result);
                    OnBogenNummer_Getter(this, __e);
                    __result = __e.Result;
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
        // END Kistl.DalProvider.Ef.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PostSetter;

        public static event PropertyIsValidHandler<Kistl.App.Test.Fragebogen> OnBogenNummer_IsValid;

        /// <summary>
        /// 
        /// </summary>
    /*
    Relation: FK_Student_füllt_aus_Testbogen
    A: ZeroOrMore TestStudent as Student
    B: ZeroOrMore Fragebogen as Testbogen
    Preferred Storage: Separate
    */
        // collection reference property
        // Kistl.DalProvider.Ef.Generator.Templates.Properties.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Test.TestStudent> Student
        {
            get
            {
                if (_Student == null)
                {
                    _Student = new ASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl, EntityCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl>>(
                            this,
                            StudentImpl);
                }
                return _Student;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Student_füllt_aus_Testbogen_B", "CollectionEntry")]
        public EntityCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl> StudentImpl
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl>(
                        "Model.FK_Student_füllt_aus_Testbogen_B",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                c.ForEach(i => i.AttachToContext(Context));
                return c;
            }
        }
        private ASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl, EntityCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntryEfImpl>> _Student;

        public static event PropertyIsValidHandler<Kistl.App.Test.Fragebogen> OnStudent_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(Fragebogen);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (Fragebogen)obj;
            var otherImpl = (FragebogenEfImpl)obj;
            var me = (Fragebogen)this;

            me.BogenNummer = other.BogenNummer;
        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }
        #region Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

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

        #endregion // Kistl.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
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
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorEfImpl<Fragebogen, IList<Kistl.App.Test.Antwort>>(
                        lazyCtx,
                        new Guid("e8f20c02-abea-4c91-850f-c321adfd46f0"),
                        "Antworten",
                        null,
                        obj => obj.Antworten,
                        null, // lists are read-only properties
                        obj => OnAntworten_IsValid), 
                    // else
                    new PropertyDescriptorEfImpl<Fragebogen, int?>(
                        lazyCtx,
                        new Guid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc"),
                        "BogenNummer",
                        null,
                        obj => obj.BogenNummer,
                        (obj, val) => obj.BogenNummer = val,
						obj => OnBogenNummer_IsValid), 
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorEfImpl<Fragebogen, ICollection<Kistl.App.Test.TestStudent>>(
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
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

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
            Antworten.Clear();
            Student.Clear();
            base.NotifyDeleting();
            if (OnNotifyDeleting_Fragebogen != null) OnNotifyDeleting_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnNotifyDeleting_Fragebogen;

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
				else 
				{
					SetInitializedProperty("ID");
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

			BinarySerializer.ToStream(eagerLoadLists, binStream);
			if (eagerLoadLists && auxObjects != null)
			{
				BinarySerializer.ToStream(true, binStream);
				BinarySerializer.ToStream(Antworten.Count, binStream);
				foreach(var obj in Antworten)
				{
					auxObjects.Add(obj);
					BinarySerializer.ToStream(obj.ID, binStream);
				}
			}
			else
			{
				BinarySerializer.ToStream(false, binStream);
			}
            BinarySerializer.ToStream(this._BogenNummer, binStream);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {

			BinarySerializer.FromStream(out Antworten_was_eagerLoaded, binStream);
			{
				bool containsList;
				BinarySerializer.FromStream(out containsList, binStream);
				if (containsList)
				{
					int numElements;
					BinarySerializer.FromStream(out numElements, binStream);
					AntwortenIds = new List<int>(numElements);
					while (numElements-- > 0) 
					{
						int id;
						BinarySerializer.FromStream(out id, binStream);
						AntwortenIds.Add(id);
					}
				}
			}
            BinarySerializer.FromStream(out this._BogenNummer, binStream);
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
            XmlStreamer.ToStream(this._BogenNummer, xml, "BogenNummer", "Kistl.App.Test");
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            XmlStreamer.FromStream(ref this._BogenNummer, xml, "BogenNummer", "Kistl.App.Test");
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