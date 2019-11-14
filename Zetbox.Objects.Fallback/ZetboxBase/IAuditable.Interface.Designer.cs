// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Marks an object class as auditable and provides the basic audit trail table.
    /// </summary>
    [Zetbox.API.DefinitionGuid("ef042c7f-93c0-4989-a7c9-0d03598671e5")]
    public interface IAuditable  
    {

        /// <summary>
        /// Eine Liste der Änderungen an diesem Datensatz.
        /// </summary>

        [Zetbox.API.DefinitionGuid("5fd0ba19-b5e8-4a43-a95e-895a6054dd95")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.AuditEntry> AuditJournal { get; }
    }
}
