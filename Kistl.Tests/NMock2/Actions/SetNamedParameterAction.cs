using System;
using System.IO;
using System.Reflection;
using NMock2.Monitoring;

namespace NMock2.Actions
{
	public class SetNamedParameterAction : IAction
	{
		private readonly string name;
		private readonly object value;

		public SetNamedParameterAction(string name, object value)
		{
			this.name = name;
			this.value = value;
		}
		
		public void Invoke(Invocation invocation)
		{
			ParameterInfo[] paramsInfo = invocation.Method.GetParameters();
			
			for (int i = 0; i < paramsInfo.Length; i++)
			{
				if (paramsInfo[i].Name == name)
				{
					invocation.Parameters[i] = value;
					return;
				}
			}
			
			throw new ArgumentException("no such parameter", name);
		}
		
		public void DescribeTo(TextWriter writer)
		{
			writer.Write("set ");
			writer.Write(name);
			writer.Write("=");
			writer.Write(value);
		}
	}
}
