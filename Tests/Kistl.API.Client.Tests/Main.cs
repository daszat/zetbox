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
            KistlContextTests test = new API.Client.Tests.KistlContextTests();

            test.SetUp();
            test.Delete();

            test.SetUp();
            test.GetList();

            test.SetUp();
            test.GetObject();
        }
    }
}
