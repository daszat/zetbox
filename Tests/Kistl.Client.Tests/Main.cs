using System;
using System.Collections.Generic;
using System.Text;

namespace Kistl.Client.Tests
{
    static class MainProgram
    {
        static void Main(string[] args)
        {
            //var test = new ObjectListModelTests();
            //test.SetUp();
            //test.TestCreation();
            var test = new DataObjectModelTests();
            test.TestDesignMock();
        }
    }
}
