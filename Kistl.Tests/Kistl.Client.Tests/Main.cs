using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMock2;

namespace Kistl.Client.Tests
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            new SetUp().Init();
            var orpt = new ObjectReferencePresenterTests();
            orpt.SetUp();
            orpt.HandleInvalidUserInput();
        }
    }
}
