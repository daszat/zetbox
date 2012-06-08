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
    [Arebis.CodeGeneration.TemplateInfo(@"P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst")]
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


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.ProxyProperty", ctx, serializationList, moduleNamespace, propertyType, propertyName, overrideParent, useEvents, hasDefaultValue, interfaceName, className, isNullable, isSetFlagName, propertyGuid, backingStoreType, backingStoreName, isCalculated);
        }

        public ProxyProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Zetbox.Generator.Templates.Serialization.SerializationMembersList serializationList, string moduleNamespace, string propertyType, string propertyName, bool overrideParent, bool useEvents, bool hasDefaultValue, string interfaceName, string className, bool isNullable, string isSetFlagName, Guid propertyGuid, string backingStoreType, string backingStoreName, bool isCalculated)
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
#line 17 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("");
#line 46 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\n");
this.WriteObjects("        // BEGIN ",  this.GetType() , "\n");
this.WriteObjects("        ",  GetModifiers() , " ",  propertyType , " ",  propertyName , "\n");
this.WriteObjects("        {\n");
this.WriteObjects("            get\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (!CurrentAccessRights.HasReadRights()) return default(",  propertyType , ");\n");
this.WriteObjects("                // create local variable to create single point of return\n");
this.WriteObjects("                // for the benefit of down-stream templates\n");
#line 55 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (hasDefaultValue || isCalculated) { 
#line 56 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Fetch",  propertyName , "OrDefault();\n");
#line 57 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} else { 
#line 58 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                var __result = Proxy.",  propertyName , ";\n");
#line 59 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 60 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyOnGetTemplate(); 
#line 61 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                return __result;\n");
this.WriteObjects("            }\n");
this.WriteObjects("            set\n");
this.WriteObjects("            {\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\n");
#line 66 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (!isCalculated) ApplyOnAllSetTemplate(); 
#line 67 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                if (Proxy.",  propertyName , " != value)\n");
this.WriteObjects("                {\n");
this.WriteObjects("                    var __oldValue = Proxy.",  propertyName , ";\n");
this.WriteObjects("                    var __newValue = value;\n");
#line 71 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPreSetTemplate(); 
#line 72 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  propertyName , "\", __oldValue, __newValue);\n");
this.WriteObjects("                    Proxy.",  propertyName , " = __newValue;\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  propertyName , "\", __oldValue, __newValue);\n");
#line 75 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (isCalculated) { 
#line 76 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("			        _",  propertyName , "_IsDirty = false;\n");
#line 77 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 78 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\n");
#line 79 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if(!isCalculated) ApplyPostSetTemplate(); 
#line 80 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("                }\n");
this.WriteObjects("				else \n");
this.WriteObjects("				{\n");
this.WriteObjects("					SetInitializedProperty(\"",  propertyName , "\");\n");
this.WriteObjects("				}\n");
this.WriteObjects("            }\n");
this.WriteObjects("        }\n");
#line 87 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
if (isCalculated) {  

#line 89 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("		private bool _",  propertyName , "_IsDirty = false;\n");
#line 90 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
} 
#line 91 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("\n");
#line 92 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
ApplyTailTemplate(); 
#line 93 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
AddSerialization(serializationList, propertyName, "Proxy." + propertyName); 
#line 94 "P:\zetbox\Zetbox.DalProvider.NHibernate.Generator\Templates\Properties\ProxyProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\n");

        }

    }
}