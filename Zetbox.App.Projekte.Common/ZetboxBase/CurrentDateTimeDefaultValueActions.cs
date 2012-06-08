namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class CurrentDateTimeDefaultValueActions
    {
        [Invocation]
        public static void GetDefaultValue(CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<System.Object> e)
        {
            var dtProp = (DateTimeProperty)obj.Property;
            switch (dtProp.DateTimeStyle)
            {
                case DateTimeStyles.Date:
                    e.Result = DateTime.Today;
                    break;
                case DateTimeStyles.Time:
                    e.Result = DateTime.Now; // TODO: what to do here?
                    break;
                case DateTimeStyles.DateTime:
                default:
                    e.Result = DateTime.Now;
                    break;
            }
        }

        [Invocation]
        public static void ToString(Kistl.App.Base.CurrentDateTimeDefaultValue obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Property != null)
            {
                var dtProp = (DateTimeProperty)obj.Property;
                switch (dtProp.DateTimeStyle)
                {
                    case DateTimeStyles.Date:
                        e.Result = string.Format("{0} will be initialized with the current date", obj.Property.Name);
                        break;
                    case DateTimeStyles.Time:
                        e.Result = string.Format("{0} will be initialized with the current date and time", obj.Property.Name);  // TODO: what to do here?
                        break;
                    case DateTimeStyles.DateTime:
                    default:
                        e.Result = string.Format("{0} will be initialized with the current date and time", obj.Property.Name);
                        break;
                }
            }
            else
            {
                e.Result = "Initializes a property with the current date and time";
            }
        }
    }
}
