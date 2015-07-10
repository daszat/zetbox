// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A configuration object for instance lists
    /// </summary>
    [Zetbox.API.DefinitionGuid("46da717e-b6e0-4193-8580-8787fc4cf04f")]
    public interface SavedListConfiguration : IDataObject, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("2063f89a-89c8-48de-b191-b6dd870072c3")]
        string Configuration {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("776c6c41-d42d-43d0-bff4-e99638a045bf")]
        Zetbox.App.Base.Identity Owner {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("93775971-e361-4495-8107-3398205589ec")]
        Zetbox.App.Base.ObjectClass Type {
            get;
            set;
        }
    }
}
