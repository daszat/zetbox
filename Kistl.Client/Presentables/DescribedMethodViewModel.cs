
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public class DescribedMethodViewModel
        : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate DescribedMethodViewModel Factory(IKistlContext dataCtx, Method meth);
#else
        public new delegate DescribedMethodViewModel Factory(IKistlContext dataCtx, Method meth);
#endif

        public DescribedMethodViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            Method meth)
            : base(appCtx, config, dataCtx, meth)
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
