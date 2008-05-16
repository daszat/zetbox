using System.IO;

namespace NMock2
{
	public interface ISelfDescribing
	{
		void DescribeTo(TextWriter writer);
	}
}
