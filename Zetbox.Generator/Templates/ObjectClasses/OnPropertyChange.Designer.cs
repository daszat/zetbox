using System;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst")]
    public partial class OnPropertyChange : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected DataType dt;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.OnPropertyChange", ctx, dt);
        }

        public OnPropertyChange(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;

        }

        public override void Generate()
        {
#line 14 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var recalcProps = GetRecalcProperties();                                                                         
#line 15 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var auditProps = GetAuditProperties();                                                                           
#line 16 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var nonModProps = GetNonModifyingProperties();                                                                   
#line 17 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
#line 19 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (auditProps.Count > 0 && !(dt is CompoundObject)) {                                                           
#line 20 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        protected override void OnPropertyChanged(string property, object oldValue, object newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.OnPropertyChanged(property, oldValue, newValue);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // Do not audit calculated properties\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 27 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in auditProps ) {                                                                         
#line 28 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 29 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 30 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    AuditPropertyChange(property, oldValue, newValue);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 34 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 35 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (recalcProps.Count > 0 && !(dt is CompoundObject)) {                                                          
#line 36 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void Recalculate(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 41 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in recalcProps) {                                                                         
#line 42 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 43 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanging(prop);                                                                      
#line 44 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    _",  prop.Name , "_IsDirty = true;\r\n");
#line 45 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanged(prop);                                                                       
#line 46 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return;\r\n");
#line 47 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 48 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            base.Recalculate(property);\r\n");
this.WriteObjects("        }\r\n");
#line 52 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 53 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (nonModProps.Count > 0) {                                                                                     
#line 54 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        protected override bool ShouldSetModified(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 59 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in nonModProps) {                                                                         
#line 60 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 61 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 62 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return false;\r\n");
this.WriteObjects("                default:\r\n");
this.WriteObjects("                    return base.ShouldSetModified(property);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 67 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 68 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}