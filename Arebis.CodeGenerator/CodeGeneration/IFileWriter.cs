using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Arebis.CodeGeneration;

namespace Arebis.CodeGeneration
{
	public interface IFileWriter
	{
		IGenerationHost Host { get; set; }
		IFileWriter NextWriter { get; set; }
		void WriteFile(string filename, string content);
	}
}
