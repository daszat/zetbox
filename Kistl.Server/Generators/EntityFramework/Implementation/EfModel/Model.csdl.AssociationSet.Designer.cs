using System;
using System.Collections.Generic;
using System.Linq;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Implementation.EfModel
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst")]
    public partial class ModelCsdlAssociationSet : Kistl.Server.Generators.KistlCodeTemplate
    {
		protected IKistlContext ctx;
		protected AssociationInfo info;


        public ModelCsdlAssociationSet(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, AssociationInfo info)
            : base(_host)
        {
			this.ctx = ctx;
			this.info = info;

        }
        
        public override void Generate()
        {
#line 16 "P:\Kistl\Kistl.Server\Generators\EntityFramework\Implementation\EfModel\Model.csdl.AssociationSet.cst"
this.WriteObjects("    <!-- ",  info.GetType().Name , " -->\r\n");
this.WriteObjects("    <AssociationSet Name=\"",  info.AssociationName , "\" Association=\"Model.",  info.AssociationName , "\" >\r\n");
this.WriteObjects("      <End Role=\"",  info.ASide.RoleName , "\" EntitySet=\"",  info.ASide.StorageEntitySet , "\" />\r\n");
this.WriteObjects("      <End Role=\"",  info.BSide.RoleName , "\" EntitySet=\"",  info.BSide.StorageEntitySet , "\" />\r\n");
this.WriteObjects("    </AssociationSet>\r\n");

        }



    }
}