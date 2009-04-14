
namespace Kistl.App.GUI
{

    /// <summary>
    /// 
    /// </summary>
    public enum VisualType
    {
		/// <summary>
		/// 
		/// </summary>
		SimpleObjectList = 16,

		/// <summary>
		/// display a value from an Enumeration
		/// </summary>
		Enumeration = 15,

		/// <summary>
		/// 
		/// </summary>
		StringList = 14,

		/// <summary>
		/// 
		/// </summary>
		String = 13,

		/// <summary>
		/// 
		/// </summary>
		IntegerList = 12,

		/// <summary>
		/// 
		/// </summary>
		Integer = 11,

		/// <summary>
		/// 
		/// </summary>
		DoubleList = 10,

		/// <summary>
		/// 
		/// </summary>
		Double = 9,

		/// <summary>
		/// a list of date/time values
		/// </summary>
		DateTimeList = 8,

		/// <summary>
		/// a date/time value
		/// </summary>
		DateTime = 7,

		/// <summary>
		/// a list of booleans
		/// </summary>
		BooleanList = 6,

		/// <summary>
		/// a boolean
		/// </summary>
		Boolean = 5,

		/// <summary>
		/// A reference to an object
		/// </summary>
		ObjectReference = 4,

		/// <summary>
		/// A list of objects
		/// </summary>
		ObjectList = 3,

		/// <summary>
		/// A group of properties
		/// </summary>
		PropertyGroup = 2,

		/// <summary>
		/// A full view of the object
		/// </summary>
		Object = 1,

		/// <summary>
		/// The renderer class is no actual "View", but neverthe less needs to be found
		/// </summary>
		Renderer = 0,

		/// <summary>
		/// 
		/// </summary>
		MenuGroup = 18,

		/// <summary>
		/// 
		/// </summary>
		MenuItem = 17,

		/// <summary>
		/// 
		/// </summary>
		TemplateEditor = 19,

		/// <summary>
		/// Displays an Integer with a slider instead of a text box
		/// </summary>
		IntegerSlider = 20,

		/// <summary>
		/// An object as entry of a list
		/// </summary>
		ObjectListEntry = 21,

		/// <summary>
		/// The debugger window for displaying the active contexts
		/// </summary>
		KistlDebugger = 22,

	}
}