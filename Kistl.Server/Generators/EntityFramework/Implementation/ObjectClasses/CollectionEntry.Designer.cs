using System;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst")]
    public partial class CollectionEntry : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected ObjectReferenceProperty Property;


        public CollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectReferenceProperty Property)
            : base(_host)
        {
			this.ctx = ctx;
			this.Property = Property;

        }
        
        public override void Generate()
        {
#line 12 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("\r\n");
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
var info = AssociationInfo.CreateInfo(ctx, this.Property);

#line 18 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\ObjectClasses\CollectionEntry.cst"
this.WriteObjects("    [System.Diagnostics.DebuggerDisplay(\"",  info.CollectionEntry.ClassName , "\")]\r\n");
this.WriteObjects("    [EdmEntityTypeAttribute(NamespaceName=\"Model\", Name=\"",  info.CollectionEntry.ClassName , "\")]\r\n");
this.WriteObjects("    public class ",  info.CollectionEntry.ClassName , "",  Kistl.API.Helper.ImplementationSuffix , "\r\n");
this.WriteObjects("        : BaseServerCollectionEntry_EntityFramework, ICollectionEntry<",  Property.ReferenceObjectClass.GetDataTypeString() , ", ",  info.Parent.Type.NameDataObject , ">\r\n");
this.WriteObjects("    {\r\n");
this.WriteObjects("    \r\n");
this.WriteObjects("        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]\r\n");
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
this.WriteObjects("\r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  Property.ReferenceObjectClass.GetDataTypeString() , " Value\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ValueImpl;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ValueImpl = (",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [XmlIgnore()]\r\n");
this.WriteObjects("        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]\r\n");
this.WriteObjects("        public ",  info.Parent.Type.NameDataObject , " Parent\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                return ParentImpl;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                ParentImpl = (",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        public int fk_Value\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Value != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    _fk_Value = Value.ID;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return _fk_Value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _fk_Value = value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _fk_Value;\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("        public int fk_Parent\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && Parent != null)\r\n");
this.WriteObjects("                {\r\n");
this.WriteObjects("                    _fk_Parent = Parent.ID;\r\n");
this.WriteObjects("                }\r\n");
this.WriteObjects("                return _fk_Parent;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                _fk_Parent = value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        private int _fk_Parent;\r\n");
this.WriteObjects("                \r\n");
this.WriteObjects("        [EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  info.AssociationName , "\", \"",  info.Child.RoleName , "\")]\r\n");
this.WriteObjects("        public ",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , " ValueImpl\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , ">(\r\n");
this.WriteObjects("                        \"Model.",  info.AssociationName , "\",\r\n");
this.WriteObjects("                        \"",  info.Child.RoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded)\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                return r.Value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , ">(\r\n");
this.WriteObjects("                        \"Model.",  info.AssociationName , "\",\r\n");
this.WriteObjects("                        \"",  info.Child.RoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded)\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                r.Value = (",  Property.ReferenceObjectClass.GetDataTypeString() , "",  Kistl.API.Helper.ImplementationSuffix , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        [EdmRelationshipNavigationPropertyAttribute(\"Model\", \"",  info.AssociationName , "\", \"",  info.Parent.RoleName , "\")]\r\n");
this.WriteObjects("        public ",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , " ParentImpl\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            get\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , ">(\r\n");
this.WriteObjects("                        \"Model.",  info.AssociationName , "\",\r\n");
this.WriteObjects("                        \"",  info.Parent.RoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded)\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                return r.Value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("            set\r\n");
this.WriteObjects("            {\r\n");
this.WriteObjects("                EntityReference<",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , "> r\r\n");
this.WriteObjects("                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , ">(\r\n");
this.WriteObjects("                    \"Model.",  info.AssociationName , "\",\r\n");
this.WriteObjects("                    \"",  info.Parent.RoleName , "\");\r\n");
this.WriteObjects("                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged) && !r.IsLoaded)\r\n");
this.WriteObjects("                    r.Load(); \r\n");
this.WriteObjects("                r.Value = (",  info.Parent.Type.NameDataObject , "",  Kistl.API.Helper.ImplementationSuffix , ")value;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        public override void ToStream(System.IO.BinaryWriter sw)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.ToStream(sw);\r\n");
this.WriteObjects("            BinarySerializer.ToBinary(this.fk_Value, sw);\r\n");
this.WriteObjects("            BinarySerializer.ToBinary(this.fk_Parent, sw);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("        public override void FromStream(System.IO.BinaryReader sr)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.FromStream(sr);\r\n");
this.WriteObjects("            BinarySerializer.FromBinary(out this._fk_Value, sr);\r\n");
this.WriteObjects("            BinarySerializer.FromBinary(out this._fk_Parent, sr);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("        \r\n");
this.WriteObjects("    }\r\n");
this.WriteObjects("\r\n");

        }



    }
}