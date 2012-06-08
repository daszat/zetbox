
namespace Zetbox.API.AbstractConsumerTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using NUnit.Framework;

    public abstract class SerializerTestFixture : AbstractTestFixture
    {
        protected MemoryStream ms;
        protected ZetboxStreamWriter sw;
        protected ZetboxStreamReader sr;
        protected InterfaceType.Factory iftFactory;

        /// <summary>
        /// Get the assmebly to build a typemap for SerializableType optimization in ZetboxStreams.
        /// </summary>
        /// <returns>by default the assembly of the currently running testfixture is returned. Override to adapt.</returns>
        protected virtual Assembly GetTypeMapAssembly()
        {
            return this.GetType().Assembly;
        }

        public override void SetUp()
        {
            base.SetUp();
            iftFactory = scope.Resolve<InterfaceType.Factory>();
            InitStreams();
        }

        private void InitStreams()
        {
            ms = new MemoryStream();
            sw = scope.Resolve<ZetboxStreamWriter.Factory>().Invoke(new BinaryWriter(ms));
            sr = scope.Resolve<ZetboxStreamReader.Factory>().Invoke(new BinaryReader(ms));
        }

        protected void TestStream<T>(Action<T> write, Func<T> read, params T[] values)
        {
            Assert.That(values, Is.Not.Empty, "need values to test");
            foreach (var v in values)
            {
                InitStreams();
                write(v);
                ms.Seek(0, SeekOrigin.Begin);
                var output = read();
                Assert.That(output, Is.EqualTo(v));
            }
        }
    }
}
