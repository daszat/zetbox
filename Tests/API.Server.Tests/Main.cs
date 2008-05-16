using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Server.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            new SetUp().Init();
            KistlDataContextTests();
        }

        private static void KistlDataContextTests()
        {
            Tests.KistlDataContextTests tests = new Tests.KistlDataContextTests();

            tests.SetUp();
            tests.SelectSomeData_Parent();

            tests.SetUp();
            tests.SelectSomeData_Children();

            tests.SetUp();
            tests.GetListOf();

            tests.SetUp();
            tests.GetListOf_ObjType();

            tests.SetUp();
            tests.GetListOf_WrongPropertyName();

            tests.SetUp();
            tests.SelectSomeData();

            tests.SetUp();
            tests.SelectSomeData_Collection();
        }
    }
}
