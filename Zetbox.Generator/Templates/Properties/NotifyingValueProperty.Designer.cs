using System;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst")]
    public partial class NotifyingValueProperty : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected Serialization.SerializationMembersList serializationList;
		protected string type;
		protected string name;
		protected string modulenamespace;
		protected string backingName;
		protected bool isCalculated;
		protected bool disableExport;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string type, string name, string modulenamespace, string backingName, bool isCalculated, bool disableExport)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.NotifyingValueProperty", ctx, serializationList, type, name, modulenamespace, backingName, isCalculated, disableExport);
        }

        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Serialization.SerializationMembersList serializationList, string type, string name, string modulenamespace, string backingName, bool isCalculated, bool disableExport)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.type = type;
			this.name = name;
			this.modulenamespace = modulenamespace;
			this.backingName = backingName;
			this.isCalculated = isCalculated;
			this.disableExport = disableExport;

        }

        public override void Generate()
        {
#line 35 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        // BEGIN ",  this.GetType() , "\r\n");
#line 38 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyRequisitesTemplate();

    ApplyAttributesTemplate();


#line 43 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        ",  GetModifiers() , " ",  type , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
#line 47 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplySecurityCheckTemplate(); 
#line 48 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                // create local variable to create single point of return\r\n");
this.WriteObjects("                // for the benefit of down-stream templates\r\n");
this.WriteObjects("                var __result = ",  backingName , ";\r\n");
#line 51 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyOnGetTemplate(); 
#line 52 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                return __result;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.IsReadonly) throw new ReadOnlyObjectException();\r\n");
#line 57 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
if (!isCalculated) ApplyOnAllSetTemplate(); 
#line 58 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                if (",  backingName , " != value)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    var __oldValue = ",  backingName , ";\r\n");
this.WriteObjects("                    var __newValue = value;\r\n");
#line 62 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
if (!isCalculated) ApplyPreSetTemplate(); 
#line 63 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                    NotifyPropertyChanging(\"",  name , "\", __oldValue, __newValue);\r\n");
this.WriteObjects("                    ",  backingName , " = __newValue;\r\n");
this.WriteObjects("                    NotifyPropertyChanged(\"",  name , "\", __oldValue, __newValue);\r\n");
#line 66 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
if (!isCalculated) { 
#line 67 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                    if(IsAttached) UpdateChangedInfo = true;\r\n");
#line 68 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
} else { 
#line 69 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                    ",  backingName , "_IsDirty = false;\r\n");
#line 70 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
} 
#line 71 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("\r\n");
#line 72 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
if (!isCalculated) ApplyPostSetTemplate(); 
#line 73 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("                }\r\n");
this.WriteObjects("                else\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    SetInitializedProperty(\"",  name , "\");\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 80 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyBackingStoreDefinition(); 
#line 81 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyBackingStoreIsDirtyDefinition(); 
#line 82 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
ApplyTailTemplate(); 
#line 83 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
AddSerialization(serializationList, name); 
#line 84 "D:\Projects\zetbox\Zetbox.Generator\Templates\Properties\NotifyingValueProperty.cst"
this.WriteObjects("        // END ",  this.GetType() , "\r\n");

        }

    }
}