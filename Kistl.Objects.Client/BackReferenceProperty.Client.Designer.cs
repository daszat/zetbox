
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

    /// <summary>
    /// Metadefinition Object for BackReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BackReferenceProperty")]
    public class BackReferenceProperty__Implementation__ : Kistl.App.Base.BaseProperty__Implementation__, BackReferenceProperty
    {


        /// <summary>
        /// Das Property, welches auf diese Klasse zeigt
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectReferenceProperty ReferenceProperty
        {
            get
            {
                if (fk_ReferenceProperty.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectReferenceProperty>(fk_ReferenceProperty.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ReferenceProperty
        {
            get
            {
                return _fk_ReferenceProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ReferenceProperty != value)
                {
                    NotifyPropertyChanging("ReferenceProperty");
                    _fk_ReferenceProperty = value;
                    NotifyPropertyChanging("ReferenceProperty");
                }
            }
        }
        private int? _fk_ReferenceProperty;

        /// <summary>
        /// Serialisierung der Liste zum Client
        /// </summary>
        // value type property
        public virtual bool PreFetchToClient
        {
            get
            {
                return _PreFetchToClient;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_PreFetchToClient != value)
                {
                    NotifyPropertyChanging("PreFetchToClient");
                    _PreFetchToClient = value;
                    NotifyPropertyChanged("PreFetchToClient");;
                }
            }
        }
        private bool _PreFetchToClient;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_BackReferenceProperty != null)
            {
                OnGetPropertyTypeString_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<BackReferenceProperty> OnGetPropertyTypeString_BackReferenceProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_BackReferenceProperty != null)
            {
                OnGetGUIRepresentation_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<BackReferenceProperty> OnGetGUIRepresentation_BackReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_BackReferenceProperty != null)
            {
                OnGetPropertyType_BackReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<BackReferenceProperty> OnGetPropertyType_BackReferenceProperty;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BackReferenceProperty != null)
            {
                OnToString_BackReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BackReferenceProperty> OnToString_BackReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BackReferenceProperty != null) OnPreSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPreSave_BackReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BackReferenceProperty != null) OnPostSave_BackReferenceProperty(this);
        }
        public event ObjectEventHandler<BackReferenceProperty> OnPostSave_BackReferenceProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_ReferenceProperty, binStream);
            BinarySerializer.ToStream(this._PreFetchToClient, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ReferenceProperty, binStream);
            BinarySerializer.FromStream(out this._PreFetchToClient, binStream);
        }

#endregion

    }


}