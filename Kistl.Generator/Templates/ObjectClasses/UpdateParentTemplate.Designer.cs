using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Generator;
using Kistl.Generator.Extensions;


namespace Kistl.Generator.Templates.ObjectClasses
{
    [Arebis.CodeGeneration.TemplateInfo(@"/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst")]
    public partial class UpdateParentTemplate : Kistl.Generator.ResourceTemplate
    {
		protected IKistlContext ctx;
		protected List<ObjectReferenceProperty> props;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, List<ObjectReferenceProperty> props)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("ObjectClasses.UpdateParentTemplate", ctx, props);
        }

        public UpdateParentTemplate(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, List<ObjectReferenceProperty> props)
            : base(_host)
        {
			this.ctx = ctx;
			this.props = props;

        }

        public override void Generate()
        {
#line 17 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
this.WriteObjects("\r\n");
#line 19 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
if (props.Count > 0)
{

#line 22 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
this.WriteObjects("		public override void UpdateParent(string propertyName, int? id)\r\n");
this.WriteObjects("		{\r\n");
this.WriteObjects("			int? __oldValue, __newValue = id;\r\n");
this.WriteObjects("			\r\n");
this.WriteObjects("			switch(propertyName)\r\n");
this.WriteObjects("			{\r\n");
#line 29 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
foreach(var prop in props)
	{
		ApplyCase(prop);
	}

#line 34 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
this.WriteObjects("				default:\r\n");
this.WriteObjects("					base.UpdateParent(propertyName, id);\r\n");
this.WriteObjects("					break;\r\n");
this.WriteObjects("			}\r\n");
this.WriteObjects("		}\r\n");
#line 40 "/srv/CCNet/Projects/zbox/repo/Kistl.Generator/Templates/ObjectClasses/UpdateParentTemplate.cst"
}


        }

    }
}