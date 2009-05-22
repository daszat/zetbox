// <autogenerated/>


namespace Kistl.App.Base
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

    using Kistl.API.Client;
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// Describes the currently loaded physical database schema
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CurrentSchema")]
    public class CurrentSchema__Implementation__ : BaseClientDataObject_ClientObjects, CurrentSchema
    {
    
		public CurrentSchema__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// XML descriptor of the current schema
        /// </summary>
        // value type property
        public virtual string Schema
        {
            get
            {
                return _Schema;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Schema != value)
                {
					var __oldValue = _Schema;
                    NotifyPropertyChanging("Schema", __oldValue, value);
                    _Schema = value;
                    NotifyPropertyChanged("Schema", __oldValue, value);
                }
            }
        }
        private string _Schema;

        /// <summary>
        /// Version number of this schema
        /// </summary>
        // value type property
        public virtual int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Version != value)
                {
					var __oldValue = _Version;
                    NotifyPropertyChanging("Version", __oldValue, value);
                    _Version = value;
                    NotifyPropertyChanged("Version", __oldValue, value);
                }
            }
        }
        private int _Version;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(CurrentSchema));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (CurrentSchema)obj;
			var otherImpl = (CurrentSchema__Implementation__)obj;
			var me = (CurrentSchema)this;

			me.Schema = other.Schema;
			me.Version = other.Version;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CurrentSchema != null)
            {
                OnToString_CurrentSchema(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<CurrentSchema> OnToString_CurrentSchema;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CurrentSchema != null) OnPreSave_CurrentSchema(this);
        }
        public event ObjectEventHandler<CurrentSchema> OnPreSave_CurrentSchema;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CurrentSchema != null) OnPostSave_CurrentSchema(this);
        }
        public event ObjectEventHandler<CurrentSchema> OnPostSave_CurrentSchema;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Schema":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(267).Constraints
						.Where(c => !c.IsValid(this, this.Schema))
						.Select(c => c.GetErrorText(this, this.Schema))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Version":
				{
					var errors = FrozenContext.Single.Find<Kistl.App.Base.Property>(268).Constraints
						.Where(c => !c.IsValid(this, this.Version))
						.Select(c => c.GetErrorText(this, this.Version))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
			
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Schema, binStream);
            BinarySerializer.ToStream(this._Version, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Schema, binStream);
            BinarySerializer.FromStream(out this._Version, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(this._Schema, xml, "Schema", "Kistl.App.Base");
            XmlStreamer.ToStream(this._Version, xml, "Version", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Schema, xml, "Schema", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._Version, xml, "Version", "Kistl.App.Base");
        }

#endregion

    }


}