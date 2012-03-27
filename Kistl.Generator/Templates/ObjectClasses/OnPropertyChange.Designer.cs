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
var props = dt.Properties.OfType<ValueTypeProperty>().Where(p => !p.IsList && p.IsCalculated).ToList(); 
#line 15 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
if (props.Count > 0 && !(dt is CompoundObject)) { 
#line 16 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("        #region ",  this.GetType() , "\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        protected override void OnPropertyChanging(string property, object oldValue, object newValue)\r\n");
this.WriteObjects("        {\r\n");
this.WriteObjects("            switch (property)\r\n");
this.WriteObjects("            {\r\n");
#line 22 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
foreach (var prop in props) { 
#line 23 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("                case \"",  prop.Name , "\":\r\n");
this.WriteObjects("                    ",  prop.Name , "_IsDirty = true;\r\n");
this.WriteObjects("                    break;\r\n");
#line 26 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
} 
#line 27 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
this.WriteObjects("            }\r\n");
this.WriteObjects("            base.OnPropertyChanging(property, oldValue, newValue);\r\n");
this.WriteObjects("        }\r\n");
this.WriteObjects("\r\n");
this.WriteObjects("        #endregion // ",  this.GetType() , "\r\n");
#line 32 "P:\Kistl\Kistl.Generator\Templates\ObjectClasses\OnPropertyChange.cst"
} 

        }

    }
}