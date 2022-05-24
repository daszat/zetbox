// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("78b6f354-013b-4129-a390-7f3a5a5e28e9")]
    public interface Icon : IDataObject, Zetbox.App.Base.IExportable, Zetbox.App.Base.IModuleMember, Zetbox.App.Base.INamedObject 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("f4dfb868-260d-450b-84b8-833dac4d25ee")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.Blob Blob {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Blob 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Base.Blob> GetProp_Blob();

        System.Threading.Tasks.Task SetProp_Blob(Zetbox.App.Base.Blob newValue);

        /// <summary>
        /// Filename of the Icon
        /// </summary>
        [Zetbox.API.DefinitionGuid("cdbdfc01-5faa-416b-960f-2eb220f268fe")]
        string IconFile {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Open();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Upload();
    }
}
