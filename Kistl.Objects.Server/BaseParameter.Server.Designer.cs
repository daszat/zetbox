
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
    /// Metadefinition Object for Parameter. This class is abstract.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="BaseParameter")]
    [System.Diagnostics.DebuggerDisplay("BaseParameter")]
    public class BaseParameter__Implementation__ : BaseServerDataObject_EntityFramework, BaseParameter
    {
    
		public BaseParameter__Implementation__()
		{
            {
            }
        }

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
        /// Description of this Parameter
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
        /// Parameter wird als List<> generiert
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsList
        {
            get
            {
                return _IsList;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsList != value)
                {
                    NotifyPropertyChanging("IsList");
                    _IsList = value;
                    NotifyPropertyChanged("IsList");
                }
            }
        }
        private bool _IsList;

        /// <summary>
        /// Es darf nur ein Return Parameter angegeben werden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual bool IsReturnParameter
        {
            get
            {
                return _IsReturnParameter;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsReturnParameter != value)
                {
                    NotifyPropertyChanging("IsReturnParameter");
                    _IsReturnParameter = value;
                    NotifyPropertyChanged("IsReturnParameter");
                }
            }
        }
        private bool _IsReturnParameter;

        /// <summary>
        /// Methode des Parameters
        /// </summary>
    /*
    Relation: FK_Method_BaseParameter_Method_44
    A: One Method as Method
    B: ZeroOrMore BaseParameter as Parameter
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
        [EdmRelationshipNavigationProperty("Model", "FK_Method_BaseParameter_Method_44", "Method")]
        public Kistl.App.Base.Method__Implementation__ Method__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Method__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Method__Implementation__>(
                        "Model.FK_Method_BaseParameter_Method_44",
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
                        "Model.FK_Method_BaseParameter_Method_44",
                        "Method");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Method__Implementation__)value;
            }
        }
        
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int? Method_pos
        {
            get
            {
                return _Method_pos;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method_pos != value)
                {
                    NotifyPropertyChanging("Method_pos");
                    _Method_pos = value;
                    NotifyPropertyChanged("Method_pos");
                }
            }
        }
        private int? _Method_pos;
        

        /// <summary>
        /// Name des Parameter
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string ParameterName
        {
            get
            {
                return _ParameterName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ParameterName != value)
                {
                    NotifyPropertyChanging("ParameterName");
                    _ParameterName = value;
                    NotifyPropertyChanged("ParameterName");
                }
            }
        }
        private string _ParameterName;

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public virtual System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_BaseParameter != null)
            {
                OnGetParameterType_BaseParameter(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseParameter.GetParameterType");
            }
            return e.Result;
        }
		public delegate void GetParameterType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret);
		public event GetParameterType_Handler<BaseParameter> OnGetParameterType_BaseParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public virtual string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_BaseParameter != null)
            {
                OnGetParameterTypeString_BaseParameter(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on BaseParameter.GetParameterTypeString");
            }
            return e.Result;
        }
		public delegate void GetParameterTypeString_Handler<T>(T obj, MethodReturnEventArgs<string> ret);
		public event GetParameterTypeString_Handler<BaseParameter> OnGetParameterTypeString_BaseParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(BaseParameter));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BaseParameter != null)
            {
                OnToString_BaseParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<BaseParameter> OnToString_BaseParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BaseParameter != null) OnPreSave_BaseParameter(this);
        }
        public event ObjectEventHandler<BaseParameter> OnPreSave_BaseParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BaseParameter != null) OnPostSave_BaseParameter(this);
        }
        public event ObjectEventHandler<BaseParameter> OnPostSave_BaseParameter;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Method.HasValue)
				Method__Implementation__ = (Kistl.App.Base.Method__Implementation__)Context.Find<Kistl.App.Base.Method>(_fk_Method.Value);
			else
				Method__Implementation__ = null;
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this._IsList, binStream);
            BinarySerializer.ToStream(this._IsReturnParameter, binStream);
            BinarySerializer.ToStream(this.fk_Method, binStream);
            BinarySerializer.ToStream(this._Method_pos, binStream);
            BinarySerializer.ToStream(this._ParameterName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            BinarySerializer.FromStream(out this._IsList, binStream);
            BinarySerializer.FromStream(out this._IsReturnParameter, binStream);
            {
                var tmp = this.fk_Method;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Method = tmp;
            }
            BinarySerializer.FromStream(out this._Method_pos, binStream);
            BinarySerializer.FromStream(out this._ParameterName, binStream);
        }

#endregion

    }


}