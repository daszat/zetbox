using System;
using System.Collections.Generic;
using System.Text;
using Arebis.CodeGeneration;

namespace Arebis.CodeGeneration
{
	[System.Diagnostics.DebuggerStepThrough]
	public abstract class BaseFileWriter : IFileWriter
	{
		private IGenerationHost host;
		private IFileWriter nextWriter;

		public virtual IGenerationHost Host
		{
			get { return this.host; }
			set { this.host = value; }
		}

		public virtual IFileWriter NextWriter
		{
			get { return this.nextWriter; }
			set { this.nextWriter = value; }
		}

		public virtual void WriteFile(string filename, string content)
		{
			if (this.NextWriter != null) this.NextWriter.WriteFile(filename, content);
		}
	}
}
