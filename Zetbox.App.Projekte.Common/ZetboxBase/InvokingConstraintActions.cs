namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class InvokingConstraintActions
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Common.CustomActions");

        private static readonly Type[] ObjectObject = new Type[] { typeof(object), typeof(object) };
        
        [Invocation]
        public static void IsValid(InvokingConstraint obj, MethodReturnEventArgs<bool> e, object constrainedObject, object constrainedValue)
        {
            var implementor = obj.IsValidInvocation.Implementor.AsType(false);
            if (implementor == null)
            {
                Log.ErrorFormat("Implementor [{0}] not found", obj.IsValidInvocation.Implementor);
                return;
            }
            var methodInfo = implementor.FindMethod(obj.IsValidInvocation.MemberName, ObjectObject);
            if (methodInfo == null)
            {
                Log.ErrorFormat("Method [{0}](object,object) not found in [{1}]", obj.IsValidInvocation.MemberName, obj.IsValidInvocation.Implementor);
                return;
            }
            e.Result = (bool)methodInfo.Invoke(null, new object[] { constrainedObject, constrainedValue });
        }

        [Invocation]
        public static void GetErrorText(InvokingConstraint obj, MethodReturnEventArgs<string> e, object constrainedObject, object constrainedValue)
        {
            var implementor = obj.GetErrorTextInvocation.Implementor.AsType(false);
            if (implementor == null)
            {
                Log.ErrorFormat("Implementor [{0}] not found", obj.GetErrorTextInvocation.Implementor);
                return;
            }
            var methodInfo = implementor.FindMethod(obj.GetErrorTextInvocation.MemberName, ObjectObject);
            if (methodInfo == null)
            {
                Log.ErrorFormat("Method [{0}](object,object) not found in [{1}]", obj.GetErrorTextInvocation.MemberName, obj.IsValidInvocation.Implementor);
                return;
            }
            e.Result = (string)methodInfo.Invoke(null, new object[] { constrainedObject, constrainedValue });
        }
    }
}
