// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Zetbox.API;


namespace Zetbox.Generator.Templates.Properties
{
    [Arebis.CodeGeneration.TemplateInfo(@"P:\Zetbox\Zetbox.Generator\Templates\Properties\PropertyListChangedEvent.cst")]
    public partial class PropertyListChangedEvent : Zetbox.Generator.MemberTemplate
    {
		protected IZetboxContext ctx;
		protected string eventName;
		protected string objType;


        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string objType)
        {
            if (_host == null) { throw new global::System.ArgumentNullException("_host"); }

            _host.CallTemplate("Properties.PropertyListChangedEvent", ctx, eventName, objType);
        }

        public PropertyListChangedEvent(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, string eventName, string objType)
            : base(_host)
        {
			this.ctx = ctx;
			this.eventName = eventName;
			this.objType = objType;

        }

        public override void Generate()
        {
#line 10 "P:\Zetbox\Zetbox.Generator\Templates\Properties\PropertyListChangedEvent.cst"
this.WriteObjects("",  GetModifiers() , " event PropertyListChangedHandler<",  objType , "> ",  eventName , ";\r\n");

        }

    }
}