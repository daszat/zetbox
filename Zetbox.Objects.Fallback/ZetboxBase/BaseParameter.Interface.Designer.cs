// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for Parameter. This class is abstract.
    /// </summary>
    [Zetbox.API.DefinitionGuid("63b8e3f7-e663-4fde-a09a-64ca876586bd")]
    public interface BaseParameter : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// Description of this Parameter
        /// </summary>
        [Zetbox.API.DefinitionGuid("20668b5a-ecaa-4531-81d8-6e50c9858ff0")]
        string Description {
            get;
            set;
        }

        /// <summary>
        /// A HTML string with a help text
        /// </summary>
        [Zetbox.API.DefinitionGuid("10e35458-34d5-4e16-ba7b-9729d9e5d1e9")]
        string HelpText {
            get;
            set;
        }

        /// <summary>
        /// Parameter wird als List&amp;lt;&amp;gt; generiert
        /// </summary>
        [Zetbox.API.DefinitionGuid("ec4d5dbc-f738-4eb3-a663-2328d0baa79c")]
        bool IsList {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("dfa5d0ec-ce8b-4bb7-ab5b-fde21f56ad3a")]
        bool IsNullable {
            get;
            set;
        }

        /// <summary>
        /// Es darf nur ein Return Parameter angegeben werden
        /// </summary>
        [Zetbox.API.DefinitionGuid("ba5bfb2e-f679-41b2-93ef-fc795e2e92d4")]
        bool IsReturnParameter {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("fcf1e2ff-470a-4abe-9e44-e7f3dc1a5c95")]
        string Label {
            get;
            set;
        }

        /// <summary>
        /// Methode des Parameters
        /// </summary>
        [Zetbox.API.DefinitionGuid("29d7eba7-6b87-438a-910d-1a2bf17d8215")]
        Zetbox.App.Base.Method Method {
            get;
            set;
        }

        /// <summary>
        /// Name des Parameter
        /// </summary>
        [Zetbox.API.DefinitionGuid("25c82fbd-cf5d-4021-b549-fccb46e166b3")]
        string Name {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        string GetLabel();

        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
        System.Type GetParameterType();

        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
        string GetParameterTypeString();
    }
}