// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// a object reference that is calculated from the contents of the containing class
    /// </summary>
    [Zetbox.API.DefinitionGuid("8708c578-6e55-4349-ba24-ede46ca6f585")]
    public interface CalculatedObjectReferenceProperty : Zetbox.App.Base.Property 
    {

        /// <summary>
        /// The properties on which the calculation depends. This is used to propagate change notifications.
        /// </summary>

        [Zetbox.API.DefinitionGuid("bfda6511-087d-4381-9780-1f76f3abcffe")]
        ICollection<Zetbox.App.Base.Property> Inputs { get; }

        /// <summary>
        /// the referenced class of objects
        /// </summary>
        [Zetbox.API.DefinitionGuid("cd62d769-0752-4a72-832f-5935ece1198b")]
        Zetbox.App.Base.ObjectClass ReferencedClass {
            get;
            set;
        }
    }
}
