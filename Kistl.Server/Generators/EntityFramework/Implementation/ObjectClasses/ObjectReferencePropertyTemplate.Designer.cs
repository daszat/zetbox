using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst")]
    public partial class ObjectReferencePropertyTemplate : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected Templates.Implementation.SerializationMembersList serializationList;
		protected String name;
		protected String associationName;
		protected String targetRoleName;
		protected String referencedInterface;
		protected String referencedImplementation;
		protected bool hasPositionStorage;


        public ObjectReferencePropertyTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Implementation.SerializationMembersList serializationList, String name, String associationName, String targetRoleName, String referencedInterface, String referencedImplementation, bool hasPositionStorage)
            : base(_host)
        {
			this.ctx = ctx;
			this.serializationList = serializationList;
			this.name = name;
			this.associationName = associationName;
			this.targetRoleName = targetRoleName;
			this.referencedInterface = referencedInterface;
			this.referencedImplementation = referencedImplementation;
			this.hasPositionStorage = hasPositionStorage;

        }
        
        public override void Generate()
        {
#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
string efName = name + Kistl.API.Helper.ImplementationSuffix;
	string fkName = "fk_" + name;
	string fkBackingName = "_fk_" + name;


#line 27 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // implement the user-visible interface\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  referencedInterface , " ",  name , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ",  efName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                // TODO: NotifyPropertyChanged()\r\n");
this.WriteObjects("                // TODO: only accept EF objects from same Context\r\n");
this.WriteObjects("                if (IsReadonly) throw new ReadOnlyObjectException();\r\n");
this.WriteObjects("                ",  efName , " = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        // provide a way to directly access the foreign key int\r\n");
this.WriteObjects("        public int? ",  fkName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) \r\n");
this.WriteObjects("                    && ",  name , " != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    ",  fkBackingName , " = ",  name , ".ID;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return ",  fkBackingName , ";\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ",  fkBackingName , " = value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int? ",  fkBackingName , ";\r\n");
#line 64 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
AddSerialization(serializationList, fkName);

#line 66 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        // EF sees only this property\r\n");
this.WriteObjects("        [EdmRelationshipNavigationProperty(\"Model\", \"",  associationName , "\", \"",  targetRoleName , "\")]\r\n");
this.WriteObjects("        public ",  referencedImplementation , " ",  efName , "\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return r.Value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  referencedImplementation , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  referencedImplementation , ">(\r\n");
this.WriteObjects("                        \"Model.",  associationName , "\",\r\n");
this.WriteObjects("                        \"",  targetRoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)\r\n");
this.WriteObjects("                    && !r.IsLoaded)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                r.Value = (",  referencedImplementation , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
#line 99 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
string posStorageName = name + Kistl.API.Helper.PositionSuffix;

	if (hasPositionStorage)
	{
		CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
		    serializationList,
			"int?", posStorageName);
	}

#line 108 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\ObjectReferencePropertyTemplate.cst"
this.WriteObjects("        \r\n");

        }



    }
}