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
                 "Kistl.Server.Generators.EntityFramework",
                 "Implementation.EfModel.ModelCsdl",
                 "Model.csdl",
                 TaskEnum.Server.ToNameSpace(),
                 ctx);
            gen.ExecuteTemplate();

            gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                 "Implementation.EfModel.ModelMsl",
                 "Model.msl",
                 TaskEnum.Server.ToNameSpace(),
                 ctx);
            gen.ExecuteTemplate();

            gen = Generator.GetTemplateGenerator(
                 "Kistl.Server.Generators.EntityFramework",
                 "Implementation.EfModel.ModelSsdl",
                 "Model.ssdl",
                 TaskEnum.Server.ToNameSpace(),
                 ctx);
            gen.ExecuteTemplate();

            gen = Generator.GetTemplateGenerator(
                "Kistl.Server.Generators.EntityFramework",
                "Implementation.ObjectClasses.AssociationSetAttributes",
                "AssociationSetAttributes.cs",
                TaskEnum.Server.ToNameSpace(),
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
