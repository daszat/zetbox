
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
    /// Metadefinition Object for a MethodInvocation on a Method of a DataType.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="MethodInvocation")]
    [System.Diagnostics.DebuggerDisplay("MethodInvocation")]
    public class MethodInvocation__Implementation__ : BaseServerDataObject_EntityFramework, MethodInvocation
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
        /// The Type implementing this invocation
        /// </summary>
    /*
    Relation: FK_MethodInvocation_TypeRef_MethodInvocation_67
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne TypeRef as Implementor
    Preferred Storage: Left
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                return Implementor__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Implementor__Implementation__ = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Implementor
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Implementor != null)
                {
                    _fk_Implementor = Implementor.ID;
                }
                return _fk_Implementor;
            }
            set
            {
                _fk_Implementor = value;
            }
        }
        private int? _fk_Implementor;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_MethodInvocation_TypeRef_MethodInvocation_67", "Implementor")]
        public Kistl.App.Base.TypeRef__Implementation__ Implementor__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_MethodInvocation_TypeRef_MethodInvocation_67",
                        "Implementor");
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
                EntityReference<Kistl.App.Base.TypeRef__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.TypeRef__Implementation__>(
                        "Model.FK_MethodInvocation_TypeRef_MethodInvocation_67",
                        "Implementor");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.TypeRef__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// In dieser Objektklasse implementieren
        /// </summary>
    /*
    Relation: FK_DataType_MethodInvocation_InvokeOnObjectClass_41
    A: One DataType as InvokeOnObjectClass
    B: ZeroOrMore MethodInvocation as MethodInvocations
    Preferred Storage: Right
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                return InvokeOnObjectClass__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                InvokeOnObjectClass__Implementation__ = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_InvokeOnObjectClass
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && InvokeOnObjectClass != null)
                {
                    _fk_InvokeOnObjectClass = InvokeOnObjectClass.ID;
                }
                return _fk_InvokeOnObjectClass;
            }
            set
            {
                _fk_InvokeOnObjectClass = value;
            }
        }
        private int? _fk_InvokeOnObjectClass;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_DataType_MethodInvocation_InvokeOnObjectClass_41", "InvokeOnObjectClass")]
        public Kistl.App.Base.DataType__Implementation__ InvokeOnObjectClass__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.DataType__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.DataType__Implementation__>(
                        "Model.FK_DataType_MethodInvocation_InvokeOnObjectClass_41",
                        "InvokeOnObjectClass");
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
                        "Model.FK_DataType_MethodInvocation_InvokeOnObjectClass_41",
                        "InvokeOnObjectClass");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.DataType__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Name des implementierenden Members
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MemberName != value)
                {
                    NotifyPropertyChanging("MemberName");
                    _MemberName = value;
                    NotifyPropertyChanged("MemberName");
                }
            }
        }
        private string _MemberName;

        /// <summary>
        /// Methode, die Aufgerufen wird
        /// </summary>
    /*
    Relation: FK_Method_MethodInvocation_Method_39
    A: One Method as Method
    B: ZeroOrMore MethodInvocation as MethodInvokations
    Preferred Storage: Right
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                return Method__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Method__Implementation__ = (Kistl.App.Base.Method__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Method != null)
                {
                    _fk_Method = Method.ID;
                }
                return _fk_Method;
            }
            set
            {
                _fk_Method = value;
            }
        }
        private int? _fk_Method;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Method_MethodInvocation_Method_39", "Method")]
        public Kistl.App.Base.Method__Implementation__ Method__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Method_MethodInvocation_Method_39",
                        "Method");
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
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Method_MethodInvocation_Method_39",
                        "Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Method__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
    /*
    Relation: FK_MethodInvocation_Module_MethodInvocation_40
    A: ZeroOrMore MethodInvocation as MethodInvocation
    B: ZeroOrOne Module as Module
    Preferred Storage: Left
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
        [EdmRelationshipNavigationProperty("Model", "FK_MethodInvocation_Module_MethodInvocation_40", "Module")]
        public Kistl.App.Base.Module__Implementation__ Module__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Module__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Module__Implementation__>(
                        "Model.FK_MethodInvocation_Module_MethodInvocation_40",
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
                        "Model.FK_MethodInvocation_Module_MethodInvocation_40",
                        "Module");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Module__Implementation__)value;
            }
        }
        
        

		public override Type GetInterfaceType()
		{
			return typeof(MethodInvocation);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocation != null)
            {
                OnToString_MethodInvocation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<MethodInvocation> OnToString_MethodInvocation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocation != null) OnPreSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPreSave_MethodInvocation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocation != null) OnPostSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPostSave_MethodInvocation;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this.fk_Implementor, binStream);
            BinarySerializer.ToStream(this.fk_InvokeOnObjectClass, binStream);
            BinarySerializer.ToStream(this._MemberName, binStream);
            BinarySerializer.ToStream(this.fk_Method, binStream);
            BinarySerializer.ToStream(this.fk_Module, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            {
                var tmp = this.fk_Implementor;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Implementor = tmp;
            }
            {
                var tmp = this.fk_InvokeOnObjectClass;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_InvokeOnObjectClass = tmp;
            }
            BinarySerializer.FromStream(out this._MemberName, binStream);
            {
                var tmp = this.fk_Method;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Method = tmp;
            }
            {
                var tmp = this.fk_Module;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Module = tmp;
            }
        }

#endregion

    }


}