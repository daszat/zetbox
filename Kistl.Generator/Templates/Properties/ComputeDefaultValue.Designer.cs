using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst")]
    public partial class ComputeDefaultValue : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string interfaceName;
		protected string className;
		protected string propertyName;
		protected bool isNullable;
		protected string isSetFlagName;
		protected Guid propertyGuid;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string className, string propertyName, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ComputeDefaultValue", ctx, interfaceName, className, propertyName, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName);
        }

        public ComputeDefaultValue(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string interfaceName, string className, string propertyName, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.interfaceName = interfaceName;
			this.className = className;
			this.propertyName = propertyName;
			this.isNullable = isNullable;
			this.isSetFlagName = isSetFlagName;
			this.propertyGuid = propertyGuid;
			this.backingStoreType = backingStoreType;
			this.backingStoreName = backingStoreName;

        }

        public override void Generate()
        {
#line 21 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst"
this.WriteObjects("                if (!",  isSetFlagName , " && ObjectState == DataObjectState.New) {\r\n");
this.WriteObjects("                    var __p = FrozenContext.FindPersistenceObject<Kistl.App.Base.Property>(new Guid(\"",  propertyGuid , "\"));\r\n");
this.WriteObjects("                    if (__p != null) {\r\n");
this.WriteObjects("                        ",  isSetFlagName , " = true;\r\n");
this.WriteObjects("                        // http://connect.microsoft.com/VisualStudio/feedback/details/593117/cannot-directly-cast-boxed-int-to-nullable-enum\r\n");
this.WriteObjects("                        object __tmp_value = __p.DefaultValue.GetDefaultValue();\r\n");
#line 27 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst"
if (isNullable) { 
#line 28 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst"
this.WriteObjects("                            if (__tmp_value == null)\r\n");
this.WriteObjects("                                __result = this.",  backingStoreName , " = null;\r\n");
this.WriteObjects("                            else\r\n");
this.WriteObjects("    ");
#line 31 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst"
} // Fix indent for next line 
#line 32 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/Properties/ComputeDefaultValue.cst"
this.WriteObjects("                        __result = this.",  backingStoreName , " = (",  backingStoreType.TrimEnd('?') , ")__tmp_value;\r\n");
this.WriteObjects("                    } else {\r\n");
this.WriteObjects("                        Kistl.API.Utils.Logging.Log.Warn(\"Unable to get default value for property '",  interfaceName , ".",  propertyName , "'\");\r\n");
this.WriteObjects("                    }\r\n");
this.WriteObjects("                }\r\n");

        }

    }
}