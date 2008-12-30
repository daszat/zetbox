using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators
{
    public interface IMappingGenerator
    {
        void Generate(Kistl.API.IKistlContext ctx, string path);
    }

    public class BaseMappingGenerator : IMappingGenerator
    {
        #region IMappingGenerator Members

        public void Generate(Kistl.API.IKistlContext ctx, string path)
        {
            var gen = Generator.GetTemplateGenerator(
                                        "Server.EfModel.ModelCsdl",
                                        "Model.csdl",
                                        Kistl.Server.GeneratorsOld.TaskEnum.Server.ToNameSpace(),
                                        ctx);
            gen.ExecuteTemplate();
        }

        #endregion
    }


    public static class MappingGeneratorFactory
    {
        public static IMappingGenerator GetGenerator()
        {
            return new BaseMappingGenerator();
        }
    }
}
