// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("9419242e-31f2-4af7-bb09-49b908f397ee")]
    public interface FilterConfiguration : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable, Zetbox.App.Base.IModuleMember 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("f61fc911-989b-4dff-81e7-df00fd8497ba")]
        string Label {
            get;
            set;
        }


        /// <summary>
        /// Overrides the default behaviour. If true the filter will be immediately applied
        /// </summary>
        [Zetbox.API.DefinitionGuid("ede29e7c-6aa4-48d4-9737-811fae5d26d4")]
        bool RefreshOnFilterChanged {
            get;
            set;
        }


        /// <summary>
        /// Overrides the default kind of the configured ViewModelDescriptor
        /// </summary>
        [Zetbox.API.DefinitionGuid("afd2747f-9165-425e-946f-aed748ca5703")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.GUI.ControlKind RequestedKind {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_RequestedKind 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.GUI.ControlKind> GetProp_RequestedKind();

        System.Threading.Tasks.Task SetProp_RequestedKind(Zetbox.App.GUI.ControlKind newValue);

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("9e23e6a1-8e4f-48c5-ae83-9dae82c6b796")]
        bool Required {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("5776e14c-4bf4-4388-8a5b-2e81b232bf8f")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.GUI.ViewModelDescriptor ViewModelDescriptor {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_ViewModelDescriptor 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.GUI.ViewModelDescriptor> GetProp_ViewModelDescriptor();

        System.Threading.Tasks.Task SetProp_ViewModelDescriptor(Zetbox.App.GUI.ViewModelDescriptor newValue);

        /// <summary>
        /// 
        /// </summary>
        Zetbox.API.IFilterModel CreateFilterModel(Zetbox.API.IZetboxContext ctx);

        /// <summary>
        /// 
        /// </summary>
        string GetLabel();
    }
}
