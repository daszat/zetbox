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

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.API.Common;

    [Implementor]
    public class MethodActions
    {
        private static IAssetsManager _assets;
        public MethodActions(IAssetsManager assets)
        {
            _assets = assets;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetLabel(Zetbox.App.Base.Method obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;

            if (obj.Module == null || obj.ObjectClass == null)
                return System.Threading.Tasks.Task.CompletedTask;

            e.Result = _assets.GetString(obj.Module, ZetboxAssetKeys.ConstructBaseName(obj), ZetboxAssetKeys.ConstructLabelKey(obj), e.Result);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task ToString(Method obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (obj.ObjectClass != null && obj.ObjectClass.Module != null)
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.Name + "." + obj.Name;

                ToStringHelper.FixupFloatingObjectsToString(obj, e);
            }
            else
            {
                e.Result = String.Format("new Method #{0}: {1}", obj.ID, obj.Name);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetReturnParameter(Method obj, MethodReturnEventArgs<BaseParameter> e)
        {
            e.Result = obj.Parameter.SingleOrDefault(param => param.IsReturnParameter);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_Name(Method obj, PropertyPostSetterEventArgs<string> e)
        {
            obj.Recalculate("CodeTemplate");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_Parameter(Method obj)
        {
            obj.Recalculate("CodeTemplate");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task get_CodeTemplate(Method obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            string objParameter;
            if (obj.ObjectClass != null)
            {
                objParameter = string.Format("{0} obj", obj.ObjectClass.Name);
            }
            else
            {
                objParameter = "<<TYPE>> obj";
            }

            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}({1}", obj.Name, objParameter);

            var returnParam = obj.GetReturnParameter();
            if (returnParam != null)
            {
                sb.AppendFormat(", MethodReturnEventArgs<{0}> e", returnParam.GetParameterTypeString());
            }

            foreach (var param in obj.Parameter.Where(p => !p.IsReturnParameter))
            {
                sb.AppendFormat(", {0} {1}",
                    param.GetParameterTypeString(),
                    param.Name);
            }

            sb.AppendLine(")\n{\n}");

            sb.AppendLine();
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}CanExec({1}, MethodReturnEventArgs<bool> e)\n{{\n}}\n", obj.Name, objParameter);

            sb.AppendLine();
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}CanExecReason({1}, MethodReturnEventArgs<string> e)\n{{\n}}\n", obj.Name, objParameter);

            e.Result = sb.ToString();

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetName(Method obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.ObjectClass as ObjectClass;
            if (cls != null)
            {
                e.Result = string.Format("{0}_Methods.{1}", cls.GetName(), obj.Name);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
