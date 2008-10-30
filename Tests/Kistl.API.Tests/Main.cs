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
            SetUp setup = new SetUp();
            setup.Init();

            var test = new ApplicationContextTests();
            test.SetCustomActionsManagerNull();
        }

        private static void BinarySerializerTests()
        {
            BinarySerializerTests test = new BinarySerializerTests();
            
            test.SetUp();
            test.ICollection_ICollectionEntry();

            test.SetUp();
            test.ICollection_IDataObject();

            test.SetUp();
            test.SerializableExpression();
        }
    }
}
