using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;

namespace Kistl.Client.Tests
{
    public class MainProgram
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new SetUp().Init();
            var test = new Kistl.GUI.Renderer.WPF.Tests.ObjectReferenceControlTests();
            test.SetItemSource();
            test.TestCombobox();
        }
    }
}
