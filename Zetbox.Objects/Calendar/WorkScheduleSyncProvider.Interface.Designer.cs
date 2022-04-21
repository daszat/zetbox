// <autogenerated/>

namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Sync provider for work schedules
    /// </summary>
    [Zetbox.API.DefinitionGuid("ed44a638-a19d-430c-b19f-766a1820fc67")]
    public interface WorkScheduleSyncProvider : Zetbox.App.Calendar.SyncProvider 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("f67558bb-7415-4a41-9196-7c39426746df")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Calendar.CalendarBook Calendar {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Calendar 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Calendar.CalendarBook> GetProp_Calendar();

        System.Threading.Tasks.Task SetProp_Calendar(Zetbox.App.Calendar.CalendarBook newValue);

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("72dcb583-17bc-4247-a7c1-39f607b4905c")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Calendar.WorkSchedule WorkSchedule {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_WorkSchedule 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Calendar.WorkSchedule> GetProp_WorkSchedule();

        System.Threading.Tasks.Task SetProp_WorkSchedule(Zetbox.App.Calendar.WorkSchedule newValue);
    }
}
