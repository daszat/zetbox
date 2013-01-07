using System;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\DiscriminatorColumnHbm.cst")]
    public partial class DiscriminatorColumnHbm : Zetbox.Generator.ResourceTemplate
    {
		protected Zetbox.App.Base.TableMapping mappingType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.App.Base.TableMapping mappingType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.DiscriminatorColumnHbm", mappingType);
        }

        public DiscriminatorColumnHbm(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.App.Base.TableMapping mappingType)
            : base(_host)
        {
			this.mappingType = mappingType;

        }

        public override void Generate()
        {
#line 23 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\DiscriminatorColumnHbm.cst"
if (mappingType == Zetbox.App.Base.TableMapping.TPH) { 
#line 24 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\DiscriminatorColumnHbm.cst"
this.WriteObjects("        <discriminator column=\"`",  Zetbox.API.Server.TableMapper.DiscriminatorColumnName , "`\"\r\n");
this.WriteObjects("                       type=\"String\" />\r\n");
this.WriteObjects("\r\n");
#line 27 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\DiscriminatorColumnHbm.cst"
} 

        }

    }
}