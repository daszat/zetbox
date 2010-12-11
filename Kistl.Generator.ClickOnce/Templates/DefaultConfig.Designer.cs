using System.Collections.Generic;
using Kistl.API.Server;


namespace Kistl.Generator.ClickOnce.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator.ClickOnce\Templates\DefaultConfig.cst")]
    public partial class DefaultConfig : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.API.IKistlContext ctx;


        public DefaultConfig(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }
        
        public override void Generate()
        {
#line 8 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\DefaultConfig.cst"
this.WriteObjects("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
this.WriteObjects("<KistlConfig xmlns=\"http://dasz.at/Kistl/\">\r\n");
this.WriteObjects("  <ConfigName>Default Client Configuration</ConfigName>\r\n");
this.WriteObjects("  <Client StartClient=\"false\" ThrowErrors=\"false\">\r\n");
this.WriteObjects("    <DocumentStore>DocumentStore\\Client\\</DocumentStore>\r\n");
this.WriteObjects("    <DevelopmentEnvironment>false</DevelopmentEnvironment>\r\n");
this.WriteObjects("    <Modules>\r\n");
this.WriteObjects("      <Module>Kistl.API.ApiModule, Kistl.API</Module>\r\n");
this.WriteObjects("      <Module>Kistl.API.Client.ClientApiModule, Kistl.API.Client</Module>\r\n");
this.WriteObjects("      <Module>Kistl.Client.ClientModule, Kistl.Client</Module>\r\n");
this.WriteObjects("      <Module>Kistl.Client.WPF.WPFModule, Kistl.Client.WPF</Module>\r\n");
this.WriteObjects("      <Module>Kistl.Objects.InterfaceModule, Kistl.Objects</Module>\r\n");
this.WriteObjects("      <Module>Kistl.Objects.ClientModule, Kistl.Objects.ClientImpl</Module>\r\n");
this.WriteObjects("      <Module>Kistl.Objects.MemoryModule, Kistl.Objects.MemoryImpl</Module>\r\n");
this.WriteObjects("      <Module>Kistl.DalProvider.Client.ClientProvider, Kistl.DalProvider.ClientObjects</Module>\r\n");
this.WriteObjects("      <Module>Kistl.DalProvider.Memory.MemoryProvider, Kistl.DalProvider.Memory</Module>\r\n");
this.WriteObjects("      <Module>Kistl.App.Projekte.Client.CustomClientActionsModule, Kistl.App.Projekte.Client</Module>\r\n");
this.WriteObjects("      <Module>Kistl.App.Projekte.Common.CustomCommonActionsModule, Kistl.App.Projekte.Common</Module>\r\n");
this.WriteObjects("      <Module>Ini50.App.Client.Ini50Module, Ini50.App.Client</Module>\r\n");
this.WriteObjects("    </Modules>\r\n");
this.WriteObjects("  </Client>\r\n");
this.WriteObjects("  <SourceFileLocation>\r\n");
this.WriteObjects("    <!-- default probe path -->\r\n");
this.WriteObjects("    <string>ZBox\\CodeGen</string>\r\n");
this.WriteObjects("    <!-- common libaries -->\r\n");
this.WriteObjects("    <string>ZBox\\Common</string>\r\n");
this.WriteObjects("  </SourceFileLocation>\r\n");
this.WriteObjects("</KistlConfig>");

        }



    }
}