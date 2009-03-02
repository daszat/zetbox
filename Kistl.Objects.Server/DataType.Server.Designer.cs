
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
    /// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="DataType")]
    [System.Diagnostics.DebuggerDisplay("DataType")]
    public class DataType__Implementation__ : BaseServerDataObject_EntityFramework, DataType
    {

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    NotifyPropertyChanging("ID");
                    _ID = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Der Name der Objektklasse
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ClassName != value)
                {
                    NotifyPropertyChanging("ClassName");
                    _ClassName = value;
                    NotifyPropertyChanged("ClassName");
                }
            }
        }
        private string _ClassName;

        /// <summary>
        /// Standard Icon wenn IIcon nicht implementiert ist
        /// </summary>
    /*
    Relation: FK_DataType_Icon_DataType_35
    A: 3 DataType as DataType
    B: 1 Icon as DefaultIcon
    Preferred Storage: 1
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Icon DefaultIcon
        {
            get
            {
                return DefaultIcon__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                DefaultIcon__Implementation__ = (Kistl.App.GUI.Icon__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_DefaultIcon
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && DefaultIcon != null)
                {
                    _fk_DefaultIcon = DefaultIcon.ID;
                }
                return _fk_DefaultIcon;
            }
            set
            {
                _fk_DefaultIcon = value;
            }
        }
        private int? _fk_DefaultIcon;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_Icon_DataType_35", "DefaultIcon")]
        public Kistl.App.GUI.Icon__Implementation__ DefaultIcon__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.GUI.Icon__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Icon__Implementation__>(
                        "Model.FK_DataType_Icon_DataType_35",
                        "DefaultIcon");
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
                EntityReference<Kistl.App.GUI.Icon__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.GUI.Icon__Implementation__>(
                        "Model.FK_DataType_Icon_DataType_35",
                        "DefaultIcon");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.GUI.Icon__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? DefaultIcon_pos
        {
            get
            {
                return _DefaultIcon_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DefaultIcon_pos != value)
                {
                    NotifyPropertyChanging("DefaultIcon_pos");
                    _DefaultIcon_pos = value;
                    NotifyPropertyChanged("DefaultIcon_pos");
                }
            }
        }
        private int? _DefaultIcon_pos;
        

        /// <summary>
        /// Description of this DataType
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }
        private string _Description;

        /// <summary>
        /// all implemented Methods in this DataType
        /// </summary>
    /*
    Relation: FK_DataType_MethodInvocation_InvokeOnObjectClass_41
    A: 2 DataType as InvokeOnObjectClass
    B: 3 MethodInvocation as MethodInvocations
    Preferred Storage: 2
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.MethodInvocation> MethodInvocations
        {
            get
            {
                if (_MethodInvocationsWrapper == null)
                {
                    _MethodInvocationsWrapper = new EntityCollectionWrapper<Kistl.App.Base.MethodInvocation, Kistl.App.Base.MethodInvocation__Implementation__>(
                            this.Context, MethodInvocations__Implementation__);
                }
                return _MethodInvocationsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_MethodInvocation_InvokeOnObjectClass_41", "MethodInvocations")]
        public EntityCollection<Kistl.App.Base.MethodInvocation__Implementation__> MethodInvocations__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.MethodInvocation__Implementation__>(
                        "Model.FK_DataType_MethodInvocation_InvokeOnObjectClass_41",
                        "MethodInvocations");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.MethodInvocation, Kistl.App.Base.MethodInvocation__Implementation__> _MethodInvocationsWrapper;



        /// <summary>
        /// Liste aller Methoden der Objektklasse.
        /// </summary>
    /*
    Relation: FK_DataType_Method_ObjectClass_25
    A: 2 DataType as ObjectClass
    B: 3 Method as Methods
    Preferred Storage: 2
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.Method> Methods
        {
            get
            {
                if (_MethodsWrapper == null)
                {
                    _MethodsWrapper = new EntityCollectionWrapper<Kistl.App.Base.Method, Kistl.App.Base.Method__Implementation__>(
                            this.Context, Methods__Implementation__);
                }
                return _MethodsWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_Method_ObjectClass_25", "Methods")]
        public EntityCollection<Kistl.App.Base.Method__Implementation__> Methods__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_DataType_Method_ObjectClass_25",
                        "Methods");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.Method, Kistl.App.Base.Method__Implementation__> _MethodsWrapper;



        /// <summary>
        /// Modul der Objektklasse
        /// </summary>
    /*
    Relation: FK_Module_DataType_Module_26
    A: 2 Module as Module
    B: 3 DataType as DataTypes
    Preferred Storage: 2
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                return Module__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Module__Implementation__ = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Module != null)
                {
                    _fk_Module = Module.ID;
                }
                return _fk_Module;
            }
            set
            {
                _fk_Module = value;
            }
        }
        private int? _fk_Module;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Module_DataType_Module_26", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_Module_DataType_Module_26",
                        "Module");
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
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_Module_DataType_Module_26",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? Module_pos
        {
            get
            {
                return _Module_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Module_pos != value)
                {
                    NotifyPropertyChanging("Module_pos");
                    _Module_pos = value;
                    NotifyPropertyChanged("Module_pos");
                }
            }
        }
        private int? _Module_pos;
        

        /// <summary>
        /// Eigenschaften der Objektklasse
        /// </summary>
    /*
    Relation: FK_DataType_BaseProperty_ObjectClass_19
    A: 2 DataType as ObjectClass
    B: 3 BaseProperty as Properties
    Preferred Storage: 2
    */
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.BaseProperty> Properties
        {
            get
            {
                if (_PropertiesWrapper == null)
                {
                    _PropertiesWrapper = new EntityCollectionWrapper<Kistl.App.Base.BaseProperty, Kistl.App.Base.BaseProperty__Implementation__>(
                            this.Context, Properties__Implementation__);
                }
                return _PropertiesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_BaseProperty_ObjectClass_19", "Properties")]
        public EntityCollection<Kistl.App.Base.BaseProperty__Implementation__> Properties__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.BaseProperty__Implementation__>(
                        "Model.FK_DataType_BaseProperty_ObjectClass_19",
                        "Properties");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.BaseProperty, Kistl.App.Base.BaseProperty__Implementation__> _PropertiesWrapper;



        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public virtual System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_DataType != null)
            {
                OnGetDataType_DataType(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on DataType.GetDataType");
            }
            return e.Result;
        }
		public delegate void GetDataType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetDataType_Handler<DataType> OnGetDataType_DataType;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public virtual string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_DataType != null)
            {
                OnGetDataTypeString_DataType(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on DataType.GetDataTypeString");
            }
            return e.Result;
        }
		public delegate void GetDataTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetDataTypeString_Handler<DataType> OnGetDataTypeString_DataType;



		public override Type GetInterfaceType()
		{
			return typeof(DataType);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DataType != null)
            {
                OnToString_DataType(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<DataType> OnToString_DataType;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DataType != null) OnPreSave_DataType(this);
        }
        public event ObjectEventHandler<DataType> OnPreSave_DataType;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DataType != null) OnPostSave_DataType(this);
        }
        public event ObjectEventHandler<DataType> OnPostSave_DataType;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._ClassName, binStream);
            BinarySerializer.ToStream(this.fk_DefaultIcon, binStream);
            BinarySerializer.ToStream(this._DefaultIcon_pos, binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_Module, binStream);
            BinarySerializer.ToStream(this._Module_pos, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._ClassName, binStream);
            {
                var tmp = this.fk_DefaultIcon;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_DefaultIcon = tmp;
            }
            BinarySerializer.FromStream(out this._DefaultIcon_pos, binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_Module;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Module = tmp;
            }
            BinarySerializer.FromStream(out this._Module_pos, binStream);
        }

#endregion

    }


}