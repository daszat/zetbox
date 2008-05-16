using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Tests
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
            Tests.ListPropetiesTests test = new Integration.Tests.Tests.ListPropetiesTests();

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
            Tests.ObjectTests test = new Integration.Tests.Tests.ObjectTests();

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
