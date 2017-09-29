using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst")]
    public partial class OnPropertyChange : Zetbox.Generator.ResourceTemplate
    {
		protected IZetboxContext ctx;
		protected DataType dt;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.OnPropertyChange", ctx, dt);
        }

        public OnPropertyChange(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt)
            : base(_host)
        {
			this.ctx = ctx;
			this.dt = dt;

        }

        public override void Generate()
        {
#line 30 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var recalcProps = GetRecalcProperties();                                                                         
#line 31 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var auditProps = GetAuditProperties();                                                                           
#line 32 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var nonModProps = GetNonModifyingProperties();                                                                   
#line 33 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
#line 35 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (auditProps.Count > 0 && !(dt is CompoundObject)) {                                                           
#line 36 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        protected override void OnPropertyChanged(string property, object oldValue, object newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.OnPropertyChanged(property, oldValue, newValue);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // Do not audit calculated properties\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 43 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in auditProps ) {                                                                         
#line 44 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 45 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 46 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    AuditPropertyChange(property, oldValue, newValue);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 50 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 51 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (recalcProps.Count > 0 && !(dt is CompoundObject)) {                                                          
#line 52 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void Recalculate(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 57 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in recalcProps) {                                                                         
#line 58 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 59 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanging(prop);                                                                      
#line 60 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    _",  prop.Name , "_IsDirty = true;\r\n");
#line 61 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanged(prop);                                                                       
#line 62 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return;\r\n");
#line 63 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 64 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            base.Recalculate(property);\r\n");
this.WriteObjects("        }\r\n");
#line 68 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 69 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (nonModProps.Count > 0) {                                                                                     
#line 70 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        protected override bool ShouldSetModified(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 75 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in nonModProps) {                                                                         
#line 76 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 77 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 78 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return false;\r\n");
this.WriteObjects("                default:\r\n");
this.WriteObjects("                    return base.ShouldSetModified(property);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 83 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 84 "C:\Projects\zetbox\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}