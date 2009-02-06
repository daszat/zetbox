
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for Properties. This class is abstract.
    /// </summary>
    public interface BaseProperty : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>

		Kistl.App.Base.DataType ObjectClass { get; set; }
        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>

		Kistl.App.Base.Module Module { get; set; }
        /// <summary>
        /// The list of constraints applying to this Property
        /// </summary>

        ICollection<Kistl.App.Base.Constraint> Constraints { get; }
        /// <summary>
        /// 
        /// </summary>

		string PropertyName { get; set; }
        /// <summary>
        /// 
        /// </summary>

		string AltText { get; set; }
        /// <summary>
        /// Description of this Property
        /// </summary>

		string Description { get; set; }
        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		 string GetPropertyTypeString() ;
        /// <summary>
        /// 
        /// </summary>

		 string GetGUIRepresentation() ;
        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		 System.Type GetPropertyType() ;
    }
}