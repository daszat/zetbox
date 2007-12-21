//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Projekt_Mitarbeiter", "A_Mitarbeiter", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Mitarbeiter), "B_Projekt", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Projekt))]

namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    using Kistl.API.Server;
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Projekt")]
    public class Projekt : BaseServerDataObject, ICloneable
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _Name;
        
        private int _fk_Mitarbeiter = Helper.INVALIDID;
        
        private System.Nullable<double> _AufwandGes;
        
        private string _Kundenname;
        
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        
        public override string EntitySetName
        {
            get
            {
                return "Projekt";
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Task_Projekt", "B_Task")]
        [XmlIgnore()]
        public EntityCollection<Kistl.App.Projekte.Task> Tasks
        {
            get
            {
                EntityCollection<Kistl.App.Projekte.Task> c = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedCollection<Kistl.App.Projekte.Task>("Model.FK_Task_Projekt", "B_Task");
                if (!c.IsLoaded) c.Load(); 
                return c;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Projekt_Mitarbeiter", "A_Mitarbeiter")]
        [XmlIgnore()]
        public Kistl.App.Projekte.Mitarbeiter Mitarbeiter
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Projekt_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Projekte.Mitarbeiter> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Mitarbeiter>("Model.FK_Projekt_Mitarbeiter", "A_Mitarbeiter");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded) r.Load(); 
                r.Value = value;
            }
        }
        
        public int fk_Mitarbeiter
        {
            get
            {
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && _fk_Mitarbeiter == Helper.INVALIDID && Mitarbeiter != null)
                {
                    _fk_Mitarbeiter = Mitarbeiter.ID;
                }
                return _fk_Mitarbeiter;
            }
            set
            {
                _fk_Mitarbeiter = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Nullable<double> AufwandGes
        {
            get
            {
                return _AufwandGes;
            }
            set
            {
                _AufwandGes = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public string Kundenname
        {
            get
            {
                return _Kundenname;
            }
            set
            {
                _Kundenname = value;
            }
        }
        
        public event ToStringHandler<Projekt> OnToString_Projekt;
        
        public event ObjectEventHandler<Projekt> OnPreSave_Projekt;
        
        public event ObjectEventHandler<Projekt> OnPostSave_Projekt;
        
        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Projekt != null)
            {
                OnToString_Projekt(this, e);
            }
            return e.Result;
        }
        
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Projekt != null) OnPreSave_Projekt(this);
        }
        
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Projekt != null) OnPostSave_Projekt(this);
        }
        
        public override object Clone()
        {
            Projekt obj = new Projekt();
            CopyTo(obj);
            return obj;
        }
        
        public void CopyTo(Projekt obj)
        {
            base.CopyTo(obj);
            obj.Name = this.Name;
            obj.fk_Mitarbeiter = this.fk_Mitarbeiter;
            obj.AufwandGes = this.AufwandGes;
            obj.Kundenname = this.Kundenname;
        }
    }
}
