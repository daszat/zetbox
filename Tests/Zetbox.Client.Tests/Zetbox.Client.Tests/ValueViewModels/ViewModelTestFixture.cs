
namespace Zetbox.Client.Tests.ValueViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Moq;
    using NUnit.Framework;

    public abstract class ViewModelTestFixture
        : AbstractClientTestFixture
    {
        protected class TestValueViewModel : ValueViewModel<object, object>
        {
            public TestValueViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
                : base(dependencies, dataCtx, parent, mdl)
            {
            }

            public override bool HasValue
            {
                get { throw new NotImplementedException(); }
            }

            protected override string FormatValue(object value)
            {
                if (OnFormatValue != null)
                {
                    return OnFormatValue(value);
                }

                Assert.Fail("Unexpected FormatValue Call");

                // unreachable code
                return null;
            }

            public event FormatValueCallback OnFormatValue;
            public delegate string FormatValueCallback(object value);

            protected override ParseResult<object> ParseValue(string str)
            {
                if (OnParseValue != null)
                {
                    var result = OnParseValue(str);
                    return new ParseResult<object>()
                    {
                        Value = result.Value,
                        Error = result.Key
                    };
                }
                
                Assert.Fail("Unexpected ParseValue Call");

                // unreachable code
                return null;
            }

            public event ParseValueCallback OnParseValue;
            public delegate KeyValuePair<string, object> ParseValueCallback(string str);

            public ValueViewModelState GetCurrentState()
            {
                return State;
            }

            protected override object GetValueFromModel()
            {
                return ValueModel.Value;
            }

            protected override void SetValueToModel(object value)
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
            valueModelMock.SetupProperty(o => o.Value);
            valueModelMock.SetupGet(o => o.Label).Returns("ValueLabel");
            valueModelMock.SetupGet(o => o.AllowNullInput).Returns(true);
            obj = new TestValueViewModel(scope.Resolve<IViewModelDependencies>(), scope.Resolve<BaseMemoryContext>(), null, valueModelMock.Object);
        }
    }
}
