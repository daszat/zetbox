
namespace Kistl.Client.Tests.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Kistl.API;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    public abstract class ViewModelTestFixture
        : AbstractClientTestFixture
    {
        protected class TestValueViewModel : ValueViewModel<object, object>
        {
            public TestValueViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, IValueModel mdl)
                : base(dependencies, dataCtx, mdl)
            {
            }

            protected override void OnValidInput()
            {
                var oldState = State;
                base.OnValidInput();
                OnStateChanged(oldState, State);
            }

            protected override void OnModelChanged()
            {
                var oldState = State;
                base.OnModelChanged();
                OnStateChanged(oldState, State);
            }

            protected virtual void OnStateChanged(ValueViewModelState oldState, ValueViewModelState newState)
            {
                if (StateChanged != null && oldState != newState)
                {
                    StateChanged(this, new StateChangedEventArgs(oldState, newState));
                }
            }

            public override bool HasValue
            {
                get { throw new NotImplementedException(); }
            }

            protected override string FormatValue()
            {
                if (OnFormatValue != null)
                {
                    return OnFormatValue();
                }

                Assert.Fail("Unexpected FormatValue Call");

                // unreachable code
                return null;
            }

            public event FormatValueCallback OnFormatValue;
            public delegate string FormatValueCallback();

            protected override void ParseValue(string str, out string error)
            {
                if (OnParseValue != null)
                {
                    error = OnParseValue(str);
                    return;
                }
                
                Assert.Fail("Unexpected ParseValue Call");

                // unreachable code
                error = String.Empty;
                return;
            }

            public event ParseValueCallback OnParseValue;
            public delegate string ParseValueCallback(string str);

            public ValueViewModelState GetCurrentState()
            {
                return State;
            }

            public event StateChangedEventHandler StateChanged;
            public delegate void StateChangedEventHandler(object sender, StateChangedEventArgs args);

            protected override object GetValue()
            {
                return ValueModel.Value;
            }

            protected override void SetValue(object value)
            {
                ValueModel.Value = value;
            }
        }

        protected TestValueViewModel obj;
        protected Mock<Models.IValueModel<object>> valueModelMock;

        public override void SetUp()
        {
            base.SetUp();
            valueModelMock = new Mock<Models.IValueModel<object>>(MockBehavior.Strict);
            // ignore Error handling for now
            valueModelMock.SetupGet<string>(o => o.Error).Returns(String.Empty);
            obj = new TestValueViewModel(scope.Resolve<IViewModelDependencies>(), scope.Resolve<BaseMemoryContext>(), valueModelMock.Object);
        }
    }
}
