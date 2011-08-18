using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst")]
    public partial class GetDeletedRelatives : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected ObjectClass cls;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.GetDeletedRelatives", ctx, cls);
        }

        public GetDeletedRelatives(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host)
        {
			this.ctx = ctx;
			this.cls = cls;

        }

        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("        public override List<NHibernatePersistenceObject> GetParentsToDelete()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            var result = base.GetParentsToDelete();\r\n");
this.WriteObjects("\r\n");
#line 20 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
foreach (var rel in cls.GetRelations()) {                                                                            
#line 21 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (rel.Storage == StorageType.Separate) continue; // handled by cascading in the mapping                       
#line 22 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var relEnd = rel.GetEndFromClass(cls);                                                                          
#line 23 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var otherEnd = rel.GetOtherEndFromClass(cls);                                                                   
#line 24 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (relEnd.Navigator == null && otherEnd.Navigator == null)                                                     
#line 25 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
throw new NotImplementedException(string.Format("Cannot handle Relation#{0} without Navigators", rel.ID));  
#line 26 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var haveStorage = (rel.Storage == StorageType.MergeIntoA && rel.A == relEnd)                                    
#line 27 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
|| (rel.Storage == StorageType.MergeIntoB && rel.B == relEnd);                                              
#line 28 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (!haveStorage) continue; // we are parent, skip this                                                         
#line 29 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("\r\n");
#line 30 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (relEnd.Navigator != null) {                                                                                 
#line 31 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("            if (this.",  relEnd.Navigator.Name , " != null && this.",  relEnd.Navigator.Name , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  relEnd.Navigator.Name , ");\r\n");
#line 33 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
} else {                                                                                                        
#line 34 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
GenerateParentSearch(otherEnd.Type.GetDataTypeString(), otherEnd.Navigator.Name, relEnd.Multiplicity.UpperBound()); 
#line 35 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
}                                                                                                               
#line 36 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
}                                                                                                                    
#line 37 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            return result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        public override List<NHibernatePersistenceObject> GetChildrenToDelete()\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            var result = base.GetChildrenToDelete();\r\n");
this.WriteObjects("\r\n");
#line 45 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
foreach (var rel in cls.GetRelations()) {                                                                            
#line 46 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (rel.Storage == StorageType.Separate) continue; // handled by cascading in the mapping                       
#line 47 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var relEnd = rel.GetEndFromClass(cls);                                                                          
#line 48 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var otherEnd = rel.GetOtherEndFromClass(cls);                                                                   
#line 49 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (relEnd.Navigator == null && otherEnd.Navigator == null)                                                     
#line 50 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
throw new NotImplementedException(string.Format("Cannot handle Relation#{0} without Navigators", rel.ID));  
#line 51 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
var haveStorage = (rel.Storage == StorageType.MergeIntoA && rel.A == relEnd)                                    
#line 52 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
|| (rel.Storage == StorageType.MergeIntoB && rel.B == relEnd);                                              
#line 53 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (haveStorage) continue; // we are child, skip this                                                           
#line 54 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
if (relEnd.Navigator != null && otherEnd.Multiplicity == Multiplicity.One) {                                    
#line 55 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("            if (this.",  relEnd.Navigator.Name , " != null && this.",  relEnd.Navigator.Name , ".ObjectState == DataObjectState.Deleted)\r\n");
this.WriteObjects("                result.Add((NHibernatePersistenceObject)this.",  relEnd.Navigator.Name , ");\r\n");
#line 57 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
} else {                                                                                                        
#line 58 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
GenerateChildSearch(otherEnd.Type.GetDataTypeString(), otherEnd.Navigator.Name); 
#line 59 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
}                                                                                                               
#line 60 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
}                                                                                                                    
#line 61 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
this.WriteObjects("\r\n");
this.WriteObjects("            return result;\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");

        }
#line 66 "P:\Kistl\Kistl.DalProvider.NHibernate.Generator\Templates\ObjectClasses\GetDeletedRelatives.cst"
public void GenerateChildSearch(string datatype, string navigatorName)
        {
            this.WriteObjects("            result.AddRange(Context.AttachedObjects\r\n");
            this.WriteObjects("                .OfType<", datatype, ">()\r\n");
            this.WriteObjects("                .Where(child => child.", navigatorName, " == this\r\n");
            this.WriteObjects("                    && child.ObjectState == DataObjectState.Deleted)\r\n");
            this.WriteObjects("                .Cast<NHibernatePersistenceObject>());\r\n");
        }

        public void GenerateParentSearch(string datatype, string navigatorName, int multiplicityUpperBound)
        {
            this.WriteObjects("            result.AddRange(Context.AttachedObjects\r\n");
            this.WriteObjects("                .OfType<", datatype, ">()\r\n");
            this.WriteObjects("                .Where(parent => parent.", navigatorName, multiplicityUpperBound == 1 ? " == this" : ".Contains(this)", "\r\n");
            this.WriteObjects("                    && parent.ObjectState == DataObjectState.Deleted)\r\n");
            this.WriteObjects("                .Cast<NHibernatePersistenceObject>());\r\n");
        }



    }
}