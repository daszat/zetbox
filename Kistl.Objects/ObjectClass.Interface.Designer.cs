
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    public interface ObjectClass : Kistl.App.Base.DataType 
    {

        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
		Kistl.App.Base.ObjectClass BaseObjectClass {
			get;
			set;
		}
        /// <summary>
        /// The default model to use for the UI
        /// </summary>
		Kistl.App.Base.TypeRef DefaultModel {
			get;
			set;
		}
        /// <summary>
        /// The default PresentableModel to use for this ObjectClass
        /// </summary>
		Kistl.App.GUI.PresentableModelDescriptor DefaultPresentableModelDescriptor {
			get;
			set;
		}
        /// <summary>
        /// Interfaces der Objektklasse
        /// </summary>

        ICollection<Kistl.App.Base.Interface> ImplementsInterfaces { get; }
        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
		bool IsFrozenObject {
			get;
			set;
		}
        /// <summary>
        /// Setting this to true marks the instances of this class as "simple." At first this will only mean that they'll be displayed inline.
        /// </summary>
		bool IsSimpleObject {
			get;
			set;
		}
        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>

        ICollection<Kistl.App.Base.ObjectClass> SubClasses { get; }
        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
		string TableName {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>

		 IList<Kistl.App.Base.Method> GetInheritedMethods() ;
    }
}