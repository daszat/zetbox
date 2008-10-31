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

            AssemblyLoaderTests();

            BinarySerializerTests();
            
        }

        private static void AssemblyLoaderTests()
        {
            AssemblyLoaderTests test = new AssemblyLoaderTests();

            test.SetUp();
            test.AssemblyResolveReflection();

            test.SetUp();
            test.AssemblyResolve();
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
