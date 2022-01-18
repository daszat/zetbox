using System;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Mappings
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.core\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\VersionColumnHbm.cst")]
    public partial class VersionColumnHbm : Zetbox.Generator.ResourceTemplate
    {


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Mappings.VersionColumnHbm");
        }

        public VersionColumnHbm(Arebis.CodeGeneration.IGenerationHost _host)
            : base(_host)
        {

        }

        public override void Generate()
        {
#line 22 "D:\Projects\zetbox.core\Zetbox.DalProvider.NHibernate.Generator\Templates\Mappings\VersionColumnHbm.cst"
this.WriteObjects("\r\n");
this.WriteObjects("      <version name=\"ChangedOn\" type=\"Timestamp\"\r\n");
this.WriteObjects("                column=\"`ChangedOn`\" />\r\n");

        }

    }
}