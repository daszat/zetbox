
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
    /// Metadefinition Object for CLR Object Parameter.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CLRObjectParameter")]
    [System.Diagnostics.DebuggerDisplay("CLRObjectParameter")]
    public class CLRObjectParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, CLRObjectParameter
    {


        /// <summary>
        /// Assembly des CLR Objektes, NULL für Default Assemblies
        /// </summary>
    /*
    NewRelation: FK_CLRObjectParameter_Assembly_CLRObjectParameter_26 
    A: ZeroOrMore CLRObjectParameter as CLRObjectParameter (site: A, no Relation, prop ID=98)
    B: ZeroOrOne Assembly as Assembly (site: B, no Relation, prop ID=98)
    Preferred Storage: MergeA
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                return Assembly__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int fk_Assembly
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Assembly != null)
                {
                    _fk_Assembly = Assembly.ID;
                }
                return _fk_Assembly;
            }
            set
            {
                _fk_Assembly = value;
            }
        }
        private int _fk_Assembly;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_CLRObjectParameter_Assembly_CLRObjectParameter_26", "Assembly")]
        public Kistl.App.Base.Assembly__Implementation__ Assembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_CLRObjectParameter_Assembly_CLRObjectParameter_26",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_CLRObjectParameter_Assembly_CLRObjectParameter_26",
                        "Assembly");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Assembly__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// Name des CLR Datentypen
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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
                    NotifyPropertyChanging("FullTypeName");
                    _FullTypeName = value;
                    NotifyPropertyChanged("FullTypeName");;
                }
            }
        }
        private string _FullTypeName;

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_CLRObjectParameter != null)
            {
                OnGetParameterTypeString_CLRObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterTypeString_Handler<CLRObjectParameter> OnGetParameterTypeString_CLRObjectParameter;



        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_CLRObjectParameter != null)
            {
                OnGetParameterType_CLRObjectParameter(this, e);
            };
            return e.Result;
        }
		public event GetParameterType_Handler<CLRObjectParameter> OnGetParameterType_CLRObjectParameter;



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

#endregion

    }


}