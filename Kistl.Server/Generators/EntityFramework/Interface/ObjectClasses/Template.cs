using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.EntityFramework.Interface.ObjectClasses
{
    public class Template : Kistl.Server.Generators.Templates.Interface.ObjectClasses.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.ObjectClass objClass)
            : base(_host, objClass)
        {
        }
    }
}
