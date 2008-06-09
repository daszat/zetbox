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
            //var test = new Kistl.GUI.Tests.BackReferencePresenterTests();
            //test.SetUp();
            //test.HandleValidUserInput();

            var test2 = new Kistl.GUI.Tests.ObjectListPresenterTests();
            test2.SetUp();
            test2.HandleInvalidUserInput();

            Type fullySpecifiedIList1 = typeof(IList<SetUp>);
            Type genericIList = fullySpecifiedIList1.GetGenericTypeDefinition();
            Type fullySpecifiedIList2 = genericIList.MakeGenericType(typeof(String));

        }
    }
}
