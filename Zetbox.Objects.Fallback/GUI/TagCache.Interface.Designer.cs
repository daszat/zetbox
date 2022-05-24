// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Cache entity for property tags
    /// </summary>
    [Zetbox.API.DefinitionGuid("891c1e32-7545-49d5-9c14-da0e7e061e8f")]
    public interface TagCache : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("3fe05228-66d6-42dd-8a0f-526ba4ac4503")]
        string Name {
            get;
            set;
        }


        /// <summary>
        /// Rebuilds the tag cache
        /// </summary>
        System.Threading.Tasks.Task Rebuild();
    }
}
