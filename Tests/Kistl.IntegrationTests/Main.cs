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

            ListPropetiesTests();
            ObjectTests();

            setup.TearDown();
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
    }
}
