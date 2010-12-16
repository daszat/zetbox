using System.Collections.Generic;
using Kistl.API.Server;


namespace Kistl.Generator.ClickOnce.Templates
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator.ClickOnce\Templates\Program.cst")]
    public partial class Program : Kistl.Generator.ResourceTemplate
    {
		protected Kistl.API.IKistlContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Program", ctx);
        }

        public Program(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 8 "P:\Kistl\Kistl.Generator.ClickOnce\Templates\Program.cst"
this.WriteObjects("namespace ClickOnceTest\r\n");
this.WriteObjects("{\r\n");
this.WriteObjects("    using System;\r\n");
this.WriteObjects("    using System.Collections.Generic;\r\n");
this.WriteObjects("    using System.Linq;\r\n");
this.WriteObjects("    using System.Text;\r\n");
this.WriteObjects("    using System.Reflection;\r\n");
this.WriteObjects("    using System.Windows;\r\n");
this.WriteObjects("    using System.Security;\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    public class Program\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("        [STAThread]\r\n");
this.WriteObjects("        public static void Main(string[] args)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            try\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                var info = new AppDomainSetup();\r\n");
this.WriteObjects("                info.ApplicationBase = \"ZBox\\\\Client\";\r\n");
this.WriteObjects("                info.PrivateBinPath = \"ZBox\\\\Client\";\r\n");
this.WriteObjects("                info.ConfigurationFile = \"Kistl.Client.WPF.exe.config\";\r\n");
this.WriteObjects("                AppDomain d = AppDomain.CreateDomain(\"ZBox Client\", null, info, new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted));\r\n");
this.WriteObjects("                d.ExecuteAssembly(\"ZBox\\\\Client\\\\Kistl.Client.WPF.exe\");\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            catch (Exception ex)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                MessageBox.Show(ex.ToString());\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("}\r\n");

        }

    }
}