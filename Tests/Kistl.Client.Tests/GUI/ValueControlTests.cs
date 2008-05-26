using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

using NMock2;
using NUnit.Framework;

namespace Kistl.GUI.Tests
{
    public abstract class ValueControlTests<TYPE, CONTROL>
        : BasicControlTests<CONTROL>
        where CONTROL : IValueControl<TYPE>
    {
        protected ValueControlTests(
            ControlHarness<CONTROL> controlHarness,
            IValues<TYPE> values
            )
            : base(controlHarness)
        {
            Assert.IsNotNull(values, "Values may not be null");
            Values = values;
        }

        protected IValues<TYPE> Values;

        #region Event Asserts

        private static bool _widget_UserInputHasFired = false;
        private EventHandler _widget_UserInputHandler = new EventHandler(delegate { _widget_UserInputHasFired = true; });

        [SetUp]
        public void AddEvents()
        {
            // have to chain a call to the base SetUp() here
            base.SetUp();
            Widget.UserInput += _widget_UserInputHandler;
        }

        [TearDown]
        public void RemoveEvents()
        {
            // important to not leak widgets when testing
            Widget.UserInput -= _widget_UserInputHandler;
            _widget_UserInputHasFired = false;
            // have to chain a call to the base TearDown() here
            base.Finish();
        }

        // override and add AssertFired(false)s
        protected override void AssertThatNoEventsFired()
        {
            base.AssertThatNoEventsFired();
            AssertUserInputEventFired(false);
        }

        // override and add AssertFired(false)s
        protected virtual void AssertThatOnlyUserInputEventFired()
        {
            base.AssertThatNoEventsFired();
            AssertUserInputEventFired(true);
        }

        protected void AssertUserInputEventFired(bool shouldFire)
        {
            Assert.AreEqual(shouldFire, _widget_UserInputHasFired,
                shouldFire ? "UserInput should have fired"
                    : "UserInput should not have fired"
                );
        }

        #endregion

        #region TestValue

        /// <summary>
        /// Use TestUserInput with a appropriate Action to simulateUserInput to implement this test.
        /// </summary>
        [Test]
        public abstract void TestUserInput();

        protected void TestUserInput(Action<CONTROL, TYPE> simulateUserInput)
        {
            TestProperty<TYPE>(
                w => w.Value,
                simulateUserInput,
                Values,
                AssertThatOnlyUserInputEventFired
                );
        }

        [Test]
        public void TestValueProgrammatically()
        {
            TestProperty<TYPE>(
                w => w.Value,
                (w, v) => w.Value = v,
                Values,
                AssertThatNoEventsFired
                );
        }

        #endregion

        [Test]
        public void TestIsValidValue()
        {
            TestProperty<bool>(
                w => w.IsValidValue,
                (w, v) => w.IsValidValue = v,
                new Values<bool>
                {
                    Valids = new[] { true, false, true, true, false, false, true },
                    Invalids = new bool[] { }
                },
                AssertThatNoEventsFired
                );
        }
    }

}
