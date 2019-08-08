// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Testclass for the required_parent tests: child
    /// </summary>
    [Zetbox.API.DefinitionGuid("3e7f2f55-ff5c-4a13-ba58-74368e9c8780")]
    public interface RequiredParentChild : IDataObject 
    {

        /// <summary>
        /// dummy property
        /// </summary>
        [Zetbox.API.DefinitionGuid("82dc687e-3915-4f03-9a1f-75e42fcbe7cd")]
        string Name {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("09fb9f88-7a59-4dae-8cad-9fbab99f32c3")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Test.RequiredParent Parent {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Parent 
		{ 
			get; 
			set;
		}
    }
}
