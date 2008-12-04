using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Arebis.CodeGenerator.Templated
{
	[Serializable]
	public class CompilationFailedException : ApplicationException
	{
		private string filename;
		private IList<CompilerError> errors;

		public CompilationFailedException(string filename, IList<CompilerError> errors)
			: base("Compilation failed.")
		{
			this.filename = filename;
			this.errors = errors;
		}

		protected CompilationFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.filename = info.GetString("filename");
			this.errors = (IList<CompilerError>)info.GetValue("errors", typeof(IList<CompilerError>));
		}

		public string Filename
		{
			get { return this.filename; } 
		}

		public IList<CompilerError> Errors
		{
			get { return errors; }
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("errors", this.errors);
		}
	}
}
