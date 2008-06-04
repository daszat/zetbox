using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;
using NUnit.Framework.Constraints;

using Kistl.API;
using Kistl.GUI.Tests;

namespace Kistl.GUI.Renderer.WPF.Tests
{
    public sealed class ListValues<TYPE> : IValues<IList<TYPE>>
    {
        /// <summary>
        /// Specify whether one List may contain a specific Item more than once.
        /// </summary>
        public bool IsUnique { get; private set; }
        /// <summary>
        /// Specify whether the empty List is considered Valid or Invalid.
        /// </summary>
        public bool IsEmptyValid { get; private set; }

        public IValues<TYPE> BaseValues { get; private set; }

        public ListValues(IValues<TYPE> baseValues, bool isUnique, bool isEmptyValid)
        {
            IsUnique = isUnique;
            IsEmptyValid = isEmptyValid;

            BaseValues = baseValues;
            Valids = GenerateLists(BaseValues.Valids, IsUnique, IsEmptyValid);

            IList<IList<TYPE>> invalidResult = new List<IList<TYPE>>();

            IList<TYPE> invalidValues = new List<TYPE>(BaseValues.Invalids);
            if (invalidValues.Count == 0)
            {
                // call GenerateLists on empty Invalids array to pass IsEmptyValid
                Invalids = GenerateLists(BaseValues.Invalids, IsUnique, !IsEmptyValid);
            }
            else
            {
                IEnumerator<TYPE> invalidEnumeration = invalidValues.GetEnumerator();
                invalidEnumeration.MoveNext();

                // generate valid Lists with one additional invalid item
                Invalids = GenerateLists(BaseValues.Valids, IsUnique, true).Select(
                    delegate(IList<TYPE> l)
                    {
                        l.Add(invalidEnumeration.Current);
                        if (!invalidEnumeration.MoveNext())
                        {
                            // reset enumeration to start over
                            invalidEnumeration = invalidValues.GetEnumerator();
                            invalidEnumeration.MoveNext();
                        }
                        return l;
                    }).ToArray();
            }
        }

        private static readonly int FIXED_SEMI_RANDOM_SEED = 23 * 42;

        internal static IList<TYPE>[] GenerateLists(TYPE[] items, bool createUniqueListItems, bool includeEmptyList)
        {
            List<IList<TYPE>> result = new List<IList<TYPE>>();

            if (includeEmptyList)
            {
                result.Add(new List<TYPE>(0));
            }

            Random rng = new Random(FIXED_SEMI_RANDOM_SEED);

            result.AddRange(from i in items select GenerateList(rng, i, items, createUniqueListItems));

            return result.ToArray();
        }

        internal static IList<TYPE> GenerateList(
            Random rng,
            TYPE mustItem, TYPE[] otherItems,
            bool createUniqueListItems)
        {
            List<TYPE> result = new List<TYPE>(otherItems);
            result.Insert(rng.Next(result.Count), mustItem);

            // enforce uniqueness constraint
            if (createUniqueListItems)
            {
                result = result.Distinct().ToList();
            }
            else
            {
                int duplicateCount = rng.Next(20);
                while (duplicateCount-- > 0)
                {
                    result.Insert(rng.Next(result.Count), result[rng.Next(result.Count)]);
                }
            }
            return result;
        }

        #region IValues<IList<TYPE>> Members

        public IList<TYPE>[] Valids { get; private set; }
        public IList<TYPE>[] Invalids { get; private set; }

        public bool IsValid(IList<TYPE> list)
        {
            if (list == null || list.Count == 0)
                return IsEmptyValid;

            if (IsUnique && list.Count != list.Distinct().Count())
                return false;

            return list.All(i => BaseValues.Valids.Contains(i));
        }

        #endregion

    }

    [TestFixture]
    public class ListValuesTests
    {
        private ListValues<int> CreateListValues(bool isUnique, bool isEmptyValid)
        {
            var basic = new Values<int>() { Valids = new[] { 1, 2, 3, 4, 5 }, Invalids = new[] { -1, -2, -3, -4, -5 } };
            ListValues<int> result = new ListValues<int>(basic, isUnique, isEmptyValid);
            Assert.AreEqual(isUnique, result.IsUnique, "result should know right 'isUnique' Value");
            Assert.AreEqual(isEmptyValid, result.IsEmptyValid, "result should know right 'IsEmptyValid' Value");
            return result;
        }

        private class FakeRandom : Random
        {
            private int _current = 0;
            public override int Next(int maxValue)
            {
                int result = _current % maxValue;
                _current += 1;
                return result;
            }
        }

        private static void BasicListTests(int? mustItem, int[] otherItems, IList<int> test)
        {
            foreach (var i in test)
            {
                if (i != mustItem)
                {
                    Assert.Contains(i, otherItems,
                        "other items should come from the otherItems array");
                }
            }

            if (mustItem.HasValue)
                Assert.That(test.Contains(mustItem.Value), "the result should contain the mustItem");
        }

        [Test]
        public void TestUniqueListGenerationCore()
        {
            int mustItem = 1;
            int[] otherItems = new[] { 10, 20, 30, 40 };
            IList<int> list = ListValues<int>.GenerateList(
                new FakeRandom(), mustItem, otherItems, true);

            BasicListTests(mustItem, otherItems, list);
            AssertEmpty(true, FilterMultiple(list),
                "There should be no duplicate entries in a unique list");
        }

        [Test]
        public void TestNonUniqueListGenerationCore()
        {
            int mustItem = 1;
            int[] otherItems = new[] { 10, 20, 30, 40 };
            IList<int> list = ListValues<int>.GenerateList(
                new FakeRandom(), mustItem, otherItems, false);

            BasicListTests(mustItem, otherItems, list);
            AssertEmpty(false, FilterMultiple(list),
                "At least one element should be duplicated");
        }

        private void TestListGeneration(bool isUnique, bool isEmptyValid)
        {
            int[] items = new[] { 10, 20, 30, 40 };
            var lists = ListValues<int>.GenerateLists(items, isUnique, isEmptyValid);

            if (isEmptyValid)
            {
                Assert.That(
                    lists.Any(list => list.Count == 0),
                    "results should contain empty list");
            }
            else
            {
                Assert.That(
                    lists.All(list => list.Count > 0),
                    "results should not contain empty list");
            }

            foreach (var list in lists)
            {
                BasicListTests(null, items, list);

                if (isUnique)
                {
                    AssertEmpty(true, FilterMultiple(list),
                        "the items should be unique");
                }
                else
                {
                    // empty list is exempt from this requirement
                    if (list.Count > 0)
                    {
                        AssertEmpty(false, FilterMultiple(list),
                            "there should be duplicate items");
                    }
                }
            }
        }

        private static void AssertEmpty(bool expectedEmpty, IList<int> list, string message, params object[] msgParams)
        {
            if (expectedEmpty)
            {
                Assert.That(list,
                    new EmptyCollectionConstraint(),
                    message ?? "list should be empty", msgParams);
            }
            else
            {
                Assert.That(list,
                    new NotConstraint(new EmptyCollectionConstraint()),
                    message ?? "list should have content", msgParams);
            }
        }

        [Test]
        public void TestUniqueListGenerationWithEmpty()
        {
            TestListGeneration(true, true);
        }

        [Test]
        public void TestNonUniqueListGenerationWithEmpty()
        {
            TestListGeneration(false, true);
        }

        [Test]
        public void TestUniqueListGenerationWithoutEmpty()
        {
            TestListGeneration(true, false);
        }

        [Test]
        public void TestNonUniqueListGenerationWithoutEmpty()
        {
            TestListGeneration(false, false);
        }

        /// <summary>
        /// returns all elements of the list which occur multiple times
        /// </summary>
        private IList<int> FilterMultiple(IList<int> list)
        {
            var listOfMultiples = from i in list where list.Count(x => x == i) > 1 select i;
            return listOfMultiples.Distinct().ToList();
        }

        [Test]
        public void TestValids()
        {
            var test = CreateListValues(false, true);

            foreach (var list in test.Valids)
            {
                foreach (var item in list)
                {
                    Assert.Contains(item, test.BaseValues.Valids);
                }
            }
        }

        [Test]
        public void TestIsValid()
        {
            var test = CreateListValues(false, true);

            foreach (var list in test.Valids)
            {
                Assert.That(test.IsValid(list), "ListValues should recognize valid values");
            }
            foreach (var list in test.Invalids)
            {
                Assert.That(!test.IsValid(list), "ListValues should recognize invalid values");
            }
        }

        [Test]
        public void TestInvalids()
        {
            var test = CreateListValues(false, true);
            foreach (var list in test.Invalids)
            {
                Assert.That(list.Any(item => test.BaseValues.Invalids.Contains(item)), "invalid entry X should contain at least one invalid item");
            }
        }


    }
}