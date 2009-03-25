using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Test;

using NUnit.Framework;

namespace Kistl.DalProvider.EF.Tests.Structs
{
    [TestFixture]
    public class when_initialized
        : StructFixture
    {

        [Test]
        public void should_be_attached_to_their_parent()
        {
            Assert.That(obj.PhoneNumberMobile__Implementation__.ParentObject, Is.SameAs(obj));
        }

        [Test]
        public void should_have_right_property_name_set()
        {
            Assert.That(obj.PhoneNumberMobile__Implementation__.ParentProperty, Is.EqualTo("PhoneNumberMobile"));
        }

    }
}
