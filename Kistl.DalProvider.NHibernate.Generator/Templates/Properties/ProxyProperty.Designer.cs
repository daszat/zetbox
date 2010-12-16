using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst")]
    public partial class ProxyProperty : Kistl.Generator.MemberTemplate
    {
		protected IKistlContext ctx;
		protected string propertyType;
		protected string propertyName;
		protected bool overrideParent;
		protected bool useEvents;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propertyType, string propertyName, bool overrideParent, bool useEvents)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ProxyProperty", ctx, propertyType, propertyName, overrideParent, useEvents);
        }

        public ProxyProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propertyType, string propertyName, bool overrideParent, bool useEvents)
            : base(_host)
        {
			this.ctx = ctx;
			this.propertyType = propertyType;
			this.propertyName = propertyName;
			this.overrideParent = overrideParent;
			this.useEvents = useEvents;

        }

        public override void Generate()
        {
#line 19 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  propertyType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
this.WriteObjects("                var __result = Proxy.",  propertyName , ";\r\n");
#line 28 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnGetTemplate();

#line 30 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("                if (Proxy.",  propertyName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = Proxy.",  propertyName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyPreSetTemplate();

#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    Proxy.",  propertyName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
#line 47 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyPostSetTemplate();

#line 49 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}