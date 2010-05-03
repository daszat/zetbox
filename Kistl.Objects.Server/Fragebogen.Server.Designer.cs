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

    using Kistl.API.Server;
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Fragebogen")]
    [System.Diagnostics.DebuggerDisplay("Fragebogen")]
    public class Fragebogen__Implementation__ : BaseServerDataObject_EntityFramework, Fragebogen
    {
    
		public Fragebogen__Implementation__()
		{
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.IdProperty
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
   		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.ObjectListProperty
	    // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Test.Antwort> Antworten
        {
            get
            {
                if (_AntwortenWrapper == null)
                {
                    _AntwortenWrapper = new EntityListWrapper<Kistl.App.Test.Antwort, Kistl.App.Test.Antwort__Implementation__>(
                            this.Context, Antworten__Implementation__, "Ein_Fragebogen", "gute_Antworten_pos");
                }
                return _AntwortenWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Ein_Fragebogen_enthaelt_gute_Antworten", "gute_Antworten")]
        public EntityCollection<Kistl.App.Test.Antwort__Implementation__> Antworten__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Test.Antwort__Implementation__>(
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
        private EntityListWrapper<Kistl.App.Test.Antwort, Kistl.App.Test.Antwort__Implementation__> _AntwortenWrapper;

		private List<int> AntwortenIds;
		private bool Antworten_was_eagerLoaded = false;


        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual int? BogenNummer
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
                    if(OnBogenNummer_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("BogenNummer", __oldValue, __newValue);
                    _BogenNummer = __newValue;
                    NotifyPropertyChanged("BogenNummer", __oldValue, __newValue);
                    if(OnBogenNummer_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<int?>(__oldValue, __newValue);
                        OnBogenNummer_PostSetter(this, __e);
                    }
                }
            }
        }
        private int? _BogenNummer;
		public static event PropertyGetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Fragebogen, int?> OnBogenNummer_PostSetter;

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
		// Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.CollectionEntryListProperty
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Test.TestStudent> Student
        {
            get
            {
                if (_StudentWrapper == null)
                {
                    _StudentWrapper = new EntityRelationASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntry__Implementation__>(
                            this,
                            Student__Implementation__);
                }
                return _StudentWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Student_füllt_aus_Testbogen_B", "CollectionEntry")]
        public EntityCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntry__Implementation__> Student__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntry__Implementation__>(
                        "Model.FK_Student_füllt_aus_Testbogen_B",
                        "CollectionEntry");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                    c.ForEach(i => i.AttachToContext(Context));
                }
                return c;
            }
        }
        private EntityRelationASideCollectionWrapper<Kistl.App.Test.TestStudent, Kistl.App.Test.Fragebogen, Kistl.App.Test.TestStudent_füllt_aus_Fragebogen_RelationEntry__Implementation__> _StudentWrapper;


		public override Type GetImplementedInterface()
		{
			return typeof(Fragebogen);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Fragebogen)obj;
			var otherImpl = (Fragebogen__Implementation__)obj;
			var me = (Fragebogen)this;

			me.BogenNummer = other.BogenNummer;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

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

        [EventBasedMethod("OnPreSave_Fragebogen")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Fragebogen != null) OnPreSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnPreSave_Fragebogen;

        [EventBasedMethod("OnPostSave_Fragebogen")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Fragebogen != null) OnPostSave_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnPostSave_Fragebogen;

        [EventBasedMethod("OnCreated_Fragebogen")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Fragebogen != null) OnCreated_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnCreated_Fragebogen;

        [EventBasedMethod("OnDeleting_Fragebogen")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Fragebogen != null) OnDeleting_Fragebogen(this);
        }
        public static event ObjectEventHandler<Fragebogen> OnDeleting_Fragebogen;


		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {
			// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
			new CustomPropertyDescriptor<Fragebogen__Implementation__, IList<Kistl.App.Test.Antwort>>(
				new Guid("e8f20c02-abea-4c91-850f-c321adfd46f0"),
				"Antworten",
				null,
				obj => obj.Antworten,
				null), // lists are read-only properties
			// else
			new CustomPropertyDescriptor<Fragebogen__Implementation__, int?>(
				new Guid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc"),
				"BogenNummer",
				null,
				obj => obj.BogenNummer,
				(obj, val) => obj.BogenNummer = val),
			// property.IsAssociation() && !property.IsObjectReferencePropertySingle()
			new CustomPropertyDescriptor<Fragebogen__Implementation__, ICollection<Kistl.App.Test.TestStudent>>(
				new Guid("3a91e745-0dd2-4f31-864e-eaf657ddb577"),
				"Student",
				null,
				obj => obj.Student,
				null), // lists are read-only properties
			// rel: Ein_Fragebogen enthaelt gute_Antworten (0f425937-0d1e-4887-ae65-a162b45fc93e)
		};
		
		protected override void CollectProperties(List<System.ComponentModel.PropertyDescriptor> props)
		{
			base.CollectProperties(props);
			props.AddRange(_properties);
		}
	

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);

			BinarySerializer.ToStream(eagerLoadLists, binStream);
			if(eagerLoadLists)
			{
				BinarySerializer.ToStream(true, binStream);
				BinarySerializer.ToStream(Antworten.Count, binStream);
				foreach(var obj in Antworten)
				{
					if (auxObjects != null) {
						auxObjects.Add(obj);
					}
					BinarySerializer.ToStream(obj.ID, binStream);
				}
			}
			else
			{
				BinarySerializer.ToStream(false, binStream);
			}
            BinarySerializer.ToStream(this._BogenNummer, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);

			BinarySerializer.FromStream(out Antworten_was_eagerLoaded, binStream);
			{
				bool containsList;
				BinarySerializer.FromStream(out containsList, binStream);
				if(containsList)
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
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._BogenNummer, xml, "BogenNummer", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._BogenNummer, xml, "BogenNummer", "Kistl.App.Test");
        }

#endregion

    }


}