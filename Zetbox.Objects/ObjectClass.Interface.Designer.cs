// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for ObjectClasses.
    /// </summary>
    [Zetbox.API.DefinitionGuid("20888dfc-1fbc-47c8-9f3c-c6a30a5c0048")]
    public interface ObjectClass : Zetbox.App.Base.DataType, Zetbox.App.Base.INamedObject 
    {

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("4514093c-0a1f-4644-b4a6-3389f1ca7aa8")]
        ICollection<Zetbox.App.Base.AccessControl> AccessControlList { get; }

        /// <summary>
        /// Pointer auf die Basisklasse
        /// </summary>
        [Zetbox.API.DefinitionGuid("ad060d41-bc7a-41b8-a3e3-ec9302c8c714")]
        Zetbox.App.Base.ObjectClass BaseObjectClass {
            get;
            set;
        }

        /// <summary>
        /// Provides a code template for default methods
        /// </summary>
        [Zetbox.API.DefinitionGuid("7afdb672-f364-4b05-ad5d-ea6d59dc3553")]
        string CodeTemplate {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("53ca9d62-07c4-4bce-a798-9dd2064b9f31")]
        Zetbox.App.Base.Property DefaultSortProperty {
            get;
            set;
        }

        /// <summary>
        /// The default ViewModel to use for this ObjectClass
        /// </summary>
        [Zetbox.API.DefinitionGuid("11adedb9-d32a-4da9-b986-0534e65df760")]
        Zetbox.App.GUI.ViewModelDescriptor DefaultViewModelDescriptor {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("bd526c1f-a6ac-40b6-8f81-66aaf553129f")]
        ICollection<Zetbox.App.GUI.ObjectClassFilterConfiguration> FilterConfigurations { get; }

        /// <summary>
        /// Class is abstract
        /// </summary>
        [Zetbox.API.DefinitionGuid("e9d1402e-3580-4084-8836-c44844683191")]
        bool IsAbstract {
            get;
            set;
        }

        /// <summary>
        /// if true then all Instances appear in FozenContext.
        /// </summary>
        [Zetbox.API.DefinitionGuid("13c33710-ea02-4621-ad50-294a1f36b07d")]
        bool IsFrozenObject {
            get;
            set;
        }

        /// <summary>
        /// Setting this to true marks the instances of this class as &amp;quot;simple.&amp;quot; At first this will only mean that they&apos;ll be displayed inline.
        /// </summary>
        [Zetbox.API.DefinitionGuid("edc853d3-0d02-4492-9159-c548c7713e9b")]
        bool IsSimpleObject {
            get;
            set;
        }

        /// <summary>
        /// Liste der vererbten Klassen
        /// </summary>

        [Zetbox.API.DefinitionGuid("0914de6e-966c-46fc-9359-e4da6c3608b1")]
        ICollection<Zetbox.App.Base.ObjectClass> SubClasses { get; }

        /// <summary>
        /// Kind of table mapping. Only valid on base classes. Default is TPT.
        /// </summary>
        [Zetbox.API.DefinitionGuid("8002bbe3-68b6-475b-b929-398744cc2398")]
        Zetbox.App.Base.TableMapping? TableMapping {
            get;
            set;
        }

        /// <summary>
        /// Tabellenname in der Datenbank
        /// </summary>
        [Zetbox.API.DefinitionGuid("2a5e5111-199c-4dce-8369-ce35ee741568")]
        string TableName {
            get;
            set;
        }

        /// <summary>
        /// Creates a new Method for this class
        /// </summary>
        Zetbox.App.Base.Method CreateMethod();

        /// <summary>
        /// Implements the &quot;Create new Relation&quot; use case
        /// </summary>
        Zetbox.App.Base.Relation CreateRelation();

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Zetbox.App.Base.Method> GetInheritedMethods();
    }
}
