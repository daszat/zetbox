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

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    public class DescribedMethodViewModel
        : DataObjectViewModel
    {
        public new delegate DescribedMethodViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Method meth);

        public DescribedMethodViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Method meth)
            : base(appCtx, dataCtx, parent, meth)
        {
            _method = meth;
        }
        private Method _method;
        public Method DescribedMethod { get { return _method; } }

        public override string Name
        {
            get
            {
                return _method.Name;
            }
        }

        public bool IsDefaultMethod
        {
            get
            {
                return _method.IsDefaultMethod();
            }
        }

        public string ReturnTypeString
        {
            get
            {
                var p = _method.GetReturnParameter();
                if (p == null) return "void";
                return p.GetParameterTypeString();
            }
        }

        public string ShortReturnTypeString
        {
            get
            {
                var p = _method.GetReturnParameter();
                if (p == null) return "void";

                if (p is BoolParameter)
                {
                    return "bool";
                }
                else if (p is IntParameter)
                {
                    return "int";
                }
                else if (p is DoubleParameter)
                {
                    return "double";
                }
                else if (p is StringParameter)
                {
                    return "string";
                }
                /*else if (p is GuidParameter)
                {
                    return "Guid";
                }*/
                else if (p is DateTimeParameter)
                {
                    return "DateTime";
                }
                else
                {
                    return ReturnTypeString;
                }
            }
        }
    }
}
