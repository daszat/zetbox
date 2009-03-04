
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for Methods.
    /// </summary>
    public interface Method : IDataObject 
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
        /// Methodenaufrufe implementiert in dieser Objekt Klasse
        /// </summary>

        ICollection<Kistl.App.Base.MethodInvocation> MethodInvokations { get; }
        /// <summary>
        /// Parameter der Methode
        /// </summary>

        IList<Kistl.App.Base.BaseParameter> Parameter { get; }
        /// <summary>
        /// 
        /// </summary>

		string MethodName { get; set; }
        /// <summary>
        /// Shows this Method in th GUI
        /// </summary>

		bool IsDisplayable { get; set; }
        /// <summary>
        /// Description of this Method
        /// </summary>

		string Description { get; set; }
        /// <summary>
        /// Returns the Return Parameter Meta Object of this Method Meta Object.
        /// </summary>

		 Kistl.App.Base.BaseParameter GetReturnParameter() ;
    }
}