using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Zetbox.API.Mocks
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

        private static SerializableType GetSerializableType<LOCALINTERFACE, ENUMTYPE>(InterfaceType.Factory iftFactory)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            return iftFactory(typeof(LOCALINTERFACE)).ToSerializableType();
        }

        private static SerializableType GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>(InterfaceType.Factory iftFactory)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            return iftFactory(typeof(IValueCollectionEntry<LOCALINTERFACE, string>)).ToSerializableType();
        }

        /// <summary>
        /// Serializes a test TestObjClass to the stream sw.
        /// </summary>
        /// <param name="sw"></param>
        public static void ToStream<LOCALINTERFACE, ENUMTYPE>(ZetboxStreamWriter sw, InterfaceType.Factory iftFactory)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {

            // BaseServerPersistenceObject
            sw.Write(GetSerializableType<LOCALINTERFACE, ENUMTYPE>(iftFactory));
            sw.Write(TestObjClassId);
            sw.Write((int)TestObjectState);
            sw.Write((int)AccessRights.Full);


            // TestObjClass

            // BaseTestObjClass Reference
            sw.Write(TestBaseClassId);

            // StringProp
            sw.Write(TestStringPropValue);

            //// SubClasses are not serialized, but fetched lazily
            //foreach (int subClassID in TestSubClassesIds)
            //{
            //    BinarySerializer.ToStream(true, sw);
            //    BinarySerializer.ToStream(subClassID, sw);
            //}
            //BinarySerializer.ToStream(false, sw);

            // TestEnumProp
            sw.Write((int)TestEnum.TestSerializationValue);

            // TestNames
            var ceType = GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>(iftFactory);
            for (int i = 0; i < TestTestNamesIds.Length; i++)
            {
                sw.Write(true);

                sw.Write(ceType);
                sw.Write(TestTestNamesIds[i]);
                sw.Write((int)TestCollectionEntryState);
                sw.Write((int)AccessRights.Full);

                sw.Write(TestTestNamesValues[i]);
            }
            sw.Write(false);
        }

        public static void AssertCorrectContents<LOCALINTERFACE, ENUMTYPE>(ZetboxStreamReader sr, InterfaceType.Factory iftFactory)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            Assert.That(sr, Is.Not.Null, "no stream to inspect");

            var objType = sr.ReadSerializableType();
            Assert.That(objType, Is.EqualTo(GetSerializableType<LOCALINTERFACE, ENUMTYPE>(iftFactory)), "wrong interface type found");

            var testObjId = sr.ReadInt32();
            Assert.That(testObjId, Is.EqualTo(TestObjClassId), "wrong object ID found");

            DataObjectState? objectState = null;
            sr.ReadConverter(i => objectState = (DataObjectState)i);
            Assert.That(objectState, Is.EqualTo(TestObjectState), "wrong ObjectState found");

            int accessRights = sr.ReadInt32();
            Assert.That(accessRights, Is.GreaterThan(0), "wrong Access Rights found");

            // TestObjClass

            // BaseTestObjClass Reference
            int? testObjRefId;
            sr.Read(out testObjRefId);
            Assert.That(testObjRefId, Is.EqualTo(TestBaseClassId), "wrong BaseObjClass ID found");

            // StringProp
            string testStringProp;
            sr.Read(out testStringProp);
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
            var testEnum = (TestEnum)sr.ReadInt32();
            Assert.That(testEnum, Is.EqualTo(TestEnum.TestSerializationValue), "wrong enum value found");

            // TestNames
            for (int i = 0; i < TestTestNamesIds.Length; i++)
            {
                var continuationMarkerForCes = sr.ReadBoolean();
                Assert.That(continuationMarkerForCes, Is.True, "wrong continuation marker for testName #{0}", i);

                var ceType = sr.ReadSerializableType();
                Assert.That(ceType, Is.EqualTo(GetSerializableCollectionEntryType<LOCALINTERFACE, ENUMTYPE>(iftFactory)), "wrong interface type found for collection entry #{0}", i);

                var readCeId = sr.ReadInt32();
                Assert.That(readCeId, Is.EqualTo(TestTestNamesIds[i]), "wrong id read for collection entry #{0}", i);

                DataObjectState? ceObjectState = null;
                sr.ReadConverter(read => ceObjectState = (DataObjectState)read);
                Assert.That(ceObjectState, Is.EqualTo(TestCollectionEntryState), "wrong ObjectState found for collection entry #{0}", i);

                var readCeAccessRights = sr.ReadInt32();
                Assert.That(readCeAccessRights, Is.GreaterThan(0), "wrong access rights for collection entry #{0}", i);

                var readValue = sr.ReadString();
                Assert.That(readValue, Is.EqualTo(TestTestNamesValues[i]), "wrong value read for collection entry #{0}", i);

            }

            var continuationMarkerAfterCes = sr.ReadBoolean();
            Assert.That(continuationMarkerAfterCes, Is.False, "wrong continuation marker after testNames collection entries");

        }

        private static void AssertCorrectContents<LOCALINTERFACE, ENUMTYPE>(LOCALINTERFACE obj)
            where LOCALINTERFACE : TestObjClass<LOCALINTERFACE, ENUMTYPE>
            where ENUMTYPE : struct
        {
            Assert.That(obj.ID, Is.EqualTo(TestObjClassId), "wrong ID");
            // TODO: Unable to check that here -> Server evaluates ClientObjectState!
            //Assert.That(obj.ObjectState, Is.EqualTo(TestObjectState), "wrong ObjectState");
            // TODO: Unable to check that here -> some tests works with detached objects
            //Assert.That(obj.BaseTestObjClass.ID, Is.EqualTo(TestBaseClassId), "wrong BaseTestObjClass.ID");
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
