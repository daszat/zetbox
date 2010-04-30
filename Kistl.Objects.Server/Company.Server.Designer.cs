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
    /// Describes a Company
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Company")]
    [System.Diagnostics.DebuggerDisplay("Company")]
    public class Company__Implementation__ : BaseServerDataObject_EntityFramework, Company
    {
    
		public Company__Implementation__()
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
        /// Company name
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
           // Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses.NotifyingDataProperty
        public virtual string Name
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
                    __result = __e.Result;
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
                    if(OnName_PreSetter != null)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    if(OnName_PostSetter != null)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
            }
        }
        private string _Name;
		public static event PropertyGetterHandler<Kistl.App.Test.Company, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Kistl.App.Test.Company, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Kistl.App.Test.Company, string> OnName_PostSetter;

		public override Type GetImplementedInterface()
		{
			return typeof(Company);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Company)obj;
			var otherImpl = (Company__Implementation__)obj;
			var me = (Company)this;

			me.Name = other.Name;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_Company")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Company != null)
            {
                OnToString_Company(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<Company> OnToString_Company;

        [EventBasedMethod("OnPreSave_Company")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Company != null) OnPreSave_Company(this);
        }
        public static event ObjectEventHandler<Company> OnPreSave_Company;

        [EventBasedMethod("OnPostSave_Company")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Company != null) OnPostSave_Company(this);
        }
        public static event ObjectEventHandler<Company> OnPostSave_Company;

        [EventBasedMethod("OnCreated_Company")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Company != null) OnCreated_Company(this);
        }
        public static event ObjectEventHandler<Company> OnCreated_Company;

        [EventBasedMethod("OnDeleting_Company")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Company != null) OnDeleting_Company(this);
        }
        public static event ObjectEventHandler<Company> OnDeleting_Company;


		private static readonly System.ComponentModel.PropertyDescriptor[] _properties = new System.ComponentModel.PropertyDescriptor[] {
			// else
			new CustomPropertyDescriptor<Company__Implementation__, string>(
				new Guid("4a038e35-fffb-4ba7-8009-1954c317a799"),
				"Name",
				null,
				obj => obj.Name,
				(obj, val) => obj.Name = val),
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
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Name, xml, "Name", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "Kistl.App.Test");
        }

#endregion

    }


}