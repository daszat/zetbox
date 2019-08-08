// <autogenerated/>

namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("740f4a8b-32fa-48ba-84d9-6792a755d5c9")]
    public interface FileImportConfiguration : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// Restricts this configuration to a specific identity
        /// </summary>
        [Zetbox.API.DefinitionGuid("4762392e-1902-43ef-a023-9d57047892d9")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.Identity Identity {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Identity 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// Restricts this configuration to a specific machine
        /// </summary>
        [Zetbox.API.DefinitionGuid("f30f0053-0d08-4119-9775-284b6dee6bfd")]
        string MachineName {
            get;
            set;
        }


        /// <summary>
        /// Pickup directory for the file import service. Env Variables can be used with %VARIABLE%. e.g. %HOMEPATH%\MyPickupDir
        /// </summary>
        [Zetbox.API.DefinitionGuid("cd085ad4-58a4-43be-90b4-4988b728903b")]
        string PickupDirectory {
            get;
            set;
        }

    }
}
