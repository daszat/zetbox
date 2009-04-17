
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
    /// Metadefinition Object for an Enumeration Entry.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="EnumerationEntry")]
    [System.Diagnostics.DebuggerDisplay("EnumerationEntry")]
    public class EnumerationEntry__Implementation__ : BaseServerDataObject_EntityFramework, EnumerationEntry
    {
    
		public EnumerationEntry__Implementation__()
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
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// Description of this Enumeration Entry
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
					var __oldValue = _Description;
                    NotifyPropertyChanging("Description", __oldValue, value);
                    _Description = value;
                    NotifyPropertyChanged("Description", __oldValue, value);
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Übergeordnete Enumeration
        /// </summary>
    /*
    Relation: FK_Enumeration_EnumerationEntry_Enumeration_47
    A: One Enumeration as Enumeration
    B: ZeroOrMore EnumerationEntry as EnumerationEntries
    Preferred Storage: Right
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Enumeration Enumeration
        {
            get
            {
                return Enumeration__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Enumeration__Implementation__ = (Kistl.App.Base.Enumeration__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Enumeration
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Enumeration != null)
                {
                    _fk_Enumeration = Enumeration.ID;
                }
                return _fk_Enumeration;
            }
            set
            {
                _fk_Enumeration = value;
            }
        }
        private int? _fk_Enumeration;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Enumeration_EnumerationEntry_Enumeration_47", "Enumeration")]
        public Kistl.App.Base.Enumeration__Implementation__ Enumeration__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Enumeration__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Enumeration__Implementation__>(
                        "Model.FK_Enumeration_EnumerationEntry_Enumeration_47",
                        "Enumeration");
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
                EntityReference<Kistl.App.Base.Enumeration__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Enumeration__Implementation__>(
                        "Model.FK_Enumeration_EnumerationEntry_Enumeration_47",
                        "Enumeration");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Enumeration__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// CLR name of this entry
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
					var __oldValue = _Name;
                    NotifyPropertyChanging("Name", __oldValue, value);
                    _Name = value;
                    NotifyPropertyChanged("Name", __oldValue, value);
                }
            }
        }
        private string _Name;

        /// <summary>
        /// The CLR value of this entry
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Value != value)
                {
					var __oldValue = _Value;
                    NotifyPropertyChanging("Value", __oldValue, value);
                    _Value = value;
                    NotifyPropertyChanged("Value", __oldValue, value);
                }
            }
        }
        private int _Value;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(EnumerationEntry));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (EnumerationEntry)obj;
			var otherImpl = (EnumerationEntry__Implementation__)obj;
			var me = (EnumerationEntry)this;

			me.Description = other.Description;
			me.Name = other.Name;
			me.Value = other.Value;
			this.fk_Enumeration = otherImpl.fk_Enumeration;
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_EnumerationEntry != null)
            {
                OnToString_EnumerationEntry(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<EnumerationEntry> OnToString_EnumerationEntry;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_EnumerationEntry != null) OnPreSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPreSave_EnumerationEntry;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_EnumerationEntry != null) OnPostSave_EnumerationEntry(this);
        }
        public event ObjectEventHandler<EnumerationEntry> OnPostSave_EnumerationEntry;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Enumeration.HasValue)
				Enumeration__Implementation__ = (Kistl.App.Base.Enumeration__Implementation__)Context.Find<Kistl.App.Base.Enumeration>(_fk_Enumeration.Value);
			else
				Enumeration__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Description, binStream);
            BinarySerializer.ToStream(this.fk_Enumeration, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this._Value, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Description, binStream);
            {
                var tmp = this.fk_Enumeration;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Enumeration = tmp;
            }
            BinarySerializer.FromStream(out this._Name, binStream);
            BinarySerializer.FromStream(out this._Value, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._Description, xml, "Description", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this.fk_Enumeration, xml, "fk_Enumeration", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Name, xml, "Name", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Value, xml, "Value", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
            // TODO: Add XML Serializer here
        }

#endregion

    }


}