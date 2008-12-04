using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.CodeGeneration
{
	/// <summary>
	/// An IFileWriter implementation that does not actually write the file,
	/// but outputs the file to the console. USefull for debugging.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	public class ConsoleFileWriter : BaseFileWriter
	{
		public override void WriteFile(string filename, string content)
		{
			Console.WriteLine("File: " + filename);
			Console.WriteLine(new String('-', filename.Length + 6));
			Console.WriteLine(content);
			Console.WriteLine();
		}
	}
}
