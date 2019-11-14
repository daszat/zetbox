// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A single entry in the auditing table of a class
    /// </summary>
    [Zetbox.API.DefinitionGuid("84d3d914-cb79-41fe-8aae-54128e7edb87")]
    public interface AuditEntry : ICompoundObject 
    {

        /// <summary>
        /// The identity that caused this audit
        /// </summary>
        [Zetbox.API.DefinitionGuid("7d954dd7-200c-4465-9316-c84ab920c30c")]
        string Identity {
            get;
            set;
        }


        /// <summary>
        /// The default format to display this change to the user
        /// </summary>
        [Zetbox.API.DefinitionGuid("0b4cba95-3c68-4370-8ba5-59b2b110c569")]
        string MessageFormat {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("1bd6c210-e746-4ed8-a786-fc3911624ee7")]
        string NewValue {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("10227950-4897-48fa-8a25-53db7cac9aab")]
        string OldValue {
            get;
            set;
        }


        /// <summary>
        /// The name of the changed property
        /// </summary>
        [Zetbox.API.DefinitionGuid("0b9d7bd4-ff9e-4662-913d-6eb3e9d05971")]
        string PropertyName {
            get;
            set;
        }


        /// <summary>
        /// The time when the change was made
        /// </summary>
        [Zetbox.API.DefinitionGuid("c9461d28-4594-443e-82ec-a2ddc9664742")]
        DateTime? Timestamp {
            get;
            set;
        }

    }
}
