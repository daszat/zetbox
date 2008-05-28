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

    /// <summary>
    /// contains the basic setup routines to test a IBasicControl
    /// </summary>
    /// <typeparam name="CONTROL"></typeparam>
    public abstract class BasicControlTests<CONTROL>
        where CONTROL : IBasicControl
    {
        protected BasicControlTests(ControlHarness<CONTROL> controlHarness)
        {
            Assert.IsNotNull(controlHarness, "controlHarness cannot be null");
            ControlHarness = controlHarness;
        }

        protected Mockery Mockery { get; private set; }
        // protected IKistlContext MockContext { get; private set; }

        private ControlHarness<CONTROL> ControlHarness { get; set; }

        protected CONTROL Widget { get { return ControlHarness.Widget; } }
        protected Visual Visual { get { return ControlHarness.Visual; } }

        [SetUp]
        public void SetUp()
        {
            Mockery = new Mockery();
            ControlHarness.SetUp();
        }

        [Test]
        public void TestSetUpCorrect()
        {
            Assert.IsNotNull(Mockery, "Mockery should have been initialised");
            // Assert.IsNotNull(MockContext, "MockContext should have been initialised");
            Assert.IsNotNull(ControlHarness, "ControlHarness should have been initialised");
            ControlHarness.TestSetUpCorrect();
        }

        [TearDown]
        public void Finish()
        {
            Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        /// <summary>
        /// This method checks whether the Interface under test has fired no Events.
        /// This is used e.g. when testing various properties.
        /// 
        /// Implementor should override this to check all relevant events
        /// </summary>
        protected virtual void AssertThatNoEventsFired() { }

        /// <summary>
        /// Tests whether a property accepts valid and rejects invalid values.
        /// </summary>
        /// 
        /// To be able to do so, you have to tell this function how to set and 
        /// get values from the property and lists of valid and invalid values.
        /// 
        /// Finally, the assertThatNoEventsFired Action let's you add additional
        /// tests after each set to assert that no superfluous Events were fired 
        /// by the Control.
        /// 
        /// Example implementation:
        /// CONTROL : IValueControl&lt;string%gt;
        /// private static bool _widget_UserInputHasFired = false;
        /// private EventHandler _widget_UserInputHandler = new EventHandler(delegate { _widget_UserInputHasFired = true; });
        /// 
        /// [SetUp]
        /// public void AddEvents()
        /// {
        ///     // have to chain a call to the base SetUp() here
        ///     base.SetUp();
        ///     Widget.UserInput += _widget_UserInputHandler;
        /// }
        /// 
        /// [TearDown]
        /// public void RemoveEvents()
        /// {
        ///     // important to not leak widgets when testing
        ///     Widget.UserInput -= _widget_UserInputHandler;
        ///     _widget_UserInputHasFired = false;
        ///     // have to chain a call to the base TearDown() here
        ///     base.Finish();
        /// }
        /// 
        /// protected override void AssertThatNoEventsFired() {
        ///     base.AssertThatNoEventsFired();
        ///     Assert.IsFalse(_widget_UserInputHasFired, "UserInput should not have fired");
        /// }
        /// <typeparam name="PTYPE"></typeparam>
        /// <param name="getProperty"></param>
        /// <param name="setProperty"></param>
        /// <param name="values"></param>
        /// <param name="AssertThatNoEventsFired"></param>
        protected void TestProperty<PTYPE>(
            Func<CONTROL, PTYPE> getProperty, Action<CONTROL, PTYPE> setProperty,
            IValues<PTYPE> values, Action assertThatNoEventsFired)
        {
            PTYPE[] valids = values.Valids;
            Assert.Greater(valids.Length, 0, "need valid data to test");

            // Ensure that the first setProperty in the loop will actually change the property
            {
                var selectDifferent = (from v in valids where (v == null || !v.Equals(valids[0])) select v);
                if (selectDifferent.Count() >= 1)
                {
                    setProperty(Widget, selectDifferent.Last());
                }
            }

            foreach (var test in valids)
            {
                setProperty(Widget, test);
                Assert.AreEqual(test, getProperty(Widget), "widget should return the new value");
                assertThatNoEventsFired();
            }
        }

        [Test]
        public void TestDescription()
        {
            TestProperty<string>(w => w.Description, (w, v) => w.Description = v,
                new Values<string>
                {
                    Valids = new[] { "some string", "some other string" },
                    // anything goes
                    Invalids = new string[] { }
                },
                AssertThatNoEventsFired
                );
        }

        [Test]
        public void TestShortLabel()
        {
            TestProperty<string>(w => w.ShortLabel, (w, v) => w.ShortLabel = v,
                new Values<string>
                {
                    Valids = new[] { "some string", "some other string" },
                    // anything goes
                    Invalids = new string[] { }
                },
                AssertThatNoEventsFired
                );
        }

        [Test]
        public void TestFieldSize()
        {
            TestProperty<FieldSize>(w => w.Size, (w, v) => w.Size = v,
                new Values<FieldSize>
                {
                    Valids = Enum.GetValues(typeof(FieldSize)).OfType<FieldSize>().ToArray<FieldSize>(),
                    Invalids = new FieldSize[] { }
                },
                AssertThatNoEventsFired
                );
        }

    }

}