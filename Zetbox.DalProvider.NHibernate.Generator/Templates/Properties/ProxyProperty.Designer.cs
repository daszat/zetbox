using System;
using System.Collections.Generic;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst")]
    public partial class ProxyProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList;
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
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ProxyProperty", ctx, serializationList, moduleNamespace, propertyType, propertyName, overrideParent, useEvents, hasDefaultValue, interfaceName, className, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName, isCalculated, disableExport);
        }

        public ProxyProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated, bool disableExport)
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
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 47 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
this.WriteObjects("        ",  GetModifiers() , " ",  propertyType , " ",  propertyName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
#line 55 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (hasDefaultValue || isCalculated) { 
#line 56 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Fetch",  propertyName , "OrDefault();\r\n");
#line 57 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} else { 
#line 58 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Proxy.",  propertyName , ";\r\n");
#line 59 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 60 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnGetTemplate(); 
#line 61 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 66 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (!isCalculated) ApplyOnAllSetTemplate(); 
#line 67 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                if (Proxy.",  propertyName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = Proxy.",  propertyName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 71 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPreSetTemplate(); 
#line 72 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    Proxy.",  propertyName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, __newValue);\r\n");
#line 75 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (!isCalculated) { 
#line 76 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    if(IsAttached) UpdateChangedInfo = true;\r\n");
#line 77 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} else { 
#line 78 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    _",  propertyName , "_IsDirty = false;\r\n");
#line 79 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 80 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
#line 81 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPostSetTemplate(); 
#line 82 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    SetInitializedProperty(\"",  propertyName , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 89 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (isCalculated) {

#line 91 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        private bool _",  propertyName , "_IsDirty = false;\r\n");
#line 92 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 93 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\r\n");
#line 94 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyTailTemplate(); 
#line 95 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
AddSerialization(serializationList, propertyName, "Proxy." + propertyName); 
#line 96 "D:\Projects\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}