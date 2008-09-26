using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.IntegrationTests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            SetUp setup = new SetUp();
            setup.Init();

            //InheritanceTests();
            //GetListOfTests();
            GetListTests();
            //ListPropetiesTests();
            //ObjectTests();
            //StructTests();

            setup.TearDown();
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

            test.SetUp();
            test.GetListWithOrderBy();

            test.SetUp();
            test.GetListWithOrderByAndWhere();

            test.SetUp();
            test.GetListWithOrderByThenOrderBy();

            test.SetUp();
            test.GetListWithTake();

            test.SetUp();
            test.GetListWithTakeAndWhere();

            test.SetUp();
            test.GetListWithPropertyAccessor();

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
