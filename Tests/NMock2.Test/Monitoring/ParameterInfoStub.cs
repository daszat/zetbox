using System.Reflection;

namespace NMock2.Test.Monitoring
{
	public class ParameterInfoStub : ParameterInfo
	{
		public ParameterInfoStub( string name, ParameterAttributes attributes )
		{
			this.NameImpl = name;
			this.AttrsImpl = attributes;
		}
	}
}
