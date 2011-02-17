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
		protected Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList;
		protected string moduleNamespace;
		protected string propertyType;
		protected string propertyName;
		protected bool overrideParent;
		protected bool useEvents;
		protected bool hasDefaultValue;
		protected string interfaceName;
		protected string className;
		protected bool isNullable;
		protected string isSetFlagName;
		protected Guid propertyGuid;
		protected string backingStoreType;
		protected string backingStoreName;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ProxyProperty", ctx, serializationList, moduleNamespace, propertyType, propertyName, overrideParent, useEvents, hasDefaultValue, interfaceName, className, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName);
        }

        public ProxyProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.moduleNamespace = moduleNamespace;
			this.propertyType = propertyType;
			this.propertyName = propertyName;
			this.overrideParent = overrideParent;
			this.useEvents = useEvents;
			this.hasDefaultValue = hasDefaultValue;
			this.interfaceName = interfaceName;
			this.className = className;
			this.isNullable = isNullable;
			this.isSetFlagName = isSetFlagName;
			this.propertyGuid = propertyGuid;
			this.backingStoreType = backingStoreType;
			this.backingStoreName = backingStoreName;

        }

        public override void Generate()
        {
#line 29 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  propertyType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (hasDefaultValue) { 
#line 38 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Fetch",  propertyName , "OrDefault();\r\n");
#line 39 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} else { 
#line 40 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Proxy.",  propertyName , ";\r\n");
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 42 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnGetTemplate(); 
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 48 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnAllSetTemplate(); 
#line 49 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                if (Proxy.",  propertyName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = Proxy.",  propertyName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyPreSetTemplate(); 
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    Proxy.",  propertyName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyPostSetTemplate(); 
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 61 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyTailTemplate(); 
#line 62 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
AddSerialization(serializationList, propertyName, "Proxy." + propertyName); 
#line 63 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}