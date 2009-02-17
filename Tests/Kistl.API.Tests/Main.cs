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

            //AssemblyLoaderTests();

            BinarySerializerTests();
            
        }

        private static void AssemblyLoaderTests()
        {
            var test = new AssemblyLoaderTests();

            test.SetUp();
            test.AssemblyResolveReflection();

            test.SetUp();
            test.AssemblyResolve();
        }

        private static void ApplicationContextTests()
        {
            var test = new ApplicationContextTests();



        }

        private static void BinarySerializerTests()
        {
            BinarySerializerTests test = new BinarySerializerTests();
            
            test.SetUp();
            test.SerializableExpression();

            test.SetUp();
            test.null_String_roundtrips_correctly();
        }
    }
}
