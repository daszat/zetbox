using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using Arebis.CodeGeneration;

namespace Arebis.CodeGenerator.Templated
{
	[System.Diagnostics.DebuggerStepThrough]
	public class DefaultFileWriter : BaseFileWriter
	{
		private bool overwriteReadonly = false;
		private string tabspaces = String.Empty;

		public override IGenerationHost Host
		{
			get
			{
				return base.Host;
			}
			set
			{
				base.Host = value;

				this.overwriteReadonly = Convert.ToBoolean(this.Host.Settings["overwritereadonly"]);
				this.tabspaces = new String(' ', Convert.ToInt32(this.Host.Settings["tabspaces"]));
			}
		}

		public override void WriteFile(string filename, string content)
		{
			// Replace tabs by spaces if 'tabspaces' setting > 0
			if (this.tabspaces.Length > 0) content = content.Replace("\t", this.tabspaces);

			// Retrieve fileinfo and make file writable:
			FileInfo fileinfo = new FileInfo(filename);
			if (this.overwriteReadonly)
			{
				if ( fileinfo.Exists && fileinfo.IsReadOnly) fileinfo.IsReadOnly = false;
			}

			// Write file:
			try
			{
				this.Host.Log("Writing file \"{0}\".", filename);
				fileinfo.Directory.Create();
				System.IO.File.WriteAllText(filename, content);
			}
			catch (UnauthorizedAccessException)
			{
				if (this.overwriteReadonly)
				{
					this.Host.Log("Warning: Failed to overwrite file '{0}'", filename);
				}
			}
		}
	}
}
