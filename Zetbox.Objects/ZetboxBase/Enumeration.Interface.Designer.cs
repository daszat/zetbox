// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for Enumerations.
    /// </summary>
    [Zetbox.API.DefinitionGuid("ee475de2-d626-49e9-9e40-6bb12cb026d4")]
    public interface Enumeration : Zetbox.App.Base.DataType, Zetbox.App.Base.INamedObject 
    {

        /// <summary>
        /// Enumeration Entries are Flags
        /// </summary>
        [Zetbox.API.DefinitionGuid("1ef92eea-d8b3-4f95-a694-9ca09ceff0e5")]
        bool AreFlags {
            get;
            set;
        }


        /// <summary>
        /// Einträge der Enumeration
        /// </summary>

        [Zetbox.API.DefinitionGuid("1619c8a7-b969-4c05-851c-7a2545cda484")]
        [System.Runtime.Serialization.IgnoreDataMember]
        IList<Zetbox.App.Base.EnumerationEntry> EnumerationEntries { get; }

        System.Threading.Tasks.Task<IList<Zetbox.App.Base.EnumerationEntry>> GetProp_EnumerationEntries();

        /// <summary>
        /// 
        /// </summary>
        Zetbox.App.Base.EnumerationEntry GetEntryByName(string name);

        /// <summary>
        /// 
        /// </summary>
        Zetbox.App.Base.EnumerationEntry GetEntryByValue(int val);

        /// <summary>
        /// 
        /// </summary>
        string GetLabelByName(string name);

        /// <summary>
        /// 
        /// </summary>
        string GetLabelByValue(int val);
    }
}
