using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Arebis.CodeGeneration
{
	public interface IGenerationSetup
	{
		void Setup(NameValueCollection settings);
	}
}
