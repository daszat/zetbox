
namespace Kistl.Generator
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Generator.Extensions;

    public class MemberTemplate
        : ResourceTemplate
    {
        protected MemberTemplate(Arebis.CodeGeneration.IGenerationHost host)
            : base(host)
        {
        }

        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMemberAttributes(MemberAttributes.Public | MemberAttributes.Final);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MemberAttributes"/> for this method and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return memberAttributes;
        }
    }
}
