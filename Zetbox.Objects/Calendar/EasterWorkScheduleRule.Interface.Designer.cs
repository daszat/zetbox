// <autogenerated/>

namespace Zetbox.App.Calendar
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// This rule applies every year, n days relative to easter.
    /// </summary>
    [Zetbox.API.DefinitionGuid("d321c926-952d-4810-904b-a57251dd5e9d")]
    public interface EasterWorkScheduleRule : Zetbox.App.Calendar.YearlyWorkScheduleRule 
    {

        /// <summary>
        /// Offset to eater. Null or zero, if easter is meant. Negative numbers are before easter.
        /// </summary>
        [Zetbox.API.DefinitionGuid("0fdcab86-001e-429d-af31-a5d0df5e6c75")]
        int? Offset {
            get;
            set;
        }

    }
}
