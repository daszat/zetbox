namespace NMock2.AcceptanceTests
{
	public interface IHelloWorld
	{
		void Hello();
		void Umm();
		void Err();
		void Ahh();
		void Goodbye();
		string Ask(string question);
//#if NET20
//        X Generic1<X>();
//        Y Generic2<Y>(Y y);
//        void Generic3<Z>(Z z);
//#endif
	}

    internal class HelloWorldImpl : IHelloWorld
    {
        #region IHelloWorld Members

        public void Hello()
        {
        }

        public void Umm()
        {
        }

        public void Err()
        {
        }

        public void Ahh()
        {
        }

        public void Goodbye()
        {
        }

        public string Ask(string question)
        {
            return null;
        }

        public X Generic1<X>()
        {
            return default(X);
        }

        public Y Generic2<Y>(Y y)
        {
            return default(Y);
        }

        public void Generic3<Z>(Z z)
        {
        }

        #endregion
    }

}
