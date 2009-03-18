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
            test.ApplyChanges();

            test.SetUp();
            test.AttachToContext();

            test.SetUp();
            test.AttachToContext_New();

            test.SetUp();
            test.DetachFromContext();
        }

        private static void KistlContextTests()
        {
            KistlContextTests test = new API.Client.Tests.KistlContextTests();

            test.SetUp();
            test.SubmitChanges();

            //test.SetUp();
            //test.Delete();

            //test.SetUp();
            //test.GetList();

        }
    }
}
