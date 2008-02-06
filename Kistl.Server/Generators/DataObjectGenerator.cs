using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public enum ClientServerEnum
    {
        Client,
        Server,
    }

    public interface IDataObjectGenerator
    {
        void Generate(Kistl.API.Server.KistlDataContext ctx, string path);
    }

    public sealed class DataObjectGeneratorFactory
    {
        public static IDataObjectGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDataObjectGenerator();
        }

        /// <summary>
        /// prevent this class from being instantiated
        /// </summary>
        private DataObjectGeneratorFactory() { }
    }
}
