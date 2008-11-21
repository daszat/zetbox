using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections;

namespace Kistl.IntegrationTests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            using (SetUp setup = new SetUp())
            {
                setup.Init();

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
        }

        private static void RelationTests()
        {
            var test = new RelationTests();

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

            //test.SetUp();
            //test.Change_Relation_1_n_Set_n_By_Index();

            test.SetUp();
            test.Change_Relation_1_n_Set_n_With_Clear();

            test.SetUp();
            test.Change_Relation_1_n_Set_n_With_Remove();

            //test.SetUp();
            //test.Change_Relation_n_m_Set_m_By_Index();

            test.SetUp();
            test.Change_Relation_n_m_Set_m_With_Clear();

            test.SetUp();
            test.Change_Relation_n_m_Set_m_With_Remove();

            //test.SetUp();
            //test.Change_Relation_n_m_Set_n_By_Index();

            test.SetUp();
            test.Change_Relation_n_m_Set_n_With_Clear();

            test.SetUp();
            test.Change_Relation_n_m_Set_n_With_Remove();
        }

        private static void FrozenContextTests()
        {
            var test = new FrozenContextTests();

            test.SetUp();
            test.IsReadonlyFlag();

            test.SetUp();
            test.IsReadonlyObject();

            test.SetUp();
            try
            {
                test.IsReadonly_Create();
            }
            catch (ReadOnlyContextException)
            {
            }


            test.SetUp();
            try
            {
                test.IsReadonlyObject_String();
            }
            catch (ReadOnlyObjectException)
            {
            }

            test.SetUp();
            try
            {
                test.IsReadonlyObject_Reference();
            }
            catch (ReadOnlyObjectException)
            {
            }

        }

        private static void GeneratorTests()
        {

            var generatorTests = new GeneratorTests();
            generatorTests.SetUp();
            generatorTests.Generate();
            generatorTests.TearDown();
        }

        private static void InheritanceTests()
        {
            InheritanceTests test = new InheritanceTests();

            test.SetUp();
            test.GetListOfInheritedObjects();

            test.SetUp();
            test.UpdateInheritedObject();
        }

        private static void GetListOfTests()
        {
            GetListOfTests test = new GetListOfTests();

            test.SetUp();
            test.GetListOf_GetObject();

            test.SetUp();
            test.GetObject_GetListOf();
        }

        private static void GetListTests()
        {
            GetListTests test = new GetListTests();

            // doesnt work yet
            //test.SetUp();
            //test.GetListByTypeWithOrderBy();

            test.SetUp();
            test.GetListWithOrderBy();

            test.SetUp();
            test.GetListWithOrderByAndWhere();

            // doesnt work yet
            //test.SetUp();
            //test.GetListWithOrderByThenOrderBy();

            test.SetUp();
            test.GetListWithTake();

            test.SetUp();
            test.GetListWithTakeAndWhere();

            test.SetUp();
            test.GetListWithPropertyAccessor();

            test.SetUp();
            test.GetListWithPropertyObjectAccessor();

            test.SetUp();
            test.GetListWithProjection();

            test.SetUp();
            test.GetListWithSingle();

            test.SetUp();
            test.GetListSingle();

            test.SetUp();
            test.GetListWithFirst();

            test.SetUp();
            test.GetListFirst();

        }

        private static void ListPropetiesTests()
        {
            ListPropetiesTests test = new ListPropetiesTests();

            test.SetUp();
            test.AddStringListPropertyContent();

            test.SetUp();
            test.DeleteStringListPropertyContent();

            test.SetUp();
            test.UpdateStringListPropertyContent();

            test.SetUp();
            test.GetPointerListPropertyContent();

            test.SetUp();
            test.GetStringListPropertyContent();
        }

        private static void ObjectTests()
        {
            ObjectTests test = new ObjectTests();

            test.SetUp();
            test.GetListOf();

            test.SetUp();
            test.GetListOf_List();

            test.SetUp();
            test.GetObject();

            test.SetUp();
            test.GetObject_Twice();

            test.SetUp();
            test.NewObject();

            test.SetUp();
            test.SetObject();
        }

        private static void StructTests()
        {
            StructTests test = new StructTests();

            test.SetUp();
            test.CreateObjectWithStruct();

            test.SetUp();
            test.GetObjectWithStruct();

            test.SetUp();
            test.SaveObjectWithStruct();

            test.SetUp();
            test.ChangeObjectWithStruct();

            test.SetUp();
            test.SetStructNull();
        }
    }
}
