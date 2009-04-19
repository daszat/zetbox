
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for Object Parameter.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="ObjectParameter")]
    [System.Diagnostics.DebuggerDisplay("ObjectParameter")]
    public class ObjectParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, ObjectParameter
    {
    
		public ObjectParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Kistl-Typ des Parameters
        /// </summary>
    /*
    Relation: FK_ObjectParameter_DataType_ObjectParameter_45
    A: ZeroOrMore ObjectParameter as ObjectParameter
    B: ZeroOrOne DataType as DataType
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType DataType
        {
            get
            {
                return DataType__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DataType__Implementation__ = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DataType
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DataType != null)
                {
                    _fk_DataType = DataType.ID;
                }
                return _fk_DataType;
            }
            set
            {
                _fk_DataType = value;
            }
        }
        private int? _fk_DataType;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_ObjectParameter_DataType_ObjectParameter_45", "DataType")]
        public Kistl.App.Base.DataType__Implementation__ DataType__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_ObjectParameter_DataType_ObjectParameter_45",
                        "DataType");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_ObjectParameter_DataType_ObjectParameter_45",
                        "DataType");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_ObjectParameter != null)
            {
                OnGetParameterType_ObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<ObjectParameter> OnGetParameterType_ObjectParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_ObjectParameter != null)
            {
                OnGetParameterTypeString_ObjectParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<ObjectParameter> OnGetParameterTypeString_ObjectParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(ObjectParameter));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (ObjectParameter)obj;
			var otherImpl = (ObjectParameter__Implementation__)obj;
			var me = (ObjectParameter)this;

			this.fk_DataType = otherImpl.fk_DataType;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectParameter != null)
            {
                OnToString_ObjectParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectParameter> OnToString_ObjectParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectParameter != null) OnPreSave_ObjectParameter(this);
        }
        public event ObjectEventHandler<ObjectParameter> OnPreSave_ObjectParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectParameter != null) OnPostSave_ObjectParameter(this);
        }
        public event ObjectEventHandler<ObjectParameter> OnPostSave_ObjectParameter;



		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references
			if (_fk_DataType.HasValue)
				DataType__Implementation__ = (Kistl.App.Base.DataType__Implementation__)Context.Find<Kistl.App.Base.DataType>(_fk_DataType.Value);
			else
				DataType__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_DataType, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_DataType;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DataType = tmp;
            }
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this.fk_DataType, xml, "DataType", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            {
                var tmp = this.fk_DataType;
                XmlStreamer.FromStream(ref tmp, xml, "DataType", "http://dasz.at/Kistl");
                this.fk_DataType = tmp;
            }
        }

#endregion

    }


}