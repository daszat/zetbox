
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

    [TestFixture]
    public class when_creating
        : ViewModelTestFixture
    {
        [Test]
        public void should_have_B_UV_state()
        {
            Assert.That(obj.GetCurrentState(), Is.EqualTo(ValueViewModelState.Blurred_UnmodifiedValue));
        }

        [Test]
        public void should_remember_ValueModel()
        {
            Assert.That(obj.ValueModel, Is.SameAs(valueModelMock.Object));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void should_reflect_AllowNullInput(bool allow)
        {
            valueModelMock.SetupGet<bool>(o => o.AllowNullInput).Returns(allow).Verifiable();

            Assert.That(obj.AllowNullInput, Is.EqualTo(allow));

            valueModelMock.Verify();
        }
    }
}
