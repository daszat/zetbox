
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
    /// Metadefinition Object for CLR Object Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("CLRObjectParameter")]
    public class CLRObjectParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, CLRObjectParameter
    {
    
		public CLRObjectParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Assembly des CLR Objektes, NULL für Default Assemblies
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                if (fk_Assembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_Assembly.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_Assembly == null)
					return;
                else if (value != null && value.ID == _fk_Assembly)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Assembly;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Assembly", oldValue, value);
                
				// next, set the local reference
                _fk_Assembly = value == null ? (int?)null : value.ID;
				
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Assembly", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Assembly
        {
            get
            {
                return _fk_Assembly;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Assembly != value)
                {
					var __oldValue = _fk_Assembly;
                    NotifyPropertyChanging("Assembly", __oldValue, value);
                    _fk_Assembly = value;
                    NotifyPropertyChanged("Assembly", __oldValue, value);
                }
            }
        }
        private int? _fk_Assembly;

        /// <summary>
        /// Name des CLR Datentypen
        /// </summary>
        // value type property
        public virtual string FullTypeName
        {
            get
            {
                return _FullTypeName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_FullTypeName != value)
                {
					var __oldValue = _FullTypeName;
                    NotifyPropertyChanging("FullTypeName", __oldValue, value);
                    _FullTypeName = value;
                    NotifyPropertyChanged("FullTypeName", __oldValue, value);
                }
            }
        }
        private string _FullTypeName;

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CLRObjectParameter != null)
            {
                OnGetParameterType_CLRObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CLRObjectParameter != null)
            {
                OnGetParameterTypeString_CLRObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(CLRObjectParameter));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (CLRObjectParameter)obj;
			var otherImpl = (CLRObjectParameter__Implementation__)obj;
			var me = (CLRObjectParameter)this;

			me.FullTypeName = other.FullTypeName;
			this.fk_Assembly = otherImpl.fk_Assembly;
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
            if (OnToString_CLRObjectParameter != null)
            {
                OnToString_CLRObjectParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<CLRObjectParameter> OnToString_CLRObjectParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CLRObjectParameter != null) OnPreSave_CLRObjectParameter(this);
        }
        public event ObjectEventHandler<CLRObjectParameter> OnPreSave_CLRObjectParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CLRObjectParameter != null) OnPostSave_CLRObjectParameter(this);
        }
        public event ObjectEventHandler<CLRObjectParameter> OnPostSave_CLRObjectParameter;


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "Assembly":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(98).Constraints
						.Where(c => !c.IsValid(this, this.Assembly))
						.Select(c => c.GetErrorText(this, this.Assembly))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "FullTypeName":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(99).Constraints
						.Where(c => !c.IsValid(this, this.FullTypeName))
						.Select(c => c.GetErrorText(this, this.FullTypeName))
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
                case "Assembly":
                    fk_Assembly = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Assembly, binStream);
            BinarySerializer.ToStream(this._FullTypeName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._FullTypeName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_Assembly, xml, "Assembly", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_Assembly, xml, "Assembly", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

#endregion

    }


}