// <autogenerated/>

namespace Zetbox.App.GUI
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("ffda4604-1536-43b6-b951-f8753d5092ca")]
    public interface ViewDescriptor : IDataObject, Zetbox.App.Base.IExportable, Zetbox.App.Base.IModuleMember 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("7720b38c-64af-4607-bc73-d015af6612e3")]
        Zetbox.App.GUI.ControlKind ControlKind {
            get;
            set;
        }

        /// <summary>
        /// The control implementing this View
        /// </summary>
        [Zetbox.API.DefinitionGuid("eff6276d-975b-4a0d-bd3c-ad76af2189c3")]
        Zetbox.App.Base.TypeRef ControlRef {
            get;
            set;
        }

        /// <summary>
        /// The control implementing this View.
        /// </summary>
        [Zetbox.API.DefinitionGuid("180968cf-8705-433f-9346-e726c8552737")]
        string ControlTypeRef {
            get;
            set;
        }

        /// <summary>
        /// Indicates that the referenced control type is deleted. Descriptors with this flag set require action to finally delete them and handle their users.
        /// </summary>
        [Zetbox.API.DefinitionGuid("4b256764-6a40-47cb-a222-242a36e06457")]
        bool Deleted {
            get;
            set;
        }

        /// <summary>
        /// A View supports one or more ViewModels.
        /// </summary>

        [Zetbox.API.DefinitionGuid("b898a824-578e-45e0-a312-193068a2b139")]
        ICollection<string> SupportedViewModelRefs { get; }

        /// <summary>
        /// A View supports one or more ViewModels
        /// </summary>

        [Zetbox.API.DefinitionGuid("4698cfda-6b1d-4cd7-8350-630a1adab1a8")]
        ICollection<Zetbox.App.Base.TypeRef> SupportedViewModels { get; }

        /// <summary>
        /// Which toolkit provides this View
        /// </summary>
        [Zetbox.API.DefinitionGuid("2a798728-d79d-471f-be51-1f488beb8dc1")]
        Zetbox.App.GUI.Toolkit Toolkit {
            get;
            set;
        }
    }
}
