using System;
using System.Collections;
using System.Reflection;

namespace NMock2.Monitoring
{
	public class ParameterList
	{
		private MethodInfo method;
		private object[] values;
		private BitArray isValueSet;
		
		public ParameterList(MethodInfo method, object[] values)
		{
			this.method = method;
			this.values = values;
			isValueSet = new BitArray(values.Length);
			
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				isValueSet[i] = !parameters[i].IsOut;
			}
		}
		
		public int Count
		{
			get { return values.Length; }
		}
		
		public object this[int i]
		{
			get
			{
				if (IsValueSet(i))
				{
					return values[i];
				}
				else
				{
					throw new InvalidOperationException("parameter "+ParameterName(i)+" has not been set");
				}
			}
			set
			{
				if (CanValueBeSet(i))
				{
					values[i] = value;
					isValueSet[i] = true;
				}
				else
				{
					throw new InvalidOperationException("cannot set the value of in parameter "+ParameterName(i));
				}
			}
		}
		
		internal object[] AsArray
		{
			get { return values; }
		}

		internal void MarkAllValuesAsSet()
		{
			isValueSet.SetAll(true);
		}
		
		private bool CanValueBeSet(int i)
		{
			return !method.GetParameters()[i].IsIn;
		}
		
		public bool IsValueSet(int i)
		{
			return isValueSet[i];
		}
		
		private string ParameterName(int i)
		{
			return method.GetParameters()[i].Name;
		}
	}
}
