using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Tests;
using Kistl.GUI.Renderer.WPF;

namespace Kistl.GUI.Integration.WPF
{
    [TestFixture]
    public class ObjectReferenceControlIntegration : ObjectReferencePresenterInfrastructure<ObjectReferenceControl>
    {
        protected override Toolkit Toolkit { get { return Toolkit.WPF; } }

        protected override void UserInput(IDataObject v)
        {
            ((System.Windows.DependencyObject)widget).SetValue(ObjectReferenceControl.ValueProperty, v);
        }

    }
}

namespace Kistl.GUI.Integration.ASPNET
{

    // TODO: re-enable when ASPNET is implemented
    // [TestFixture]
    public class ObjectReferenceControlIntegration : ObjectReferencePresenterInfrastructure<ObjectReferenceControl>
    {
        protected override Toolkit Toolkit { get { return Toolkit.ASPNET; } }

        protected override void UserInput(IDataObject v)
        {
            throw new NotImplementedException();
        }

    }

}
