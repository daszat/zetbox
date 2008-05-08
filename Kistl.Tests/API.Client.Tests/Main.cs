using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Client.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            new SetUp().Init();
            Tests.KistlContextTests test = new API.Client.Tests.Tests.KistlContextTests();

            test.SetUp();
            test.GetList();

            test.SetUp();
            test.GetObject();
        }
    }
}
