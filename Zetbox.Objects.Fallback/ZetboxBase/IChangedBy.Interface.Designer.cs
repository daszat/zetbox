// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// IChangedBy
    /// </summary>
    [Zetbox.API.DefinitionGuid("a73f2d0a-786b-483d-b474-b9d9df42b8b3")]
    public interface IChangedBy  
    {

        /// <summary>
        /// Identity which changed this object
        /// </summary>
        [Zetbox.API.DefinitionGuid("ca0d65eb-05e2-40a6-9fb2-cdee91f1dd2d")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.Identity ChangedBy {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_ChangedBy 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Base.Identity> GetProp_ChangedBy();

        System.Threading.Tasks.Task SetProp_ChangedBy(Zetbox.App.Base.Identity newValue);

        /// <summary>
        /// Date and time where this object was changed
        /// </summary>
        [Zetbox.API.DefinitionGuid("76be9590-f9ce-412a-bdda-882527203ffc")]
        DateTime ChangedOn {
            get;
            set;
        }


        /// <summary>
        /// Identity which created this object
        /// </summary>
        [Zetbox.API.DefinitionGuid("2b628a47-48d6-4611-9fd7-8535bbd30b75")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.Identity CreatedBy {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_CreatedBy 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Base.Identity> GetProp_CreatedBy();

        System.Threading.Tasks.Task SetProp_CreatedBy(Zetbox.App.Base.Identity newValue);

        /// <summary>
        /// Date and time where this object was created
        /// </summary>
        [Zetbox.API.DefinitionGuid("906415f6-3e1b-45ea-849f-26b269b3f0ea")]
        DateTime CreatedOn {
            get;
            set;
        }

    }
}
