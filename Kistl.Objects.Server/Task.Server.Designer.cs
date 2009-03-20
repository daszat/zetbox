
namespace Kistl.App.Projekte
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
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Task")]
    [System.Diagnostics.DebuggerDisplay("Task")]
    public class Task__Implementation__ : BaseServerDataObject_EntityFramework, Task
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
        /// Aufwand in Stunden
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual double? Aufwand
        {
            get
            {
                return _Aufwand;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Aufwand != value)
                {
                    NotifyPropertyChanging("Aufwand");
                    _Aufwand = value;
                    NotifyPropertyChanged("Aufwand");
                }
            }
        }
        private double? _Aufwand;

        /// <summary>
        /// Enddatum
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime? DatumBis
        {
            get
            {
                return _DatumBis;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumBis != value)
                {
                    NotifyPropertyChanging("DatumBis");
                    _DatumBis = value;
                    NotifyPropertyChanged("DatumBis");
                }
            }
        }
        private DateTime? _DatumBis;

        /// <summary>
        /// Start Datum
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
        public virtual DateTime? DatumVon
        {
            get
            {
                return _DatumVon;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumVon != value)
                {
                    NotifyPropertyChanging("DatumVon");
                    _DatumVon = value;
                    NotifyPropertyChanged("DatumVon");
                }
            }
        }
        private DateTime? _DatumVon;

        /// <summary>
        /// Taskname
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
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>
    /*
    Relation: FK_Projekt_Task_Projekt_22
    A: ZeroOrOne Projekt as Projekt
    B: ZeroOrMore Task as Tasks
    Preferred Storage: Right
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return Projekt__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Projekt
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) 
                    && Projekt != null)
                {
                    _fk_Projekt = Projekt.ID;
                }
                return _fk_Projekt;
            }
            set
            {
                _fk_Projekt = value;
            }
        }
        private int? _fk_Projekt;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Projekt_Task_Projekt_22", "Projekt")]
        public Kistl.App.Projekte.Projekt__Implementation__ Projekt__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Task_Projekt_22",
                        "Projekt");
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
                EntityReference<Kistl.App.Projekte.Projekt__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt__Implementation__>(
                        "Model.FK_Projekt_Task_Projekt_22",
                        "Projekt");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Projekte.Projekt__Implementation__)value;
            }
        }
        
        

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Task));
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Task != null)
            {
                OnToString_Task(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Task> OnToString_Task;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Task != null) OnPreSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPreSave_Task;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Task != null) OnPostSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPostSave_Task;



		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_Projekt.HasValue)
				Projekt__Implementation__ = (Kistl.App.Projekte.Projekt__Implementation__)Context.Find<Kistl.App.Projekte.Projekt>(_fk_Projekt.Value);
			else
				Projekt__Implementation__ = null;
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Aufwand, binStream);
            BinarySerializer.ToStream(this._DatumBis, binStream);
            BinarySerializer.ToStream(this._DatumVon, binStream);
            BinarySerializer.ToStream(this._Name, binStream);
            BinarySerializer.ToStream(this.fk_Projekt, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Aufwand, binStream);
            BinarySerializer.FromStream(out this._DatumBis, binStream);
            BinarySerializer.FromStream(out this._DatumVon, binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
            {
                var tmp = this.fk_Projekt;
                BinarySerializer.FromStream(out tmp, binStream);
                this.fk_Projekt = tmp;
            }
        }

#endregion

    }


}