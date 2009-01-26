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
string leftType = rel.Left.Referenced.ClassName;
	string rightType = rel.Right.Referenced.ClassName;
	List <string> fields = new List<string>();

#line 23 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    [EdmEntityType(NamespaceName=\"Model\", Name=\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  rel.GetCollectionEntryClassName() , "",  Kistl.API.Helper.ImplementationSuffix , "\r\n");
this.WriteObjects("        : BaseServerCollectionEntry_EntityFramework, INewCollectionEntry<",  leftType , ", ",  rightType , ">\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
#line 32 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.IdProperty", ctx);

#line 34 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to right part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 39 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"Right", rel.GetRightToCollectionEntryAssociationName(), "Right",
			rel.Right.Referenced.NameDataObject, rel.Right.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix);
		
		fields.Add("_fk_Right");
		

#line 46 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("		/// <summary>\r\n");
this.WriteObjects("		/// Reference to left part of this relation\r\n");
this.WriteObjects("		/// </summary>\r\n");
#line 51 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
if (rel.IsTwoProngedAssociation(ctx))
		{
			CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
				"Left", rel.GetLeftToCollectionEntryAssociationName(), "Left",
				rel.Left.Referenced.NameDataObject, rel.Left.Referenced.NameDataObject + Kistl.API.Helper.ImplementationSuffix);
			fields.Add("_fk_Left");
		}
		else
		{
			CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx,
				rel.Left.Referenced.ToSystemType(), "Left");
			fields.Add("_Left");
		}

#line 65 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#region Serializer\r\n");
this.WriteObjects("\r\n");
#line 69 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.ToStream, fields);
		
		CallTemplate("Implementation.ObjectClasses.SerializerTemplate", ctx,
			SerializerDirection.FromStream, fields);

#line 75 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("#endregion\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");

        }



    }
}