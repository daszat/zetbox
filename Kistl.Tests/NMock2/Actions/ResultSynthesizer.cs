using System;
using System.Collections;
using System.IO;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class ResultSynthesizer : IAction
	{
		private static Hashtable DEFAULT_RESULTS = new Hashtable();
		static ResultSynthesizer()
		{
			DEFAULT_RESULTS[typeof (string)] = new ReturnAction("");
			DEFAULT_RESULTS[typeof (ArrayList)] = new ReturnCloneAction(new ArrayList());
			DEFAULT_RESULTS[typeof (SortedList)] = new ReturnCloneAction(new SortedList());
			DEFAULT_RESULTS[typeof (Hashtable)] = new ReturnCloneAction(new Hashtable());
			DEFAULT_RESULTS[typeof (Queue)] = new ReturnCloneAction(new Queue());
			DEFAULT_RESULTS[typeof (Stack)] = new ReturnCloneAction(new Stack());
		}
		
		private Hashtable results = new Hashtable();
		
		public void Invoke(Invocation invocation)
		{
			Type returnType = invocation.Method.ReturnType;
			
			if (returnType == typeof(void)) return; // sanity check
			
			if (results.ContainsKey(returnType))
			{
				IAction action = GetAction(returnType, results);
				action.Invoke(invocation);
			}
			else if (returnType.IsArray)
			{
				invocation.Result = NewEmptyArray(returnType);
			}
			else if (returnType.IsValueType)
			{
				invocation.Result = Activator.CreateInstance(returnType);
			}
			else if(DEFAULT_RESULTS.ContainsKey(returnType))
			{
				IAction action = GetAction(returnType, DEFAULT_RESULTS);
				action.Invoke(invocation);
			}
			else
			{
				throw new InvalidOperationException("No action registered for return type "+returnType);
			}
		}
		
		private IAction GetAction(Type returnType, Hashtable results)
		{
			return ((IAction)results[returnType]);
		}
		
		public void SetResult(Type type, object result)
		{
			SetAction(type, Return.Value(result));
		}
		
		private object SetAction(Type type, IAction action)
		{
			return results[type] = action;
		}

		public void DescribeTo(TextWriter writer)
		{
			writer.Write("a synthesized result");
		}
		
		private static object NewEmptyArray(Type arrayType)
		{
			int rank = arrayType.GetArrayRank();
			int[] dimensions = new int[rank];
			
			return Array.CreateInstance(arrayType.GetElementType(), dimensions);
		}
	}
}
