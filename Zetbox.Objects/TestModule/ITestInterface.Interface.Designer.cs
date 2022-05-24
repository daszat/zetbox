// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A Test Interface
    /// </summary>
    [Zetbox.API.DefinitionGuid("c8ff9958-dd26-4a92-a049-7fa9d51d8bf2")]
    public interface ITestInterface  
    {

        /// <summary>
        /// Objektpointer für das Testinterface
        /// </summary>
        [Zetbox.API.DefinitionGuid("bd5c671d-c81f-4f13-9ee8-158ea4892d24")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Projekte.Kunde ObjectProp {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_ObjectProp 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Projekte.Kunde> GetProp_ObjectProp();

        System.Threading.Tasks.Task SetProp_ObjectProp(Zetbox.App.Projekte.Kunde newValue);

        /// <summary>
        /// String Property für das Testinterface
        /// </summary>
        [Zetbox.API.DefinitionGuid("dd027211-bc39-4279-b567-47ee7f0de22f")]
        string StringProp {
            get;
            set;
        }


        /// <summary>
        /// Test Enum Property
        /// </summary>
        [Zetbox.API.DefinitionGuid("657b719f-dcda-4308-9587-4e2c10e7b60f")]
        Zetbox.App.Test.TestEnum TestEnumProp {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task TestMethod(DateTime DateTimeParam);
    }
}
