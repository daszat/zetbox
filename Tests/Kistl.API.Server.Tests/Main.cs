using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            var setup = new SetUp();
            setup.Init();
            BaseServerCollectionEntryTests();
            BaseServerDataObjectTests();
        }

        private static void BaseServerCollectionEntryTests()
        {
            var test = new BaseServerCollectionEntryTests();

            test.SetUp();
            test.should_roundtrip_correctly();
        }

        private static void BaseServerDataObjectTests()
        {
            var bsdot = new BaseServerDataObjectTests();
            bsdot.SetUp();
            bsdot.FromStream_creates_correct_Object();

            //bsdot.SetUp();
            //bsdot.ToStream_creates_correct_Stream();
        }
    }
}
