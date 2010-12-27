using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\SaveOrUpdateToMethod.cst")]
    public partial class SaveOrUpdateToMethod : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.SaveOrUpdateToMethod", ctx);
        }

        public SaveOrUpdateToMethod(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx)
            : base(_host)
        {
			this.ctx = ctx;

        }

        public override void Generate()
        {
#line 15 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\SaveOrUpdateToMethod.cst"
this.WriteObjects("        public override void SaveOrUpdateTo(global::NHibernate.ISession session)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (this.ObjectState)\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                case DataObjectState.New:\r\n");
this.WriteObjects("                    session.Save(this.Proxy);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("                case DataObjectState.Modified:\r\n");
this.WriteObjects("                    session.Update(this.Proxy);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("                case DataObjectState.Unmodified:\r\n");
this.WriteObjects("                    // ignore\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("                case DataObjectState.Deleted:\r\n");
this.WriteObjects("                    session.Delete(this.Proxy);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("                case DataObjectState.NotDeserialized:\r\n");
this.WriteObjects("                    throw new InvalidOperationException(\"object not deserialized\");\r\n");
this.WriteObjects("                default:\r\n");
this.WriteObjects("                    throw new NotImplementedException(String.Format(\"unknown DataObjectState encountered: '{0}'\", this.ObjectState));\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");

        }

    }
}