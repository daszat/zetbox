
namespace Zetbox.API.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum Layout
    {
        /// <summary>
        /// Layout contents in a grid
        /// </summary>
        Grid
    }

    [Flags]
    public enum Formatting
    {
        None = 0,
        Bold = 1,
        Italic = 2,
    }

    /// <summary>
    /// Display the annotated list as Groups.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GuiGridAttribute : Attribute
    {
        public GuiGridAttribute()
        {
        }
    }

    /// <summary>
    /// Display the annotated list as Tabs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GuiTabbedAttribute : Attribute
    {
        public GuiTabbedAttribute()
        {
        }
    }

    /// <summary>
    /// Use the annotated property as Description for this object
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiDescriptionAttribute : Attribute
    {
        public GuiDescriptionAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies that the decorated property should be formatted as percentage value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class GuiFormatAsPercentAttribute : Attribute
    {
        public GuiFormatAsPercentAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies that the decorated property should be formatted as table of values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class GuiTableAttribute : Attribute
    {
        public GuiTableAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies that the decorated entity should not have a separate view model.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class GuiSkipViewModelAttribute : Attribute
    {
        public GuiSkipViewModelAttribute()
        {
        }
    }

    /// <summary>
    /// Specify a property to be used as title for the containing class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiClassTitleAttribute : Attribute
    {
        /// <summary>
        /// Use this Property for specifying the title of this DTO.
        /// </summary>
        public GuiClassTitleAttribute()
        {
        }
    }

    /// <summary>
    /// Specify a title or a property to be used as title for the decorated entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GuiTitleAttribute : Attribute
    {
        private readonly string _title;

        /// <summary>
        /// Use this Property for specifying the title of this DTO.
        /// </summary>
        public GuiTitleAttribute()
            : this(null)
        {
        }

        /// <summary>
        /// Display this DTO with the specified title.
        /// </summary>
        public GuiTitleAttribute(string title)
        {
            this._title = title;
        }

        public string Title
        {
            get { return _title; }
        }
    }

    /// <summary>
    /// Specify a format string to be used for this value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiFormatStringAttribute : Attribute
    {
        private readonly string _formatString;

        public GuiFormatStringAttribute(string formatString)
        {
            this._formatString = formatString;
        }

        public string FormatString
        {
            get { return _formatString; }
        }
    }

    /// <summary>
    /// Specify whether or not the class can be the root of a print-out.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiPrintableRootAttribute : Attribute
    {
        private readonly bool _isPrintableRoot;

        /// <summary>
        /// This class can be used as printable root.
        /// </summary>
        public GuiPrintableRootAttribute()
            : this(true)
        {
        }

        /// <summary>
        /// Explicitely specify whether this class can be used as printable root.
        /// </summary>
        public GuiPrintableRootAttribute(bool isPrintableRoot)
        {
            this._isPrintableRoot = isPrintableRoot;
        }

        public bool IsPrintableRoot
        {
            get { return _isPrintableRoot; }
        }
    }

    /// <summary>
    /// The decorated property specifies whether or not the class can be the root of a print-out.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiClassPrintableRootAttribute : Attribute
    {
        /// <summary>
        /// Use this Property for specifying whether this class can be used as printable root.
        /// </summary>
        public GuiClassPrintableRootAttribute()
        {
        }
    }

    /// <summary>
    /// Specify a method as print post processor. This is called everytime a report is rendered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class GuiPrintPostProcessorAttribute : Attribute
    {
        public GuiPrintPostProcessorAttribute()
        {
        }
    }

    /// <summary>
    /// Specifies where in a Grid a DTO should be displayed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    // TODO: make into a "value"-type with custom Equals, GetHashCode, ==, ...
    public sealed class GuiGridLocationAttribute : Attribute
    {
        private readonly int _row;
        private readonly int _column;

        private readonly int _rowSpan;
        private readonly int _columnSpan;

        public GuiGridLocationAttribute(int row, int column)
            : this(row, column, 1, 1)
        {
        }

        public GuiGridLocationAttribute(int row, int column, int rowSpan, int columnSpan)
        {
            if (row < 0) throw new ArgumentOutOfRangeException("row", "row must be >= 0");
            if (column < 0) throw new ArgumentOutOfRangeException("column", "column must be >= 0");
            if (rowSpan < 0) throw new ArgumentOutOfRangeException("rowSpan", "rowSpan must be >= 1");
            if (columnSpan < 0) throw new ArgumentOutOfRangeException("columnSpan", "columnSpan must be >= 1");

            this._row = row;
            this._column = column;
            this._rowSpan = rowSpan;
            this._columnSpan = columnSpan;
        }

        public int Row { get { return _row; } }
        public int Column { get { return _column; } }

        public int RowSpan { get { return _rowSpan; } }
        public int ColumnSpan { get { return _columnSpan; } }

        public override bool Equals(object obj)
        {
            if (!(obj is GuiGridLocationAttribute)) return false;
            var other = (GuiGridLocationAttribute)obj;
            return this.Row == other.Row && this.Column == other.Column && this.RowSpan == other.RowSpan && this.ColumnSpan == other.ColumnSpan;
        }

        public override int GetHashCode()
        {
            return Row * 3571 + Column + RowSpan * 181 + ColumnSpan * 29;
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class GuiFormattingAttribute : Attribute
    {
        private readonly Formatting _formatting;

        public GuiFormattingAttribute(Formatting formatting)
        {
            this._formatting = formatting;
        }

        public Formatting Formatting
        {
            get { return _formatting; }
        }
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class GuiClassFormattingAttribute : Attribute
    {
        public GuiClassFormattingAttribute()
        {
        }
    }

    /// <summary>
    /// Use this color as background for the decorated entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class GuiBackgroundAttribute : Attribute
    {
        private readonly string _color;
        private readonly string _alternateColor;

        public GuiBackgroundAttribute(string color)
            : this(color, color)
        {
        }

        public GuiBackgroundAttribute(string color, string alternateColor)
        {
            this._color = color;
            this._alternateColor = alternateColor;
        }

        public string Color
        {
            get { return _color; }
        }

        /// <summary>
        /// The color to be used for accents and alternations
        /// </summary>
        public string AlternateColor
        {
            get { return _alternateColor; }
        }
    }

    /// <summary>
    /// Specify a property as background for the containing DTO.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiClassBackgroundAttribute : Attribute
    {
        public GuiClassBackgroundAttribute()
        {
        }
    }
    /// <summary>
    /// Specify a property as alternatebackground for the containing DTO.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class GuiClassAlternateBackgroundAttribute : Attribute
    {
        public GuiClassAlternateBackgroundAttribute()
        {
        }
    }
}
