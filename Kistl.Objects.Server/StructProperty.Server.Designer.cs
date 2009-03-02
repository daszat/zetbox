
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
    /// Metadefinition Object for Struct Properties.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="StructProperty")]
    [System.Diagnostics.DebuggerDisplay("StructProperty")]
    public class StructProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, StructProperty
    {


        /// <summary>
        /// Definition of this Struct
        /// </summary>
    /*
    Relation: FK_StructProperty_Struct_StructProperty_52
    A: ZeroOrMore StructProperty as StructProperty
    B: ZeroOrOne Struct as StructDefinition
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Struct StructDefinition
        {
            get
            {
                return StructDefinition__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                StructDefinition__Implementation__ = (Kistl.App.Base.Struct__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_StructDefinition
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && StructDefinition != null)
                {
                    _fk_StructDefinition = StructDefinition.ID;
                }
                return _fk_StructDefinition;
            }
            set
            {
                _fk_StructDefinition = value;
            }
        }
        private int? _fk_StructDefinition;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_StructProperty_Struct_StructProperty_52", "StructDefinition")]
        public Kistl.App.Base.Struct__Implementation__ StructDefinition__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Struct__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Struct__Implementation__>(
                        "Model.FK_StructProperty_Struct_StructProperty_52",
                        "StructDefinition");
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
                EntityReference<Kistl.App.Base.Struct__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Struct__Implementation__>(
                        "Model.FK_StructProperty_Struct_StructProperty_52",
                        "StructDefinition");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Struct__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? StructDefinition_pos
        {
            get
            {
                return _StructDefinition_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_StructDefinition_pos != value)
                {
                    NotifyPropertyChanging("StructDefinition_pos");
                    _StructDefinition_pos = value;
                    NotifyPropertyChanged("StructDefinition_pos");
                }
            }
        }
        private int? _StructDefinition_pos;
        

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_StructProperty != null)
            {
                OnGetGUIRepresentation_StructProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<StructProperty> OnGetGUIRepresentation_StructProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_StructProperty != null)
            {
                OnGetPropertyType_StructProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<StructProperty> OnGetPropertyType_StructProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_StructProperty != null)
            {
                OnGetPropertyTypeString_StructProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<StructProperty> OnGetPropertyTypeString_StructProperty;



		public override Type GetInterfaceType()
		{
			return typeof(StructProperty);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StructProperty != null)
            {
                OnToString_StructProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StructProperty> OnToString_StructProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StructProperty != null) OnPreSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPreSave_StructProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StructProperty != null) OnPostSave_StructProperty(this);
        }
        public event ObjectEventHandler<StructProperty> OnPostSave_StructProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_StructDefinition, binStream);
            BinarySerializer.ToStream(this._StructDefinition_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_StructDefinition;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_StructDefinition = tmp;
            }
            BinarySerializer.FromStream(out this._StructDefinition_pos, binStream);
        }

#endregion

    }


}