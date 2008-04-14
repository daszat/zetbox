using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Kistl.GUI.DB;
using Kistl.Client.Controls;

namespace Kistl.Client.Tests
{
    [TestFixture]
    public class GuiDbTests
    {
        [Test]
        public void ControlInfo()
        {
            var cInfo = KistlGUIContext.FindControlInfo(Toolkit.WPF, new Visual() { Name = TestControl.Info.Control });
            Assert.That(cInfo.Equals(TestControl.Info));
        }
    }
}
