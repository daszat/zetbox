using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            new SetUp().Init();
            //BaseClientDataObjectTests();
            KistlContextTests();
        }

        private static void BaseClientDataObjectTests()
        {
            BaseClientDataObjectTests test = new BaseClientDataObjectTests();

            test.SetUp();
            test.Stream();
        }

        private static void KistlContextTests()
        {
            KistlContextTests test = new API.Client.Tests.KistlContextTests();

            test.SetUp();
            test.Find_ObjectType();

            test.SetUp();
            test.Delete();

            test.SetUp();
            test.GetList();

            test.SetUp();
            test.GetObject();

            test.SetUp();
            test.Attach_IDataObject_WithList_Add();

            test.SetUp();
            test.Attach_IDataObject_WithList_Insert();

            test.SetUp();
            test.Attached_IDataObject_WithList_Add();

            test.SetUp();
            test.Attached_IDataObject_WithList_Insert();
        }
    }
}
