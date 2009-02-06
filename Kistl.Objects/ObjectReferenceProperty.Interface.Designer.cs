
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    public interface ObjectReferenceProperty : Kistl.App.Base.Property 
    {

        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>

		Kistl.App.Base.ObjectClass ReferenceObjectClass { get; set; }
        /// <summary>
        /// This Property is the right Part of the selected Relation.
        /// </summary>

		Kistl.App.Base.Relation RightOf { get; set; }
        /// <summary>
        /// This Property is the left Part of the selected Relation.
        /// </summary>

		Kistl.App.Base.Relation LeftOf { get; set; }
    }
}