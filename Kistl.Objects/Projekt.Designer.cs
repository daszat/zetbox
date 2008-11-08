//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kistl.App.Projekte
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Xml.Serialization;
    using Kistl.API;
    
    
    /// <summary>
    /// Mapped to: Projekte
    /// TODO: Add description to a DataType
    /// </summary>
    public interface Projekt : IDataObject
    {
        
        /// <summary>
        /// Projektname
        /// </summary>
        string Name
        {
            get;
            set;
        }
        
        ICollection<Kistl.App.Projekte.Task> Tasks
        {
            get;
        }
        
        IList<Kistl.App.Projekte.Mitarbeiter> Mitarbeiter
        {
            get;
        }
        
        System.Double? AufwandGes
        {
            get;
            set;
        }
        
        /// <summary>
        /// Bitte geben Sie den Kundennamen ein
        /// </summary>
        string Kundenname
        {
            get;
            set;
        }
        
        /// <summary>
        /// Kostenträger
        /// </summary>
        ICollection<Kistl.App.Zeiterfassung.Kostentraeger> Kostentraeger
        {
            get;
        }
        
        /// <summary>
        /// Aufträge
        /// </summary>
        ICollection<Kistl.App.Projekte.Auftrag> Auftraege
        {
            get;
        }
    }
}
