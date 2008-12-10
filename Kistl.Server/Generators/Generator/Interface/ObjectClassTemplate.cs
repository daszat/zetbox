using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;


namespace Kistl.Server.Generators.EntityFramework.Interface
{
    public class ObjectClassTemplate : Kistl.Server.Generators.Templates.Interface.ObjectClassTemplate
    {
        public ObjectClassTemplate(Arebis.CodeGeneration.IGenerationHost _host, ObjectClass objClass)
            : base(_host, objClass)
        {
            System.Diagnostics.Debugger.Launch();
        }

        protected override string GetBaseClass()
        {
            return base.GetBaseClass();
        }
    }
}
