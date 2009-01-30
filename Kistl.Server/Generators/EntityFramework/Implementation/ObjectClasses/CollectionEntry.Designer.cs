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
#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
bool isList = rel.A.HasPersistentOrder || rel.B.HasPersistentOrder;
	string ceInterface = isList ? "INewListEntry" : "INewCollectionEntry";
	string ceName = rel.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
	
	List <string> fields = new List<string>();

#line 24 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    [EdmEntityType(NamespaceName=\"Model\", Name=\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  ceName , "\r\n");
this.WriteObjects("        : BaseServerCollectionEntry_EntityFramework, ",  ceInterface , "<",  rel.A.Type.ClassName , ", ",  rel.B.Type.ClassName , ">\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to A part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"A", rel.GetCollectionEntryAssociationName(rel.A), rel.A.RoleName,
			rel.A.Type.NameDataObject, rel.A.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
			rel.A.HasPersistentOrder);
		
		fields.Add("_fk_A");

		if (rel.A.HasPersistentOrder)
		{
			fields.Add("_A" + Kistl.API.Helper.PositionSuffix);

#line 50 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("		public int? AIndex { get { return A",  Kistl.API.Helper.PositionSuffix , "; } set { A",  Kistl.API.Helper.PositionSuffix , " = value; } }\r\n");
#line 52 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
}
		else if (isList)
		{

#line 56 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("		/// <summary>ignored implementation for INewListEntry</summary>\r\n");
this.WriteObjects("		public int? AIndex { get { return null; } set { } }\r\n");
#line 59 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
}


#line 62 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to B part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 67 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"B", rel.GetCollectionEntryAssociationName(rel.B), rel.B.RoleName,
			rel.B.Type.NameDataObject, rel.B.Type.NameDataObject + Kistl.API.Helper.ImplementationSuffix,
			rel.B.HasPersistentOrder);
		
		fields.Add("_fk_B");

		if (rel.B.HasPersistentOrder)
		{
			fields.Add("_B" + Kistl.API.Helper.PositionSuffix);

#line 78 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("		public int? BIndex { get { return B",  Kistl.API.Helper.PositionSuffix , "; } set { B",  Kistl.API.Helper.PositionSuffix , " = value; } }\r\n");
#line 80 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
}
		else if (isList)
		{

#line 84 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("		/// <summary>ignored implementation for INewListEntry</summary>\r\n");
this.WriteObjects("		public int? BIndex { get { return null; } set { } }\r\n");
#line 87 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
}


#line 90 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 95 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.ToStream, fields);
		
		CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.FromStream, fields);

#line 101 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("    }\r\n");

        }



    }
}