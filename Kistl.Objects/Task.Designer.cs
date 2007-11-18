//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Data.Objects.DataClasses.EdmRelationshipAttribute("Model", "FK_Task_Projekt", "Projekt", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Kistl.App.Projekte.Projekt), "Task", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Kistl.App.Projekte.Task))]

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
    
    
    [EdmEntityTypeAttribute(NamespaceName="Model", Name="Task")]
    public sealed class Task : BaseDataObject
    {
        
        private int _ID = Helper.INVALIDID;
        
        private string _Name;
        
        private System.Nullable<System.DateTime> _DatumVon;
        
        private System.Nullable<System.DateTime> _DatumBis;
        
        private System.Nullable<double> _Aufwand;
        
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
        
        [EdmScalarPropertyAttribute()]
        public System.Nullable<System.DateTime> DatumVon
        {
            get
            {
                return _DatumVon;
            }
            set
            {
                _DatumVon = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Nullable<System.DateTime> DatumBis
        {
            get
            {
                return _DatumBis;
            }
            set
            {
                _DatumBis = value;
            }
        }
        
        [EdmScalarPropertyAttribute()]
        public System.Nullable<double> Aufwand
        {
            get
            {
                return _Aufwand;
            }
            set
            {
                _Aufwand = value;
            }
        }
        
        [EdmRelationshipNavigationPropertyAttribute("Model", "FK_Task_Projekt", "Projekt")]
        [XmlIgnore()]
        public Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                EntityReference<Kistl.App.Projekte.Projekt> r = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt>("Model.FK_Task_Projekt", "Projekt");
                if (!r.IsLoaded) r.Load(); 
                return r.Value;
            }
            set
            {
                ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Projekte.Projekt>("Model.FK_Task_Projekt", "Projekt").Value = value;
            }
        }
        
        public event ToStringHandler<Task> OnToString;
        
        public event ObjectEventHandler<Task> OnPreSave;
        
        public event ObjectEventHandler<Task> OnPostSave;
        
        public override string ToString()
        {
            if (OnToString != null)
            {
                ToStringEventArgs e = new ToStringEventArgs();
                OnToString(this, e);
                return e.Result;
            }
            return base.ToString();
        }
        
        public override void NotifyPreSave()
        {
            if (OnPreSave != null) OnPreSave(this);
        }
        
        public override void NotifyPostSave()
        {
            if (OnPostSave != null) OnPostSave(this);
        }
    }
}
