// <autogenerated/>

namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A container (book) for events
    /// </summary>
    [Zetbox.API.DefinitionGuid("1450be5f-2266-4367-8a56-1f0bee5699c4")]
    public interface CalendarBook : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IDeactivatable, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("5ae7dd37-9789-47a6-b679-3ccaecbbb75d")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.Group> GroupReaders { get; }

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("d960ba8e-1605-4a70-b950-4e12c4c1ae75")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.Group> GroupWriters { get; }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("e1647548-92ee-4fae-9155-faad7d7e9187")]
        string Name {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("7e39185f-5826-481f-b0fd-bc0ffd1400ad")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.Identity Owner {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Owner 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("ebd1fd99-f1c9-4260-832d-5ef5c48e7660")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.Identity> Readers { get; }

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("e52271a9-ca1a-486d-95c0-795a502af48e")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.Identity> Writers { get; }

        /// <summary>
        /// Gets a list of ViewModels that can create a new event. Multiple implementations can support different kind of events
        /// </summary>
        void GetNewEventViewModels(System.Object args);
    }
}
