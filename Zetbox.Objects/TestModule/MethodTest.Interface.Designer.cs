// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Test class for methods
    /// </summary>
    [Zetbox.API.DefinitionGuid("68a664ee-67e0-4ba7-a0dc-148b9dfa32a7")]
    public interface MethodTest : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("bf48b883-8821-4c4e-8509-590a72604f9e")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Test.MethodTest> Children { get; }

        System.Threading.Tasks.Task<ICollection<Zetbox.App.Test.MethodTest>> GetProp_Children();

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("02a7d534-9325-48e5-bbc2-b61420afd940")]
		[System.Runtime.Serialization.IgnoreDataMember]
        Zetbox.App.Test.MethodTest Parent {
            get;
            set;
        }

		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		int? FK_Parent 
		{ 
			get; 
			set;
		}

        System.Threading.Tasks.Task<Zetbox.App.Test.MethodTest> GetProp_Parent();

        System.Threading.Tasks.Task SetProp_Parent(Zetbox.App.Test.MethodTest newValue);

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("8d226658-fecc-4139-8234-aa88a4738b4d")]
        string StringProp {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Group1();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Group2();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task ObjParameter(Zetbox.App.Test.MethodTest objParam);

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task<Zetbox.App.Test.MethodTest> ObjRet();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Parameterless();

        /// <summary>
        /// Does nothing, on the server
        /// </summary>
        System.Threading.Tasks.Task ServerMethod();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task<Zetbox.App.Test.TestObjClass> ServerObjParameter(Zetbox.App.Test.TestObjClass input);

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task ServerParameterless();

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task StringParameter(string str);

        /// <summary>
        /// 
        /// </summary>
        System.Threading.Tasks.Task Summary();
    }
}
