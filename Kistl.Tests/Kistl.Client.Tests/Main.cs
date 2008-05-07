using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMock2;

namespace Kistl.Client.Tests
{
    public class MainProgram
    {
        public interface ITest
        {
            T RetT<T>();
        }
        public static void Main(string[] args)
        {
            Mockery mocks = new Mockery();
            ITest t = mocks.NewMock<ITest>();
        }
    }
}
