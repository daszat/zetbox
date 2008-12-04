using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.CodeGeneration
{
	/// <summary>
	/// Decorates a template class with template information.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class TemplateInfoAttribute : Attribute
	{
		private string fileName;

		/// <summary>
		/// Decorates a template class with template information.
		/// </summary>
		public TemplateInfoAttribute(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>
		/// The full path of the original template file.
		/// </summary>
		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}
	}
}
