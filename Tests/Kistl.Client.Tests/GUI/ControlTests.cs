using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Mocks;

using NMock2;
using NUnit.Framework;

namespace Kistl.GUI.Tests
{

    /// <summary>
    /// contains the basic setup routines to test a control
    /// </summary>
    /// <typeparam name="CONTROL"></typeparam>
    public abstract class ControlTests<CONTROL>
        where CONTROL : IBasicControl
    {
        protected ControlTests(ControlHarness<CONTROL> controlHarness)
        {
            Assert.IsNotNull(controlHarness, "controlHarness cannot be null");
            ControlHarness = controlHarness;
        }

        protected Mockery Mockery { get; private set; }
        protected IKistlContext MockContext { get; private set; }

        private ControlHarness<CONTROL> ControlHarness { get; set; }

        protected CONTROL Widget { get { return ControlHarness.Widget; } }
        protected Visual Visual { get { return ControlHarness.Visual; } }

        /// <summary>
        /// This method checks whether the Interface under test has fired no Events.
        /// This is used e.g. when testing various properties.
        /// 
        /// Implementor should override this to check all relevant events
        /// </summary>
        /// Example implementation:
        /// CONTROL : IValueControl&lt;string%gt;
        /// private bool _widget_UserInputHasFired = false;
        /// private EventHandler _widget_UserInputHandler = new EventHandler(delegate { _widget_UserInputHasFired = true; });
        /// 
        /// [SetUp]
        /// public void AddEvents()
        /// {
        ///     // have to chain a call to the base SetUp() here
        ///     base.SetUp();
        ///     widget.UserInput += _widget_UserInputHandler;
        /// }
        /// 
        /// [TearDown]
        /// public void RemoveEvents()
        /// {
        ///     // important to not leak widgets when testing
        ///     widget.UserInput -= _widget_UserInputHandler;
        ///     _widget_UserInputHasFired = false;
        ///     // have to chain a call to the base TearDown() here
        ///     base.TearDown();
        /// }
        /// 
        /// protected override void AssertThatNoEventsFired() { Assert.IsFalse(_widget_UserInputHasFired, "UserInput should not have fired"); }
        protected virtual void AssertThatNoEventsFired() { }

        [SetUp]
        public void SetUp()
        {
            ControlHarness.SetUp();
        }

        [Test]
        public void TestSetUpCorrect()
        {
            Assert.IsNotNull(Mockery, "Mockery should have been initialised");
            Assert.IsNotNull(MockContext, "MockContext should have been initialised");
            Assert.IsNotNull(ControlHarness, "ControlHarness should have been initialised");
        }

        [TearDown]
        public void Finish()
        {
            Mockery.VerifyAllExpectationsHaveBeenMet();
        }


        protected void TestProperty<PTYPE>(
            Func<CONTROL, PTYPE> getProperty, Action<CONTROL, PTYPE> setProperty,
            IEnumerable<PTYPE> validValues, IEnumerable<PTYPE> invalidValues)
        {
            foreach (var test in validValues)
            {
                Assert.AreNotEqual(test, getProperty(Widget), "widget should not have strange values");
                setProperty(Widget, test);
                Assert.AreEqual(test, getProperty(Widget), "widget should let the values be set");
                AssertThatNoEventsFired();
            }

            foreach (var test in invalidValues)
            {
                var original = getProperty(Widget);
                Assert.AreNotEqual(test, original, "widget should not have invalid values");

                bool catched = false;
                try
                {
                    setProperty(Widget, test);
                }
                catch (ArgumentException argEx)
                {
                    catched = true;
                }
                Assert.IsTrue(catched, "widget should signal illegal value with ArgumentException");

                Assert.AreEqual(original, getProperty(Widget), "widget should not accept invalid values");
                AssertThatNoEventsFired();
            }
        }

        [Test]
        public void TestDescription()
        {
            TestProperty<string>(w => w.Description, (w, v) => w.Description = v,
                new List<string>(new[] { "some string", "some other string" }),
                // anything goes
                new List<string>()
                );
        }

        [Test]
        public void TestShortLabel()
        {
            TestProperty<string>(w => w.ShortLabel, (w, v) => w.ShortLabel = v,
                new List<string>(new[] { "some string", "some other string" }),
                // anything goes
                new List<string>()
                );
        }

        [Test]
        public void TestFieldSize()
        {
            TestProperty<FieldSize>(w => w.Size, (w, v) => w.Size = v,
                Enum.GetValues(typeof(FieldSize)).OfType<FieldSize>(),
                // anything goes
                new List<FieldSize>()
                );
        }

    }

}