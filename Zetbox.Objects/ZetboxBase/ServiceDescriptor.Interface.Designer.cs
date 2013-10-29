// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Descriptor Object for Zetbox Services
    /// </summary>
    [Zetbox.API.DefinitionGuid("d1bf8a7e-a8c0-435b-9dfe-b5ab61e71d1a")]
    public interface ServiceDescriptor : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IModuleMember 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("0ddea895-aca0-41ff-ada3-37e99100d081")]
        Zetbox.App.Base.DeploymentRestriction? DeploymentRestriction {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("10e8c65c-ca0f-4ed6-b830-457117c30c42")]
        string Description {
            get;
            set;
        }

        /// <summary>
        /// Export Guid
        /// </summary>
        [Zetbox.API.DefinitionGuid("93a1fd7b-b7ba-475a-a9bd-7ddaeb1ccc14")]
        Guid ExportGuid {
            get;
            set;
        }
    }
}