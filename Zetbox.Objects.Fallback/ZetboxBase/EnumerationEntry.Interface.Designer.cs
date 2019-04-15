// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for an Enumeration Entry.
    /// </summary>
    [Zetbox.API.DefinitionGuid("6365c62d-60a6-4fa3-9c78-370ffcc50478")]
    public interface EnumerationEntry : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// Description of this Enumeration Entry
        /// </summary>
        [Zetbox.API.DefinitionGuid("3366c523-0593-4a29-978f-5ac8a4f15eca")]
        string Description {
            get;
            set;
        }

        /// <summary>
        /// Übergeordnete Enumeration
        /// </summary>
        [Zetbox.API.DefinitionGuid("115c3bfb-72fd-46f2-81fe-74ce1cfa1874")]
        Zetbox.App.Base.Enumeration Enumeration {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("feb0b203-5f83-4b9b-848c-a3e4ee895055")]
        string Label {
            get;
            set;
        }

        /// <summary>
        /// CLR name of this entry
        /// </summary>
        [Zetbox.API.DefinitionGuid("1c1e497b-294f-442e-8793-478b298d4aba")]
        string Name {
            get;
            set;
        }

        /// <summary>
        /// If true, the entry will not be selectable in the UI
        /// </summary>
        [Zetbox.API.DefinitionGuid("43a77c0a-75bf-4130-9d6d-a2ac629d3602")]
        bool NotSelectable {
            get;
            set;
        }

        /// <summary>
        /// The CLR value of this entry
        /// </summary>
        [Zetbox.API.DefinitionGuid("2fea1d2e-d5ed-457f-9828-4df8c3d3d3aa")]
        int Value {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        string GetLabel();
    }
}
