using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            SetUp setup = new SetUp();
            setup.Init();
            try
            {
                RelationTests();
                //FrozenContextTests();
                //GetListTests();

                //GeneratorTests();
                //GetListOfTests();
                //InheritanceTests();
                //ListPropetiesTests();
                //ObjectTests();
                //StructTests();
            }
            finally
            {
                setup.TearDown();
            }
        }

        private static void RelationTests()
        {
            var test = new RelationTests();

            test.SetUp();
            test.Sort_Relation_1_n();

            test.SetUp();
            test.Relation_1_n_Set_1();

            test.SetUp();
            test.Relation_1_n_Set_n();

            test.SetUp();
            test.Relation_1_n_Set_n();

            test.SetUp();
            test.Relation_n_m_Set_n();

            test.SetUp();
            test.Relation_n_m_Set_m();

            test.SetUp();
            test.Relation_1_1_Set_Left();

            test.SetUp();
            test.Relation_1_1_Set_Right();

            test.SetUp();
            test.Change_Relation_1_n_Set_1();

            test.SetUp();
            test.Change_Relation_1_n_Set_n_By_Index();

            test.SetUp();
            test.Change_Relation_1_n_Set_n_With_Clear();

            test.SetUp();
            test.Change_Relation_1_n_Set_n_With_Remove();

            //test.SetUp();
            //test.Change_Relation_n_m_Set_n_By_Index();

            test.SetUp();
            test.Change_Relation_n_m_Set_n_With_Clear();

            test.SetUp();
            test.Change_Relation_n_m_Set_n_With_Remove();

            //test.SetUp();
            //test.Change_Relation_n_m_Set_m_By_Index();

            test.SetUp();
            test.Change_Relation_n_m_Set_m_With_Clear();

            test.SetUp();
            test.Change_Relation_n_m_Set_m_With_Remove();
        }

    }
}
