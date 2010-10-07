
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.Base;

    public class DescribedPropertyViewModel 
        : DataObjectViewModel
    {
        public new delegate DescribedPropertyViewModel Factory(IKistlContext dataCtx, Property prop);

        public DescribedPropertyViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            Property prop)
            : base(appCtx, config, dataCtx, prop)
        {
            _prop = prop;
        }
        private Property _prop;

        public Property DescribedProperty { get { return _prop; } }

        public override string Name
        {
            get
            {
                return _prop.Name;
            }
        }

        public string TypeString
        {
            get
            {
                return _prop.GetPropertyTypeString();
            }
        }

        public string ShortTypeString
        {
            get
            {
                if (_prop is BoolProperty)
                {
                    return "bool";
                }
                else if (_prop is IntProperty)
                {
                    return "int";
                }
                else if (_prop is DecimalProperty)
                {
                    return "decimal";
                }
                else if (_prop is DoubleProperty)
                {
                    return "double";
                }
                else if (_prop is StringProperty)
                {
                    return "string";
                }
                else if (_prop is GuidProperty)
                {
                    return "Guid";
                }
                else if (_prop is DateTimeProperty)
                {
                    return "DateTime";
                }
                else
                {
                    return TypeString;
                }
            }
        }

        public bool IsList
        {
            get
            {
                if (_prop is ValueTypeProperty)
                {
                    return ((ValueTypeProperty)_prop).IsList;
                }
                else if (_prop is ObjectReferenceProperty)
                {
                    var orp = (ObjectReferenceProperty)_prop;
                    return orp.GetIsList();
                }
                return false;
            }
        }
    }
}
