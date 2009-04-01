using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Kistl.API.Mocks
{
    public interface TestObjClass<LOCALINTERFACE, ENUMTYPE>
        : IDataObject
        where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
        where ENUMTYPE : struct
    {
        LOCALINTERFACE BaseTestObjClass { get; set; }
        string StringProp { get; set; }
        ICollection<LOCALINTERFACE> SubClasses { get; }
        ENUMTYPE TestEnumProp { get; set; }
        ICollection<string> TestNames { get; }

        void TestMethod(DateTime DateTimeParamForTestMethod);
    }

    public static class TestObjClassSerializationMock
    {
        public readonly static int TestObjClassId = 23;
        public readonly static DataObjectState TestObjectState = DataObjectState.Unmodified;
        public readonly static int? TestBaseClassId = 22;
        public readonly static string TestStringPropValue = "stringprop testvalue";
        public readonly static int[] TestSubClassesIds = new[] { 3, 4, 5 };
        public readonly static int[] TestTestNamesIds = new[] { 20, 30, 40 };
        public readonly static string[] TestTestNamesValues = new[] { "testname1", "testname2", "testname3" };
        public readonly static DataObjectState TestCollectionEntryState = DataObjectState.Unmodified;

        private static SerializableType GetSerializableType<LOCALINTERFACE, ENUMTYPE>()
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            return new SerializableType(new InterfaceType(typeof(LOCALINTERFACE)));
        }

        private static SerializableType GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>()
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            return new SerializableType(new InterfaceType(typeof(INewCollectionEntry<LOCALINTERFACE, string>)));
        }

        /// <summary>
        /// Serializes a test TestObjClass to the stream sw.
        /// </summary>
        /// <param name="sw"></param>
        public static void ToStream<LOCALINTERFACE, ENUMTYPE>(BinaryWriter sw)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {

            // BaseServerPersistenceObject
            BinarySerializer.ToStream(GetSerializableType<LOCALINTERFACE, ENUMTYPE>(), sw);
            BinarySerializer.ToStream(TestObjClassId, sw);
            BinarySerializer.ToStream((int)TestObjectState, sw);


            // TestObjClass

            // BaseTestObjClass Reference
            BinarySerializer.ToStream(TestBaseClassId, sw);

            // StringProp
            BinarySerializer.ToStream(TestStringPropValue, sw);

            //// SubClasses are not serialized, but fetched lazily
            //foreach (int subClassID in TestSubClassesIds)
            //{
            //    BinarySerializer.ToStream(true, sw);
            //    BinarySerializer.ToStream(subClassID, sw);
            //}
            //BinarySerializer.ToStream(false, sw);

            // TestEnumProp
            BinarySerializer.ToStream((int)TestEnum.TestSerializationValue, sw);

            // TestNames
            var ceType = GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>();
            for (int i = 0; i < TestTestNamesIds.Length; i++)
            {
                BinarySerializer.ToStream(true, sw);

                BinarySerializer.ToStream(ceType, sw);
                BinarySerializer.ToStream(TestTestNamesIds[i], sw);
                BinarySerializer.ToStream((int)TestCollectionEntryState, sw);

                BinarySerializer.ToStream(TestTestNamesValues[i], sw);
            }
            BinarySerializer.ToStream(false, sw);
        }

        public static void AssertCorrectContents<LOCALINTERFACE, ENUMTYPE>(BinaryReader sr)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            Assert.That(sr, Is.Not.Null, "no stream to inspect");

            SerializableType objType = null;
            BinarySerializer.FromStream(out objType, sr);
            Assert.That(objType, Is.EqualTo(GetSerializableType<LOCALINTERFACE, ENUMTYPE>()), "wrong interface type found");

            int testObjId;
            BinarySerializer.FromStream(out testObjId, sr);
            Assert.That(testObjId, Is.EqualTo(TestObjClassId), "wrong object ID found");

            DataObjectState? objectState = null;
            BinarySerializer.FromStreamConverter(i => objectState = (DataObjectState)i, sr);
            Assert.That(objectState, Is.EqualTo(TestObjectState), "wrong ObjectState found");

            // TestObjClass

            // BaseTestObjClass Reference
            int? testObjRefId;
            BinarySerializer.FromStream(out testObjRefId, sr);
            Assert.That(testObjRefId, Is.EqualTo(TestBaseClassId), "wrong BaseObjClass ID found");

            // StringProp
            string testStringProp;
            BinarySerializer.FromStream(out testStringProp, sr);
            Assert.That(testStringProp, Is.EqualTo(TestStringPropValue), "wrong StringProp Value found");

            //// SubClasses are not serialized, but fetched lazily
            //foreach (int subClassID in TestSubClassesIds)
            //{
            //    bool continuationMarkerForSubClasses = false;
            //    BinarySerializer.FromStream(out continuationMarkerForSubClasses, sr);
            //    Assert.That(continuationMarkerForSubClasses, Is.True, "wrong continuation marker for subClassId {0}", subClassID);

            //    int readSubClassId;
            //    BinarySerializer.FromStream(out readSubClassId, sr);
            //    Assert.That(readSubClassId, Is.EqualTo(readSubClassId), "wrong subClassId read", subClassID);
            //}

            //bool continuationMarkerAfterSubClasses = false;
            //BinarySerializer.FromStream(out continuationMarkerAfterSubClasses, sr);
            //Assert.That(continuationMarkerAfterSubClasses, Is.False, "wrong continuation marker after subClassIds");

            // TestEnumProp
            int testEnum;
            BinarySerializer.FromStream(out testEnum, sr);
            Assert.That(testEnum, Is.EqualTo((int)TestEnum.TestSerializationValue), "wrong enum value found");

            // TestNames
            for (int i = 0; i < TestTestNamesIds.Length; i++)
            {
                bool continuationMarkerForCes = false;
                BinarySerializer.FromStream(out continuationMarkerForCes, sr);
                Assert.That(continuationMarkerForCes, Is.True, "wrong continuation marker for testName #{0}", i);

                SerializableType ceType = null;
                BinarySerializer.FromStream(out ceType, sr);
                Assert.That(ceType, Is.EqualTo(GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>()), "wrong interface type found for collection entry #{0}", i);

                int readCeId;
                BinarySerializer.FromStream(out readCeId, sr);
                Assert.That(readCeId, Is.EqualTo(TestTestNamesIds[i]), "wrong id read for collection entry #{0}", i);

                DataObjectState? ceObjectState = null;
                BinarySerializer.FromStreamConverter(read => ceObjectState = (DataObjectState)read, sr);
                Assert.That(ceObjectState, Is.EqualTo(TestCollectionEntryState), "wrong ObjectState found for collection entry #{0}", i);

                string readValue;
                BinarySerializer.FromStream(out readValue, sr);
                Assert.That(readValue, Is.EqualTo(TestTestNamesValues[i]), "wrong value read for collection entry #{0}", i);

            }

            bool continuationMarkerAfterCes = false;
            BinarySerializer.FromStream(out continuationMarkerAfterCes, sr);
            Assert.That(continuationMarkerAfterCes, Is.False, "wrong continuation marker after testNames collection entries");

        }

        private static void AssertCorrectContents<LOCALINTERFACE, ENUMTYPE>(LOCALINTERFACE obj)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            Assert.That(obj.ID, Is.EqualTo(TestObjClassId), "wrong ID");
            // TODO: Unable to check that here -> Server evaluates ClientObjectState!
            //Assert.That(obj.ObjectState, Is.EqualTo(TestObjectState), "wrong ObjectState");
            Assert.That(obj.BaseTestObjClass.ID, Is.EqualTo(TestBaseClassId), "wrong BaseTestObjClass.ID");
            Assert.That(obj.StringProp, Is.EqualTo(TestStringPropValue), "wrong StringProp");
            // not serialized
            //Assert.That(obj.SubClasses.Select(sc => sc.ID).ToArray(), Is.EqualTo(TestSubClassesIds), "wrong SubClasses");
            // TODO: test IDs too?
            Assert.That(obj.TestNames, Is.EqualTo(TestTestNamesValues), "wrong testnames");
        }

        public static void AssertCorrectContentsInt<LOCALINTERFACE>(LOCALINTERFACE obj)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, int>
        {
            AssertCorrectContents<LOCALINTERFACE, int>(obj);
            Assert.That(obj.TestEnumProp, Is.EqualTo((int)TestEnum.TestSerializationValue), "wrong TestEnumProp");
        }

        public static void AssertCorrectContentsEnum<LOCALINTERFACE>(LOCALINTERFACE obj)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, TestEnum>
        {
            AssertCorrectContents<LOCALINTERFACE, TestEnum>(obj);
            Assert.That(obj.TestEnumProp, Is.EqualTo(TestEnum.TestSerializationValue), "wrong TestEnumProp");
        }
    }
}
