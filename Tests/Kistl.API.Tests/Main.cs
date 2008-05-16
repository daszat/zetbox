using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            new SetUp().Init();
            new APIInitTest().SecondInitFail();
        }
    }
}
