// <autogenerated/>

namespace Zetbox.App.Projekte
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("3fbb42ca-a084-491d-9135-85ed24f1ef78")]
    public interface Task : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable, Zetbox.App.Base.IMergeable 
    {

        /// <summary>
        /// Aufwand in Stunden
        /// </summary>
        [Zetbox.API.DefinitionGuid("a28f7536-9b8a-49ca-bc97-d28e1c2c4d3e")]
        double? Aufwand {
            get;
            set;
        }


        /// <summary>
        /// Enddatum
        /// </summary>
        [Zetbox.API.DefinitionGuid("2b705496-388a-43a8-82e8-b17b652a55fc")]
        DateTime? DatumBis {
            get;
            set;
        }


        /// <summary>
        /// Start Datum
        /// </summary>
        [Zetbox.API.DefinitionGuid("1485a7b7-c4d5-456a-a18a-0c409c3eca8e")]
        DateTime DatumVon {
            get;
            set;
        }


        /// <summary>
        /// Taskname
        /// </summary>
        [Zetbox.API.DefinitionGuid("91595e02-411c-40f2-ab83-4cced76e954d")]
        string Name {
            get;
            set;
        }


        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>
        [Zetbox.API.DefinitionGuid("5545ba8a-3e89-4b22-bd66-c12f3622ace0")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Projekte.Projekt Projekt {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Projekt 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Projekte.Projekt> GetProp_Projekt();

        System.Threading.Tasks.Task SetProp_Projekt(Zetbox.App.Projekte.Projekt newValue);
    }
}
