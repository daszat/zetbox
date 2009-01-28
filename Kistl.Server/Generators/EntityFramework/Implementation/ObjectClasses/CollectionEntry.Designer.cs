using System;
using System.Collections.Generic;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.Movables;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst")]
    public partial class CollectionEntry : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected NewRelation rel;


        public CollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, NewRelation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
#line 19 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
bool isList = rel.A.HasPersistentOrder || rel.B.HasPersistentOrder;
	string ceInterface = isList ? "INewListEntry" : "INewCollectionEntry";
	string ceName = rel.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
	
	List <string> fields = new List<string>();

#line 25 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    [EdmEntityType(NamespaceName=\"Model\", Name=\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  ceName , "\r\n");
this.WriteObjects("        : BaseServerCollectionEntry_EntityFramework, ",  ceInterface , "<",  rel.A.Type.ClassName , ", ",  rel.B.Type.ClassName , ">\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
#line 33 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);

#line 35 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to A part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 40 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"A", rel.GetCollectionEntryAssociationName(rel.A), "CollectionEntry",
			rel.A.Type.NameDataObject, rel.A.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix);
		
		fields.Add("_fk_A");

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Index on the A-side list of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
			typeof(int?), "AIndex");
		
		fields.Add("_AIndex");

#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to B part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 61 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"B", rel.GetCollectionEntryAssociationName(rel.B), "CollectionEntry",
			rel.B.Type.NameDataObject, rel.B.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix);
		
		fields.Add("_fk_B");

#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Index on the B-side list of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 72 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
			typeof(int?), "BIndex");
		
		fields.Add("_BIndex");

#line 77 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 82 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.ToStream, fields);
		
		CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.FromStream, fields);

#line 88 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("    }\r\n");

        }



    }
}