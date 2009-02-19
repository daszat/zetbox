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
            SerializableTypeTests();
            
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
            test.SerializableExpressionTest();

            test.SetUp();
            test.null_String_roundtrips_correctly();
        }

        private static void SerializableTypeTests()
        {
            var tests = new SerializableTypeTests();
            //tests.GetHashCode_returns_right_value();
            //tests.GetSystemType_fails_on_invalid_AssemblyQualifiedName();
            tests.GetSystemType_fails_on_invalid_TypeName();
            //tests.GetSystemType_returns_right_type();
        }

    }
}
