using System;
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
		protected FullRelation rel;


        public CollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, FullRelation rel)
            : base(_host)
        {
			this.ctx = ctx;
			this.rel = rel;

        }
        
        public override void Generate()
        {
#line 13 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
string leftType = rel.Left.Referenced.ClassName;
	string rightType = rel.Right.Referenced.ClassName;


#line 22 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    [EdmEntityType(NamespaceName=\"Model\", Name=\"",  rel.GetCollectionEntryClassName() , "\")]\r\n");
this.WriteObjects("    public class ",  rel.GetCollectionEntryClassName() , "",  Kistl.API.Helper.ImplementationSuffix , "\r\n");
this.WriteObjects("        : BaseServerCollectionEntry_EntityFramework, INewCollectionEntry<",  leftType , ", ",  rightType , ">\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]\r\n");
this.WriteObjects("        public override int ID\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return _ID;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _ID = value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _ID;\r\n");
this.WriteObjects("        \r\n");
#line 45 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"Right", rel.GetRightToCollectionEntryAssociationName(), "Right",
			rel.Right.Referenced.ClassName, rel.Right.Referenced.ClassName + Kistl.API.Helper.ImplementationSuffix);
			
		CallTemplate("Implementation.ObjectClasses.ObjectReferencePropertyTemplate", ctx,
			"Left", rel.GetLeftToCollectionEntryAssociationName(), "Left",
			rel.Left.Referenced.ClassName, rel.Left.Referenced.ClassName + Kistl.API.Helper.ImplementationSuffix);

#line 53 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void ToStream(System.IO.BinaryWriter sw)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ToStream(sw);\r\n");
this.WriteObjects("            BinarySerializer.ToBinary(this._fk_Left, sw);\r\n");
this.WriteObjects("            BinarySerializer.ToBinary(this._fk_Right, sw);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        public override void FromStream(System.IO.BinaryReader sr)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.FromStream(sr);\r\n");
this.WriteObjects("            BinarySerializer.FromBinary(out this._fk_Left, sr);\r\n");
this.WriteObjects("            BinarySerializer.FromBinary(out this._fk_Right, sr);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");

        }



    }
}