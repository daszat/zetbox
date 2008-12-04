using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Reflection
{
	/// <summary>
	/// ClassName is a class used to parse class names.
	/// </summary>
	public class ClassName
	{
		private string assemblyName;
		private string nameSpace;
		private string className;

		/// <summary>
		/// Instantiates a ClassName object based on a given name (full classname) eventually
		/// separated by a comma from an assemblyname.
		/// </summary>
		public ClassName(string classNameOrRef)
		{
			int sep = classNameOrRef.LastIndexOf('.');
			if (sep == -1)
			{
				this.className = classNameOrRef;
				this.nameSpace = String.Empty;
			}
			else
			{
				this.className = classNameOrRef.Substring(sep + 1);
				this.nameSpace = classNameOrRef.Substring(0, sep);
			}
		}

		/// <summary>
		/// Instantiates a ClassName object for the given type.
		/// </summary>
		/// <param name="type"></param>
		public ClassName(Type type)
			: this(type.FullName)
		{
			this.assemblyName = type.Assembly.FullName.Split(',')[0];
		}

		/// <summary>
		/// The assembly name of the class.
		/// </summary>
		public string AssemblyName
		{
			get { return this.assemblyName; }
			set { this.assemblyName = value; }
		}

		/// <summary>
		/// The namespace of the class.
		/// </summary>
		public string NameSpace
		{
			get { return this.nameSpace; }
			set { this.nameSpace = value; }
		}

		/// <summary>
		/// The name (without namespace) of the class.
		/// </summary>
		public string Name
		{
			get { return this.className; }
			set { this.className = value; }
		}

		/// <summary>
		/// The full name (namespace.classname) of the class.
		/// </summary>
		public string FullName
		{
			get { return this.NameSpace + "." + this.Name; }
		}

		/// <summary>
		/// A version neutral assembly referenced name of the class
		/// formatted as "namespace.classname, assemblyname".
		/// </summary>
		public string ReferenceName
		{
			get { return this.FullName + ", " + this.AssemblyName; }
		}
	}
}
