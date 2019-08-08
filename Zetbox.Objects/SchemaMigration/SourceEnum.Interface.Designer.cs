// <autogenerated/>

namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Mapps an Enum
    /// </summary>
    [Zetbox.API.DefinitionGuid("138d462f-e432-46a0-8ce2-e7f9893654d4")]
    public interface SourceEnum : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("0d29a48f-6791-4640-be97-7e93d246a389")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Base.EnumerationEntry DestinationValue {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_DestinationValue 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("b7195fb9-e6da-493f-b1be-9465e4d9d5ae")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.SchemaMigration.SourceColumn SourceColumn {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_SourceColumn 
		{ 
			get; 
			set;
		}

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("885a90e0-62e0-426a-a3b5-64dca3a38d18")]
        string SourceValue {
            get;
            set;
        }

    }
}
