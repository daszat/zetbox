using System;
using System.Linq;
using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Base;
using Zetbox.Generator;
using Zetbox.Generator.Extensions;


namespace Zetbox.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst")]
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
#line 30 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var recalcProps = GetRecalcProperties();                                                                         
#line 31 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var auditProps = GetAuditProperties();                                                                           
#line 32 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
var nonModProps = GetNonModifyingProperties();                                                                   
#line 33 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
#line 35 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (auditProps.Count > 0 && !(dt is CompoundObject)) {                                                           
#line 36 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        protected override void OnPropertyChanged(string property, object oldValue, object newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            base.OnPropertyChanged(property, oldValue, newValue);\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            // Do not audit calculated properties\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 43 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in auditProps ) {                                                                         
#line 44 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 45 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 46 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    AuditPropertyChange(property, oldValue, newValue);\r\n");
this.WriteObjects("                    break;\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 50 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 51 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (recalcProps.Count > 0 && !(dt is CompoundObject)) {                                                          
#line 52 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        public override void Recalculate(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 57 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in recalcProps) {                                                                         
#line 58 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 59 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanging(prop);                                                                      
#line 60 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    _",  prop.Name , "_IsDirty = true;\r\n");
#line 61 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
ApplyNotifyPropertyChanged(prop);                                                                       
#line 62 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return;\r\n");
#line 63 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 64 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("            base.Recalculate(property);\r\n");
this.WriteObjects("        }\r\n");
#line 68 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 69 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (nonModProps.Count > 0) {                                                                                     
#line 70 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("\r\n");
this.WriteObjects("        protected override bool ShouldSetModified(string property)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 75 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in nonModProps) {                                                                         
#line 76 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
#line 77 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                           
#line 78 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                    return false;\r\n");
this.WriteObjects("                default:\r\n");
this.WriteObjects("                    return base.ShouldSetModified(property);\r\n");
this.WriteObjects("            }\r\n");
this.WriteObjects("        }\r\n");
#line 83 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
}                                                                                                                
#line 84 "D:\Projects\zetbox.net4\Zetbox.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");

        }

    }
}