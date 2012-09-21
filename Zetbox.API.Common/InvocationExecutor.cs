
namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.API.Utils;
    using System.Reflection;
    using Autofac;

    public interface IInvocationExecutor
    {
        void CallInvocation(IInvocation obj, Type prototype, params object[] parameter);
        TResult CallInvocation<TResult>(IInvocation obj, Type prototype, params object[] parameter);
        bool CanCallInvocation(IInvocation obj, Type prototype);
        bool HasValidInvocation(IInvocation obj);
    }

    public class InvocationExecutor : Zetbox.API.Common.IInvocationExecutor
    {
        private IDeploymentRestrictor _restrictor;
        private ILifetimeScope _scope;
        public InvocationExecutor(IDeploymentRestrictor restrictor, ILifetimeScope scope)
        {
            if (restrictor == null) throw new ArgumentNullException("restrictor");
            if (scope == null) throw new ArgumentNullException("scope");
            _restrictor = restrictor;
            _scope = scope;
        }

        /// <summary>
        /// Checks if a IInvocation is defined. Use this function to check if a Invocation can be called.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool HasValidInvocation(IInvocation obj)
        {
            return obj != null && 
                   !string.IsNullOrEmpty(obj.MemberName) && 
                   obj.Implementor != null && 
                   _restrictor.IsAcceptableDeploymentRestriction((int)obj.Implementor.Assembly.DeploymentRestrictions);
        }

        /// <summary>
        /// Checks if a IInvocation is valid. Use this method only if you have a proper error handling code.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="prototype"></param>
        /// <returns></returns>
        public bool CanCallInvocation(IInvocation obj, Type prototype)
        {
            if (!HasValidInvocation(obj)) return false;
            if (obj == null) throw new ArgumentNullException("obj");
            if (prototype == null) throw new ArgumentNullException("prototype");

            var delegateInfo = prototype.GetMethod("Invoke");
            if (delegateInfo == null) throw new ArgumentOutOfRangeException("prototype", "Does not look like a delegate type");

            var implementor = obj.Implementor.AsType(false);
            if (implementor == null)
            {
                return false;
            }
            var methodInfo = implementor.FindMethod(obj.MemberName, delegateInfo.GetParameters().Select(p => p.ParameterType).ToArray());
            return methodInfo != null;
        }

        /// <summary>
        /// Calls an IInvocation. Throws an InvalidOperationException if the invocation is empty or is invalid.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="prototype">A delegate type used as a prototye to look up the correct method</param>
        /// <param name="parameter"></param>
        /// <returns>the result of the invocation</returns>
        public TResult CallInvocation<TResult>(IInvocation obj, Type prototype, params object[] parameter)
        {
            if(obj == null) throw new ArgumentNullException("obj");
            if (prototype == null) throw new ArgumentNullException("prototype");
            if (!HasValidInvocation(obj)) throw new InvalidOperationException("Object has no valid invocation");

            var delegateInfo = prototype.GetMethod("Invoke");
            if (delegateInfo == null) throw new ArgumentOutOfRangeException("prototype", "Does not look like a delegate type");

            var implementor = obj.Implementor.AsType(false);
            if (implementor == null)
            {
                throw new InvalidOperationException(string.Format("Implementor [{0}] not found", obj.Implementor));
            }
            var methodInfo = implementor.FindMethod(obj.MemberName, delegateInfo.GetParameters().Select(p => p.ParameterType).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException(string.Format("Method [{0}](object,object) not found in [{1}]", obj.MemberName, obj.Implementor));
            }

            if (methodInfo.IsStatic)
            {
                return (TResult)methodInfo.Invoke(null, parameter);
            }
            else
            {
                var implObj = _scope.Resolve(implementor);
                return (TResult)methodInfo.Invoke(implObj, parameter);
            }
        }

        /// <summary>
        /// Calls an IInvocation. Throws an InvalidOperationException if the invocation is empty or is invalid.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="prototype">A delegate type used as a prototye to look up the correct method</param>
        /// <param name="parameter"></param>
        public void CallInvocation(IInvocation obj, Type prototype, params object[] parameter)
        {
            CallInvocation<object>(obj, prototype, parameter);
        }
    }
}
