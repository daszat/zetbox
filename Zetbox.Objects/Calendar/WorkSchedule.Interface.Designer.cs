// <autogenerated/>

namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A WorkSchedule describing recurrent working hours
    /// </summary>
    [Zetbox.API.DefinitionGuid("901a2ddd-1330-4129-b8a2-92b8e655d168")]
    public interface WorkSchedule : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable, Zetbox.App.Base.IModuleMember 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("17a8fbd3-5a42-4cf6-9517-0adf4142f4fe")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Calendar.WorkSchedule BaseWorkSchedule {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_BaseWorkSchedule 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("c2fc6792-bc1f-42bb-b6c3-451ab99ddbef")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Calendar.WorkSchedule> ChildWorkSchedule { get; }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("49cdf3fb-639f-4c20-b9ca-9af1bbe0d4d7")]
        string Name {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("b16c20d8-ac72-45e8-883c-52c6f28571f2")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Calendar.WorkScheduleRule> WorkScheduleRules { get; }

        /// <summary>
        /// Duplicates this work schedule
        /// </summary>
        Zetbox.App.Calendar.WorkSchedule Duplicate();

        /// <summary>
        /// Gets the number of free days between two dates
        /// </summary>
        int GetOffDays(DateTime from, DateTime until);

        /// <summary>
        /// Get the number of working days between two dates
        /// </summary>
        int GetWorkingDays(DateTime from, DateTime until);

        /// <summary>
        /// Returns the amount of working hours between two dates
        /// </summary>
        decimal GetWorkingHours(DateTime from, DateTime until);
    }
}
