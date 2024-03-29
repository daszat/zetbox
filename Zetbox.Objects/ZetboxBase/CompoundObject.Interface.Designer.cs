// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for Compound Objects.
    /// </summary>
    [Zetbox.API.DefinitionGuid("2cb3f778-dd6a-46c7-ad2b-5f8691313035")]
    public interface CompoundObject : Zetbox.App.Base.DataType, Zetbox.App.Base.INamedObject 
    {

        /// <summary>
        /// An optional default ViewModelDescriptor for Properties of this type
        /// </summary>
        [Zetbox.API.DefinitionGuid("908757d2-053b-40c5-89f8-9e5f79b5fe83")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.GUI.ViewModelDescriptor DefaultPropertyViewModelDescriptor {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_DefaultPropertyViewModelDescriptor 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.GUI.ViewModelDescriptor> GetProp_DefaultPropertyViewModelDescriptor();

        System.Threading.Tasks.Task SetProp_DefaultPropertyViewModelDescriptor(Zetbox.App.GUI.ViewModelDescriptor newValue);

        /// <summary>
        /// The default ViewModel to use for this Compound Object
        /// </summary>
        [Zetbox.API.DefinitionGuid("863dece6-ff86-41c5-82ad-ec520adf6309")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.GUI.ViewModelDescriptor DefaultViewModelDescriptor {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_DefaultViewModelDescriptor 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.GUI.ViewModelDescriptor> GetProp_DefaultViewModelDescriptor();

        System.Threading.Tasks.Task SetProp_DefaultViewModelDescriptor(Zetbox.App.GUI.ViewModelDescriptor newValue);
    }
}
