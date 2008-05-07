using System.ComponentModel.Design;
using NUnit.Framework;

namespace NMock2.AcceptanceTests
{
    [TestFixture]
    public class MockeryAcceptanceTest
    {
        [Test]
        public void CallingVerifyOnMockeryShouldEnableMockeryToBeUsedSuccessfullyForOtherTests()
        {
            Mockery mocks = new Mockery();
            IMockedType mockWithUninvokedExpectations = (IMockedType)mocks.NewMock(typeof(IMockedType));
            Expect.Once.On(mockWithUninvokedExpectations).Method("Method").WithNoArguments();
            try
            {
                mocks.VerifyAllExpectationsHaveBeenMet();
                Assert.Fail("Expected ExpectationException to be thrown");
            }
            catch (NMock2.Internal.ExpectationException expected)
            {
                Assert.IsTrue(expected.Message.IndexOf("not all expected invocations were performed") != -1);
            }

            IMockedType mockWithInvokedExpectations = (IMockedType)mocks.NewMock(typeof(IMockedType));
            Expect.Once.On(mockWithInvokedExpectations).Method("Method").WithNoArguments();
            mockWithInvokedExpectations.Method();
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void MockObjectsMayBePlacedIntoServiceContainers()
        {
            Mockery mocks = new Mockery();
            ServiceContainer container = new ServiceContainer();

            IMockedType mockedType =
                mocks.NewMock(typeof(IMockedType)) as IMockedType;

            container.AddService(typeof(IMockedType), mockedType);

            Assert.AreSame(mockedType, container.GetService(typeof(IMockedType)));
        }

        public interface IEmpty { }

        [Test]
        public void MockEmptyInterface()
        {
            Mockery mocks = new Mockery();
            IEmpty mock = mocks.NewMock(typeof(IEmpty)) as IEmpty;
            Assert.IsNotNull(mock);
        }

        public interface INoArgs : IEmpty
        {
            void NoArgs();
            int NoArgsReturnInt();
            object NoArgsReturnObject();
        }

        [Test]
        public void MockSimpleInterface()
        {
            Mockery mocks = new Mockery();
            INoArgs mock = mocks.NewMock(typeof(INoArgs)) as INoArgs;
            Assert.IsNotNull(mock);
        }

        public interface INoArgsGeneric<T>
        {
            void NoArgs();
            T NoArgsReturnT();
        }

        [Test]
        public void MockSimpleGenericInterface()
        {
            Mockery mocks = new Mockery();
            INoArgsGeneric<int> intMock = mocks.NewMock(typeof(INoArgsGeneric<int>)) as INoArgsGeneric<int>;
            INoArgsGeneric<object> objectMock = mocks.NewMock(typeof(INoArgsGeneric<object>)) as INoArgsGeneric<object>;
            Assert.IsNotNull(intMock);
            Assert.IsNotNull(objectMock);
        }

        public interface IArgs
        {
            void IntArg(int arg1);
            int IntArgReturnInt(int arg1);
            object ObjectArgsReturnObject(object arg1);
        }

        [Test]
        public void MockSimpleInterfaceWithArgs()
        {
            Mockery mocks = new Mockery();
            IArgs mock = mocks.NewMock(typeof(IArgs)) as IArgs;
            Assert.IsNotNull(mock);
        }

        public interface IArgsGeneric<T>
        {
            void TArg(T arg1);
            T TArgReturnT(T arg1);
        }

        [Test]
        public void MockSimpleGenericInterfaceWithArgs()
        {
            Mockery mocks = new Mockery();
            IArgsGeneric<int> intMock = mocks.NewMock(typeof(IArgsGeneric<int>)) as IArgsGeneric<int>;
            IArgsGeneric<object> objectMock = mocks.NewMock(typeof(IArgsGeneric<object>)) as IArgsGeneric<object>;
            Assert.IsNotNull(intMock);
            Assert.IsNotNull(objectMock);
        }

        public interface IArgsGenericFunction1
        {
            void TArg<T>(T arg1);
        }

        public interface IArgsGenericFunction2
        {
            T ReturnT<T>();
        }

        public interface IArgsGenericFunction3
        {
            T IntArgReturnT<T>(int args);
        }

        public interface IArgsGenericFunction4
        {
            void MultiArg<A>(A a1, A a2, A a3);
        }

        public interface IArgsGenericFunction5
        {
            void MultiArg<A, B, C, D, E, F>(A a, B b, C c, D d, E e, F f);
        }

        public interface IArgsGenericFunction6
        {
            void MultiArg<A, B, C, D, E, F>(A a, int arg1, B b, bool arg2, C c, object arg3, D d, IEmpty arg4, E e, IArgsGeneric<F> arg5, F f);
        }

        [Test]
        public void MockGenericInterfaceWithArgs1()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction1 mock1 = mocks.NewMock(typeof(IArgsGenericFunction1)) as IArgsGenericFunction1;
            Assert.IsNotNull(mock1);
        }

        [Test]
        public void MockGenericInterfaceWithArgs2()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction2 mock2 = mocks.NewMock(typeof(IArgsGenericFunction2)) as IArgsGenericFunction2;
            Assert.IsNotNull(mock2);
        }

        [Test]
        public void MockGenericInterfaceWithArgs3()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction3 mock3 = mocks.NewMock(typeof(IArgsGenericFunction3)) as IArgsGenericFunction3;
            Assert.IsNotNull(mock3);
        }

        [Test]
        public void MockGenericInterfaceWithArgs4()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction4 mock4 = mocks.NewMock(typeof(IArgsGenericFunction4)) as IArgsGenericFunction4;
            Assert.IsNotNull(mock4);
        }

        [Test]
        public void MockGenericInterfaceWithArgs5()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction5 mock5 = mocks.NewMock(typeof(IArgsGenericFunction5)) as IArgsGenericFunction5;
            Assert.IsNotNull(mock5);
        }

        [Test]
        public void MockGenericInterfaceWithArgs6()
        {
            Mockery mocks = new Mockery();
            IArgsGenericFunction6 mock6 = mocks.NewMock(typeof(IArgsGenericFunction6)) as IArgsGenericFunction6;
            Assert.IsNotNull(mock6);
        }

        public interface IMixed<X>
            where X : IEmpty
        {
            void TArg<T>(T arg1, X arg2);
            T ReturnT<T>(X arg2);
            T IntArgReturnT<T>(int arg1, X arg2);
            X ReturnX<T>(T arg2);
        }

        [Test]
        public void MockMixedInterface()
        {
            Mockery mocks = new Mockery();
            IMixed<IEmpty> emptyMock = mocks.NewMock(typeof(IMixed<IEmpty>)) as IMixed<IEmpty>;
            IMixed<INoArgs> inheritMock = mocks.NewMock(typeof(IMixed<INoArgs>)) as IMixed<INoArgs>;
            Assert.IsNotNull(emptyMock);
            Assert.IsNotNull(inheritMock);
        }

    }

    public interface IMockedType
    {
        void Method();
    }

}
