using System;
using System.IO;
using System.Collections.Specialized;

namespace Arebis.CodeGeneration
{
    public abstract class CodeTemplate : ContextBoundObject
    {
        private IGenerationHost _host;

		/// <summary>
		/// Creates a new CodeTemplate instance.
		/// </summary>
		public CodeTemplate(IGenerationHost _host)
		{ 
			// Store host
			this._host = _host;

			// Initialize:
			this.Initialize();
		}

		/// <summary>
		/// Initializes the template instance.
		/// This method is available to be overriden.
		/// </summary>
		public virtual void Initialize()
		{ }

		/// <summary>
		/// Transforms any value into a string to be rendered.
		/// </summary>
		public virtual string Render(object value)
		{
			return Convert.ToString(value);
		}

		/// <summary>
		/// The generation method generating the output.
		/// </summary>
        public abstract void Generate();

		/// <summary>
		///  The generation host.
		/// </summary>
        public IGenerationHost Host
        {
            get { return this._host; }
        }

		/// <summary>
		/// The generation settings.
		/// </summary>
        public NameValueCollection Settings
        {
            get { return this.Host.Settings; }
        }

		/// <summary>
		/// Calls the given template with the given parameters.
		/// Template output is embedded in the output generated by this template.
		/// </summary>
		protected virtual void CallTemplate(string templatefile, params object[] parameters)
		{
			this.Host.CallTemplate(templatefile, parameters);
		}

		/// <summary>
		/// Calls the given template with the given parameters.
		/// Template output is embedded in the output generated by this template.
		/// </summary>
		protected virtual void CallTemplateToFile(string templatefile, string outputfile, params object[] parameters)
		{
			this.Host.CallTemplateToFile(templatefile, outputfile, parameters);
		}

        #region Write methods

		/// <summary>
		/// Writes a string to the output.
		/// This method can be overriden.
		/// </summary>
		public virtual void Write(string s)
		{
			this.Host.WriteOutput(s);
		}

		/// <summary>
		/// Writes a value to the output.
		/// Uses the Render() method to render the value as string.
		/// </summary>
		public void Write(object value)
		{
			this.Write(this.Render(value));
		}

		/// <summary>
		/// Writes a formatstring and its arguments to the output.
		/// </summary>
        public void Write(string fmt, params object[] args)
        {
            this.Write(String.Format(fmt, args));
        }

		/// <summary>
		/// Writes a formatstring and its arguments followed by a newline to the output.
		/// </summary>
		public void WriteLine(string fmt, params object[] args)
        {
            this.WriteLine(String.Format(fmt, args));
        }

		/// <summary>
		/// Writes a string followed by a newline to the output.
		/// </summary>
		public void WriteLine(string s)
		{
			this.Write(s);
			this.WriteLine();
		}

		/// <summary>
		/// Writes a value followed by a newline to the output.
		/// Uses the Render() method to render the value as string.
		/// </summary>
		public void WriteLine(object value)
		{
			this.Write(Render(value));
			this.WriteLine();
		}

		/// <summary>
		/// Writes a string followed by a newline to the output.
		/// </summary>
		public virtual void WriteLine()
		{
			this.Write(Host.NewLineString);
		}

		/// <summary>
		/// Writes a series of values to the output.
		/// Uses the Render() method to render the values as strings.
		/// </summary>
		public void WriteObjects(params object[] values)
		{ 
			foreach(object value in values)
			{
				this.Write(this.Render(value));
			}
		}

        #endregion Write methods
    }
}
