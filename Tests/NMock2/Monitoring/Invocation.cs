using System;
using System.IO;
using System.Reflection;

namespace NMock2.Monitoring
{
	public class Invocation : ISelfDescribing
	{
		public readonly object Receiver;
		public readonly MethodInfo Method;
		public readonly ParameterList Parameters;
		
		private object result = null;
		private Exception exception = null;
		private bool isThrowing = false;
		
		public Invocation(object receiver, MethodInfo method, object[] parameters)
		{
			Receiver = receiver;
			Method = method;
			Parameters = new ParameterList(method, parameters);
		}
		
		public object Result
		{
			get
			{
				return result;
			}

			set
			{
				CheckReturnType(value);

				result = value;
				exception = null;
				isThrowing = false;
			}
		}
		
		private void CheckReturnType(object value)
		{
			if (Method.ReturnType == typeof(void) && value != null)
			{
				throw new ArgumentException("cannot return a value from a void method", "Result");
			}
			
			if (Method.ReturnType != typeof(void) && Method.ReturnType.IsValueType && value == null)
			{
 #if NET20
 				if ( !( Method.ReturnType.IsGenericType && Method.ReturnType.GetGenericTypeDefinition() == typeof( Nullable<> ) ) )
 				{
 					throw new ArgumentException( "cannot return a null value type", "Result" );
 				}
#else
				throw new ArgumentException("cannot return a null value type", "Result" );
#endif
			}
			
			if (value != null && !Method.ReturnType.IsInstanceOfType(value))
			{
				throw new ArgumentException("cannot return a value of type " + value.GetType()
											+ " from a method returning " + Method.ReturnType,
											"Result");
			}
		}
		
		public Exception Exception
		{
			get
			{
				return exception;
			}
			set
			{
				if (value == null) throw new ArgumentNullException("Exception");

				exception = value;
				result = null;
				isThrowing = true;
			}
		}
		
		public bool IsThrowing
		{
			get { return isThrowing; }
		}
	
		public void InvokeOn(object otherReceiver)
		{
			try
			{
				Result = Method.Invoke(otherReceiver, Parameters.AsArray);
				Parameters.MarkAllValuesAsSet();
			}
			catch(TargetInvocationException e)
			{
				Exception = e.InnerException;
			}
		}
		
		public void DescribeTo(TextWriter writer)
		{
			writer.Write(Receiver.ToString());
			
			if (MethodIsIndexerGetter())
			{
				DescribeAsIndexerGetter(writer);
			}
			else if (MethodIsIndexerSetter())
			{
				DescribeAsIndexerSetter(writer);
			}
			else if (MethodIsEventAdder())
			{
				DescribeAsEventAdder(writer);
			}
			else if (MethodIsEventRemover())
			{
				DescribeAsEventRemover(writer);
			}
			else if (MethodIsProperty())
			{
				DescribeAsProperty(writer);
			}
			else
			{
				DescribeNormalMethod(writer);
			}
        }
        
        private bool MethodIsProperty()
        {
            return Method.IsSpecialName &&
				   ((Method.Name.StartsWith("get_") && Parameters.Count == 0) ||
                    (Method.Name.StartsWith("set_") && Parameters.Count == 1));
        }
		
		private	bool MethodIsIndexerGetter()
		{
			return Method.IsSpecialName
				&& Method.Name == "get_Item"
				&& Parameters.Count >= 1;
		}

		private	bool MethodIsIndexerSetter()
		{
			return Method.IsSpecialName
				&& Method.Name == "set_Item"
				&& Parameters.Count >= 2;
		}
		
		private bool MethodIsEventAdder()
		{
			return Method.IsSpecialName
				&& Method.Name.StartsWith("add_")
				&& Parameters.Count == 1
				&& typeof(Delegate).IsAssignableFrom(Method.GetParameters()[0].ParameterType);
		}
		
		private bool MethodIsEventRemover()
		{
			return Method.IsSpecialName
				&& Method.Name.StartsWith("remove_")
				&& Parameters.Count == 1
				&& typeof(Delegate).IsAssignableFrom(Method.GetParameters()[0].ParameterType);
		}

		private void DescribeAsProperty(TextWriter writer)
        {
			writer.Write(".");
			writer.Write(Method.Name.Substring(4));
            if (Parameters.Count > 0)
            {
                writer.Write(" = ");
                writer.Write(Parameters[0]);
            }
        }

		private void DescribeAsIndexerGetter(TextWriter writer)
		{
			writer.Write("[");
			WriteParameterList(writer, Parameters.Count);
			writer.Write("]");
		}
		
		private void DescribeAsIndexerSetter(TextWriter writer)
		{
			writer.Write("[");
			WriteParameterList(writer, Parameters.Count-1);
			writer.Write("] = ");
			writer.Write(Parameters[Parameters.Count-1]);
		}
		
		private void DescribeNormalMethod(TextWriter writer)
        {
			writer.Write(".");
			writer.Write(Method.Name);
			writer.Write("(");

        	WriteParameterList(writer, Parameters.Count);

        	writer.Write(")");
		}
		
		private void WriteParameterList(TextWriter writer, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (i > 0) writer.Write(", ");

				if (Method.GetParameters()[i].IsOut)
				{
					writer.Write("out");
				}
				else
				{
					writer.Write(Parameters[i]);
				}
			}
		}
		
		private void DescribeAsEventAdder(TextWriter writer)
		{
			writer.Write(" += ");
			writer.Write(Parameters[0]);
		}

		private void DescribeAsEventRemover(TextWriter writer)
		{
			writer.Write(" -= ");
			writer.Write(Parameters[0]);		}
	}
}
