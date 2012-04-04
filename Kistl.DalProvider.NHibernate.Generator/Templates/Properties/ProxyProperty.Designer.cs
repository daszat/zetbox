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
		protected bool isCalculated;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ProxyProperty", ctx, serializationList, moduleNamespace, propertyType, propertyName, overrideParent, useEvents, hasDefaultValue, interfaceName, className, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName, isCalculated);
        }

        public ProxyProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated)
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
			this.isCalculated = isCalculated;

        }

        public override void Generate()
        {
#line 30 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  propertyType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return default(",  propertyType , ");\r\n");
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
#line 39 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (hasDefaultValue || isCalculated) { 
#line 40 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Fetch",  propertyName , "OrDefault();\r\n");
#line 41 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} else { 
#line 42 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Proxy.",  propertyName , ";\r\n");
#line 43 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 44 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnGetTemplate(); 
#line 45 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 50 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (!isCalculated) ApplyOnAllSetTemplate(); 
#line 51 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                if (Proxy.",  propertyName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = Proxy.",  propertyName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 55 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPreSetTemplate(); 
#line 56 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    Proxy.",  propertyName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (isCalculated) { 
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("			        _",  propertyName , "_IsDirty = false;\r\n");
#line 61 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 62 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
#line 63 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPostSetTemplate(); 
#line 64 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("				else \r\n");
this.WriteObjects("				{\r\n");
this.WriteObjects("					SetInitializedProperty(\"",  propertyName , "\");\r\n");
this.WriteObjects("				}\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 71 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (isCalculated) {  

#line 73 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("		private bool _",  propertyName , "_IsDirty = false;\r\n");
#line 74 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 75 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
#line 76 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyTailTemplate(); 
#line 77 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
AddSerialization(serializationList, propertyName, "Proxy." + propertyName); 
#line 78 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}