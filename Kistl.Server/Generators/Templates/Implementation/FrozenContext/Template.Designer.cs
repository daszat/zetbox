

namespace Kistl.Server.Generators.Templates.Implementation.FrozenContext
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\temp\Kistl2\Kistl.Server\Generators\Templates\Implementation\FrozenContext\Template.cst")]
    public partial class Template : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected Kistl.API.IKistlContext ctx;


        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {

        }



    }
}