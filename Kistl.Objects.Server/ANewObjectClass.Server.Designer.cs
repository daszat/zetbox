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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ANewObjectClass")]
    [System.Diagnostics.DebuggerDisplay("ANewObjectClass")]
    public class ANewObjectClass__Implementation__ : BaseServerDataObject_EntityFramework, ANewObjectClass
    {
    
		public ANewObjectClass__Implementation__()
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
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
   		// Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses.NotifyingValueProperty
        public virtual string TestString
        {
            get
            {
                return _TestString;
            }
            set
            {
                if (((IPersistenceObject)this).IsReadonly) throw new ReadOnlyObjectException();
                if (_TestString != value)
                {
					var __oldValue = _TestString;
                    NotifyPropertyChanging("TestString", __oldValue, value);
                    _TestString = value;
                    NotifyPropertyChanged("TestString", __oldValue, value);
                }
            }
        }
        private string _TestString;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ANewObjectClass));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ANewObjectClass)obj;
			var otherImpl = (ANewObjectClass__Implementation__)obj;
			var me = (ANewObjectClass)this;

			me.TestString = other.TestString;
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ANewObjectClass != null)
            {
                OnToString_ANewObjectClass(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ANewObjectClass> OnToString_ANewObjectClass;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ANewObjectClass != null) OnPreSave_ANewObjectClass(this);
        }
        public event ObjectEventHandler<ANewObjectClass> OnPreSave_ANewObjectClass;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ANewObjectClass != null) OnPostSave_ANewObjectClass(this);
        }
        public event ObjectEventHandler<ANewObjectClass> OnPostSave_ANewObjectClass;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "TestString":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(110).Constraints
						.Where(c => !c.IsValid(this, this.TestString))
						.Select(c => c.GetErrorText(this, this.TestString))
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
            BinarySerializer.ToStream(this._TestString, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._TestString, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._TestString, xml, "TestString", "Kistl.App.Test");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._TestString, xml, "TestString", "Kistl.App.Test");
        }

#endregion

    }


}