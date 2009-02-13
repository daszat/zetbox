
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
    /// </summary>
    public interface DataType : IDataObject 
    {

        /// <summary>
        /// Der Name der Objektklasse
        /// </summary>

		string ClassName { get; set; }
        /// <summary>
        /// Eigenschaften der Objektklasse
        /// </summary>

        ICollection<Kistl.App.Base.BaseProperty> Properties { get; }
        /// <summary>
        /// Liste aller Methoden der Objektklasse.
        /// </summary>

        ICollection<Kistl.App.Base.Method> Methods { get; }
        /// <summary>
        /// Modul der Objektklasse
        /// </summary>

		Kistl.App.Base.Module Module { get; set; }
        /// <summary>
        /// Standard Icon wenn IIcon nicht implementiert ist
        /// </summary>

		Kistl.App.GUI.Icon DefaultIcon { get; set; }
        /// <summary>
        /// all implemented Methods in this DataType
        /// </summary>

        ICollection<Kistl.App.Base.MethodInvocation> MethodInvocations { get; }
        /// <summary>
        /// Description of this DataType
        /// </summary>

		string Description { get; set; }
        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		 string GetDataTypeString() ;
        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		 System.Type GetDataType() ;
    }
}