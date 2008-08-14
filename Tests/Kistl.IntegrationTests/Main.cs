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

            GetListOfTests();
            //GetListTests();
            //ListPropetiesTests();
            //ObjectTests();
            //StructTests();

            setup.TearDown();
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
            test.GetListWithPropertyAccessor();

            test.SetUp();
            test.GetListWithSingle();

            test.SetUp();
            test.GetListWithProjection();
        }

        private static void ListPropetiesTests()
        {
            ListPropetiesTests test = new ListPropetiesTests();

            test.SetUp();
            test.DeleteStringListPropertyContent();

            test.SetUp();
            test.AddStringListPropertyContent();

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
