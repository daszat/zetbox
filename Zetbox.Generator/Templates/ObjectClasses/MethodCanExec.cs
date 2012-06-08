
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using Kistl.Generator;
    using Kistl.Generator.Extensions;

    public partial class MethodCanExec
    {
        protected MethodCanExec(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
            throw new NotSupportedException("this constructor only exists to allow overrinding in a CST");
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            // Methods are usually virtual ...
            var result = base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;

            // ... and override on derived types
            if (this.m.ObjectClass != this.dt)
                result = result | MemberAttributes.Override;

            return result;
        }
    }
}
