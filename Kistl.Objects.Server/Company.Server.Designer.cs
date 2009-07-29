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
    using Kistl.DALProvider.EF;
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
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.IdProperty
        public override int ID
        {
            get
            {
				return _ID;
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
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string Name
        {
            get
            {
				var __value = _Name;
				if(OnName_Getter != null)
				{
					var e = new PropertyGetterEventArgs<string>(__value);
					OnName_Getter(this, e);
					__value = e.Result;
				}
                return __value;
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
						var e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
						OnName_PreSetter(this, e);
						__newValue = e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);

                    if(OnName_PostSetter != null)
                    {
						var e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
						OnName_PostSetter(this, e);
                    }
                }
            }
        }
        private string _Name;
		public event PropertyGetterHandler<Kistl.App.Test.Company, string> OnName_Getter;
		public event PropertyPreSetterHandler<Kistl.App.Test.Company, string> OnName_PreSetter;
		public event PropertyPostSetterHandler<Kistl.App.Test.Company, string> OnName_PostSetter;
		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Company));
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
        public event ToStringHandler<Company> OnToString_Company;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Company != null) OnPreSave_Company(this);
        }
        public event ObjectEventHandler<Company> OnPreSave_Company;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Company != null) OnPostSave_Company(this);
        }
        public event ObjectEventHandler<Company> OnPostSave_Company;

        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_Company != null) OnCreated_Company(this);
        }
        public event ObjectEventHandler<Company> OnCreated_Company;

        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_Company != null) OnDeleting_Company(this);
        }
        public event ObjectEventHandler<Company> OnDeleting_Company;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Name":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(41).Constraints
						.Where(c => !c.IsValid(this, this.Name))
						.Select(c => c.GetErrorText(this, this.Name))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
			
            base.ToStream(binStream, auxObjects);
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