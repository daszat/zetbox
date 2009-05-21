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
    
		public CLRObjectParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Assembly des CLR Objektes, NULL für Default Assemblies
        /// </summary>
    /*
    Relation: FK_CLRObjectParameter_Assembly_CLRObjectParameter_46
    A: ZeroOrMore CLRObjectParameter as CLRObjectParameter
    B: ZeroOrOne Assembly as Assembly
    Preferred Storage: MergeIntoA
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
        
        private int? _fk_Assembly;
        private Guid? _fk_guid_Assembly = null;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_CLRObjectParameter_Assembly_CLRObjectParameter_46", "Assembly")]
        public Kistl.App.Base.Assembly__Implementation__ Assembly__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_CLRObjectParameter_Assembly_CLRObjectParameter_46",
                        "Assembly");
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
                EntityReference<Kistl.App.Base.Assembly__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Assembly__Implementation__>(
                        "Model.FK_CLRObjectParameter_Assembly_CLRObjectParameter_46",
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
			this._fk_Assembly = otherImpl._fk_Assembly;
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

		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references

			if (_fk_guid_Assembly.HasValue)
				Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.FindPersistenceObject<Kistl.App.Base.Assembly>(_fk_guid_Assembly.Value);
			else if (_fk_Assembly.HasValue)
				Assembly__Implementation__ = (Kistl.App.Base.Assembly__Implementation__)Context.Find<Kistl.App.Base.Assembly>(_fk_Assembly.Value);
			else
				Assembly__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
			
            base.ToStream(binStream);
            BinarySerializer.ToStream(Assembly != null ? Assembly.ID : (int?)null, binStream);
            BinarySerializer.ToStream(this._FullTypeName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
			
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._FullTypeName, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
			
            base.ToStream(xml);
            XmlStreamer.ToStream(Assembly != null ? Assembly.ID : (int?)null, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.ToStream(this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
			
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_Assembly, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
			
            base.Export(xml, modules);
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(Assembly != null ? Assembly.ExportGuid : (Guid?)null, xml, "Assembly", "Kistl.App.Base");
	
            if (modules.Contains("*") || modules.Contains("Kistl.App.Base")) XmlStreamer.ToStream(this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
			
            base.MergeImport(xml);
            XmlStreamer.FromStream(ref this._fk_guid_Assembly, xml, "Assembly", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._FullTypeName, xml, "FullTypeName", "Kistl.App.Base");
        }

#endregion

    }


}