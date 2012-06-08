// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Dtos;
    using Zetbox.API.Utils;
    using Zetbox.Client.Presentables;

    public class DtoBuilder
    {
        // looks, sounds and smells like fetch?
        public static DtoBaseViewModel BuildFrom(object root, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            return BuildFrom(root, null, root, dependencies, dataCtx, parent, fileOpener, tmpService);
        }

        private static DtoBaseViewModel BuildFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null) return null;

            // avoid using the prop's attributes or type if the dto is not assignable to the property, e.g. when working on a list's elements
            if (parentProp != null && parentProp.PropertyType.IsAssignableFrom(dto.GetType()))
            {
                var propertyType = dto.GetType();
                if (   typeof(byte).IsAssignableFrom(propertyType)
                    || typeof(int).IsAssignableFrom(propertyType)
                    || typeof(uint).IsAssignableFrom(propertyType)
                    || typeof(long).IsAssignableFrom(propertyType)
                    || typeof(ulong).IsAssignableFrom(propertyType)
                    || typeof(double).IsAssignableFrom(propertyType)
                    || typeof(float).IsAssignableFrom(propertyType)
                    || typeof(decimal).IsAssignableFrom(propertyType)
                    || typeof(DateTime).IsAssignableFrom(propertyType)

                    || typeof(byte?).IsAssignableFrom(propertyType)
                    || typeof(int?).IsAssignableFrom(propertyType)
                    || typeof(uint?).IsAssignableFrom(propertyType)
                    || typeof(long?).IsAssignableFrom(propertyType)
                    || typeof(ulong?).IsAssignableFrom(propertyType)
                    || typeof(double?).IsAssignableFrom(propertyType)
                    || typeof(float?).IsAssignableFrom(propertyType)
                    || typeof(decimal?).IsAssignableFrom(propertyType)
                    || typeof(DateTime?).IsAssignableFrom(propertyType)

                    || typeof(string).IsAssignableFrom(propertyType))
                {
                    return FormatValue(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                }
                else
                {
                    var propAttrs = parentProp.GetCustomAttributes(false);
                    foreach (var attr in propAttrs)
                    {
                        if (attr is GuiTabbedAttribute)
                        {
                            return BuildTabbedFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                        }
                        else if (attr is GuiGridAttribute)
                        {
                            return BuildGridFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                        }
                        else if (attr is GuiTableAttribute)
                        {
                            return BuildTableFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                        }
                    }
                }
            }

            var type = dto.GetType();
            var skipAttr = type.GetCustomAttributes(typeof(GuiSkipViewModelAttribute), false);
            if (skipAttr.Length > 0)
            {
                return BuildEmptyFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
            }

            var attrs = type.GetCustomAttributes(false);
            foreach (var attr in attrs)
            {
                if (attr is GuiTabbedAttribute)
                {
                    return BuildTabbedFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                }
                else if (attr is GuiGridAttribute)
                {
                    return BuildGridFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                }
                else if (attr is GuiTableAttribute)
                {
                    return BuildTableFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
                }
            }

            if (dto.GetType().HasGenericDefinition(typeof(XmlDictionary<,>)))
            {
                return BuildTabbedFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(dto.GetType()))
            {
                return BuildTableFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
            }
            else
            {
                return BuildGroupFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
            }
        }

        private static DtoBaseViewModel BuildEmptyFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null)
            {
                // cannot inspect runtime type on null reference
                return null;
            }
            var isPrintableRoot = ExtractIsPrintableRoot(parentProp, dto);

            var dataProps = new List<PropertyInfo>();
            var percentProps = new Dictionary<string, PropertyInfo>();
            ExtractProps(dto.GetType(), dataProps, percentProps);

            var items = new List<DtoBaseViewModel>();

            foreach (var prop in dataProps)
            {
                var value = dto.GetPropertyValue<object>(prop.Name);
                var viewModel = BuildFrom(root, prop, value, dependencies, dataCtx, parent, fileOpener, tmpService);
                if (viewModel == null) continue; // do not add without content

                var valueModel = viewModel as DtoValueViewModel;
                if (valueModel != null && percentProps.ContainsKey(prop.Name))
                {
                    valueModel.AlternateRepresentation = string.Format("{0:0.00} %", 100 * Convert.ToDouble(dto.GetPropertyValue<object>(percentProps[prop.Name].Name)));
                    valueModel.AlternateRepresentationAlignment = ContentAlignment.MiddleRight;
                }

                items.Add(viewModel);
            }

            var result = parent as DtoGroupedViewModel;

            if (result != null)
            {
                items.ForEach(result.Items.Add);
                result.IsPrintableRoot = isPrintableRoot;
                return result;
            }
            else if (items.Count == 1)
            {
                items[0].IsPrintableRoot = isPrintableRoot;
                return items[0];
            }
            else
            {
                throw new NotSupportedException(string.Format("Cannot GuiSkipViewModel on multi-property class [{0}], when having a non-grouped parent [{1}]", dto.GetType(), parent.GetType()));
            }
        }

        /// <summary>
        /// Creates a descriptive grouping of the specified object
        /// </summary>
        public static DtoGroupedViewModel BuildGroupFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null)
            {
                // cannot inspect runtime type on null reference
                return null;
            }
            var debugInfo = parentProp == null
                ? string.Format("topgroup:{0}", dto.GetType())
                : string.Format("group:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto.GetType());
            var result = new DtoGroupedViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
            {
                Title = ExtractTitle(parentProp, dto),
                Description = ExtractDescription(parentProp, dto),
                Background = ExtractBackground(parentProp, dto),
                Formatting = ExtractFormatting(parentProp, dto),
                IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                Root = root,
                Data = dto,
            };

            var dataProps = new List<PropertyInfo>();
            var percentProps = new Dictionary<string, PropertyInfo>();
            ExtractProps(dto.GetType(), dataProps, percentProps);

            foreach (var prop in dataProps)
            {
                var value = dto.GetPropertyValue<object>(prop.Name);
                var viewModel = BuildFrom(root, prop, value, dependencies, dataCtx, parent, fileOpener, tmpService);
                if (viewModel == null) continue; // do not add without content

                var valueModel = viewModel as DtoValueViewModel;
                if (valueModel != null && percentProps.ContainsKey(prop.Name))
                {
                    valueModel.AlternateRepresentation = string.Format("{0:0.00} %", 100 * Convert.ToDouble(dto.GetPropertyValue<object>(percentProps[prop.Name].Name)));
                    valueModel.AlternateRepresentationAlignment = ContentAlignment.MiddleRight;
                }

                result.Items.Add(viewModel);
            }

            return result;
        }

        /// <summary>
        /// Creates a table out of a list of DTOs
        /// </summary>
        public static DtoTableViewModel BuildTableFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null) return null;

            // skip XmlDictionary to its values
            if (dto.GetType().HasGenericDefinition(typeof(XmlDictionary<,>)))
            {
                dto = dto.GetPropertyValue<object>("Values");
            }

            var debugInfo = parentProp == null
                ? string.Format("topTable: {0}", dto.GetType())
                : string.Format("table:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto.GetType());
            var result = new DtoTableViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
            {
                IsDataTable = true,
                Title = ExtractTitle(parentProp, dto),
                Description = ExtractDescription(parentProp, dto),
                Background = ExtractBackground(parentProp, dto),
                AlternateBackground = ExtractAlternateBackground(parentProp, dto),
                Formatting = ExtractFormatting(parentProp, dto),
                IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                Root = root,
                Data = dto,
            };

            var dataProps = new List<PropertyInfo>();
            var percentProps = new Dictionary<string, PropertyInfo>();
            ExtractProps(dto.GetType().FindElementTypes().SingleOrDefault(t => t != typeof(object)) ?? dto.GetType(),
                dataProps, percentProps);

            var allColumns = new Dictionary<PropertyInfo, DtoColumnViewModel>();
            int columnIdx = 0;
            foreach (var prop in dataProps)
            {
                var column = new DtoColumnViewModel(dependencies, dataCtx, result, fileOpener, tmpService, columnIdx, string.Format("column:{0}.{1}", dto.GetType(), prop.Name))
                {
                    Title = ExtractTitle(prop, null),
                    Description = ExtractDescription(prop, null),
                    Background = ExtractBackground(prop, null)
                };
                allColumns[prop] = column;
                columnIdx += 1;
                result.Columns.Add(column);
            }

            int rowIdx = 0;
            foreach (var line in (IEnumerable)dto)
            {
                var row = new DtoRowViewModel(dependencies, dataCtx, result, fileOpener, tmpService, rowIdx, string.Format("row:{0}[{1}]", dto.GetType(), rowIdx));
                if (rowIdx % 2 == 0)
                {
                    row.Background = result.AlternateBackground;
                }

                result.Rows.Add(row);

                columnIdx = -1;
                foreach (var prop in dataProps)
                {
                    var propName = prop.Name;
                    columnIdx += 1;
                    var viewModel = BuildFrom(root, prop, line.GetPropertyValue<object>(propName), dependencies, dataCtx, row, fileOpener, tmpService);
                    if (viewModel == null) continue; // do not add cell without content

                    viewModel.Title = null; // do not display title in table
                    var valueModel = viewModel as DtoValueViewModel;
                    if (valueModel != null && percentProps.ContainsKey(propName))
                    {
                        valueModel.AlternateRepresentation = string.Format("{0:0.00} %", 100 * Convert.ToDouble(dto.GetPropertyValue<object>(percentProps[propName].Name)));
                        valueModel.AlternateRepresentationAlignment = ContentAlignment.MiddleRight;
                    }

                    var cellDebugInfo = parentProp == null
                        ? string.Format("topCell:[{0}].{1}", rowIdx, propName)
                        : string.Format("cell:{0}.{1}[{2}].{3}", parentProp.DeclaringType, parentProp.Name, rowIdx, propName);
                    var cell = new DtoCellViewModel(dependencies, dataCtx, result, fileOpener, tmpService, row, allColumns[prop], new GuiGridLocationAttribute(rowIdx, columnIdx), viewModel, cellDebugInfo);
                    result.Cells.Add(cell);
                }

                rowIdx += 1;
            }

            return result;
        }

        /// <summary>
        /// Arranges the contained Objects in a grid. Use GridLocation to specify where
        /// </summary>
        public static DtoTableViewModel BuildGridFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null) return null;

            var debugInfo = parentProp == null
                ? string.Format("topGrid:{0}", dto.GetType())
                : string.Format("grid:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto.GetType());
            var result = new DtoTableViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
            {
                IsDataTable = false,
                Title = ExtractTitle(parentProp, dto),
                Description = ExtractDescription(parentProp, dto),
                Background = ExtractBackground(parentProp, dto),
                Formatting = ExtractFormatting(parentProp, dto),
                IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                Root = root,
                Data = dto,
            };

            // TODO: add description

            var cells = new Dictionary<GuiGridLocationAttribute, ViewModel>();

            if (typeof(IEnumerable).IsAssignableFrom(dto.GetType()))
            {
                var propertyMsg = parentProp == null
                    ? string.Empty
                    : string.Format(" contained in property {0}.{1}", parentProp.DeclaringType.Name, parentProp.Name);
                Logging.Client.WarnFormat("Unable to format a list from dto '{0}' of type '{1}'{2}",
                                dto,
                                dto.GetType().Name,
                                propertyMsg);
            }
            else
            {
                var dataProps = new List<PropertyInfo>();
                var percentProps = new Dictionary<string, PropertyInfo>();
                ExtractProps(dto.GetType(), dataProps, percentProps);

                if (percentProps.Count != 0)
                {
                    // TODO: fail: cannot display in grid?
                }

                foreach (var prop in dataProps)
                {
                    var value = BuildFrom(root, prop, dto.GetPropertyValue<object>(prop.Name), dependencies, dataCtx, result, fileOpener, tmpService);
                    if (value == null) continue; // do not add without content

                    // struct initialises to (0,0) by default
                    var gridLocation = prop.GetCustomAttributes(false)
                        .OfType<GuiGridLocationAttribute>()
                        .Single();

                    // TODO: avoid silent overwriting
                    /* 
                     * TODO: might want to consider automatic appending?
                     * That is, given a class with five properties that should be arranged
                     * 
                     *    A | B | C
                     *    D | E | -
                     * 
                     * specify
                     * 
                     *    [GridRow(0)]
                     *    int A { get; set; }
                     *    int B { get; set; }
                     *    int C { get; set; }
                     *    [GridRow(1)]
                     *    int D { get; set; }
                     *    int E { get; set; }
                     *
                     * or even only [GridRowBreak] on D?
                     */
                    cells[gridLocation] = value;
                }
            }

            var allRows = new Dictionary<int, DtoRowViewModel>();
            for (int i = cells.Keys.Select(k => k.Row).Max(); i >= 0; i--)
            {
                allRows[i] = new DtoRowViewModel(dependencies, dataCtx, result, fileOpener, tmpService, i, string.Format("gridrow:{0}.[{1}]", dto.GetType(), i));
            }

            var allColumns = new Dictionary<int, DtoColumnViewModel>();
            for (int i = cells.Keys.Select(k => k.Column).Max(); i >= 0; i--)
            {
                allColumns[i] = new DtoColumnViewModel(dependencies, dataCtx, result, fileOpener, tmpService, i, string.Format("gridcolum:{0}.[][{1}]", dto.GetType(), i));
            }

            foreach (var kvp in cells)
            {
                result.Cells.Add(new DtoCellViewModel(dependencies, dataCtx, result, fileOpener, tmpService, allRows[kvp.Key.Row], allColumns[kvp.Key.Column], kvp.Key, kvp.Value, string.Format("gridcell[{0}][{1}]", kvp.Key.Row, kvp.Key.Column)));
            }

            allRows.Values.ForEach(result.Rows.Add);
            allColumns.Values.ForEach(result.Columns.Add);

            return result;
        }

        /// <summary>
        /// Build a tabbed interface from the specified object. Page oriented output might create new pages for each tab or similar.
        /// </summary>
        public static DtoTabbedViewModel BuildTabbedFrom(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null) return null;

            var debugInfo = parentProp == null
                ? string.Format("topTabbed:{0}", dto.GetType())
                : string.Format("tabbed:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto.GetType());
            var result = new DtoTabbedViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
            {
                Title = ExtractTitle(parentProp, dto),
                Description = ExtractDescription(parentProp, dto),
                Background = ExtractBackground(parentProp, dto),
                Formatting = ExtractFormatting(parentProp, dto),
                IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                Root = root,
                Data = dto,
            };

            // need to extract value from XmlDictionaries
            var dtoData = dto as IXmlDictionaryDtoData;
            if (dtoData != null)
            {
                foreach (var kvp in dtoData.DtoData.OrderBy(e => e.Key))
                {
                    var item = BuildFrom(root, parentProp, kvp.Value, dependencies, dataCtx, result, fileOpener, tmpService);
                    if (item != null) result.Items.Add(item);
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(dto.GetType()))
            {
                foreach (var element in ((IEnumerable)dto))
                {
                    var item = BuildFrom(root, parentProp, element, dependencies, dataCtx, result, fileOpener, tmpService);
                    if (item != null) result.Items.Add(item);
                }
            }
            else
            {
                var dataProps = new List<PropertyInfo>();
                var percentProps = new Dictionary<string, PropertyInfo>();
                ExtractProps(dto.GetType(), dataProps, percentProps);

                if (percentProps.Count != 0)
                {
                    // TODO: fail: cannot display in grid?
                }

                foreach (var prop in dataProps)
                {
                    var val = dto.GetPropertyValue<object>(prop.Name);
                    var item = BuildFrom(root, prop, val, dependencies, dataCtx, result, fileOpener, tmpService);
                    if (item != null) result.Items.Add(item);
                }
            }

            return result;
        }

        private static DtoBaseViewModel FormatValue(object root, PropertyInfo parentProp, object dto, IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (dto == null) throw new ArgumentNullException("dto");
            if (parentProp == null) throw new ArgumentNullException("parentProp");

            DtoBaseViewModel valueModel = null;
            var propertyType = dto.GetType();
            var title = ExtractTitle(parentProp, dto);
            var description = ExtractDescription(parentProp, dto);
            var background = ExtractBackground(parentProp, dto);
            var asPercent = parentProp.GetCustomAttributes(typeof(GuiFormatAsPercentAttribute), true).Length > 0;
            var formatString = parentProp.GetCustomAttributes(typeof(GuiFormatStringAttribute), true).OfType<GuiFormatStringAttribute>().Select(gfsa => gfsa.FormatString).SingleOrDefault();

            if (typeof(long).IsAssignableFrom(propertyType) || typeof(int).IsAssignableFrom(propertyType) || typeof(short).IsAssignableFrom(propertyType))
            {
                valueModel = new DtoValueViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, string.Format("value:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto))
                {
                    Value = asPercent ? string.Format("{0} %", 100 * Convert.ToInt64(dto)) : string.Format(formatString ?? "{0}", dto),
                    ValueAlignment = ContentAlignment.MiddleRight,
                    Title = title,
                    Description = description,
                    Formatting = ExtractFormatting(parentProp, dto),
                    IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                    Root = root,
                    Data = dto,
                };
                if (!string.IsNullOrEmpty(background))
                {
                    valueModel.Background = background;
                }
            }
            else if (typeof(double).IsAssignableFrom(propertyType) || typeof(decimal).IsAssignableFrom(propertyType) || typeof(float).IsAssignableFrom(propertyType))
            {
                valueModel = new DtoValueViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, string.Format("value:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto))
                {
                    Value = asPercent ? string.Format("{0:0.00} %", 100 * Convert.ToDouble(dto)) : string.Format(formatString ?? "{0:0.00}", dto),
                    ValueAlignment = ContentAlignment.MiddleRight,
                    Title = title,
                    Description = description,
                    Formatting = ExtractFormatting(parentProp, dto),
                    IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                    Root = root,
                    Data = dto,
                };
                if (!string.IsNullOrEmpty(background))
                {
                    valueModel.Background = background;
                }
            }
            else if (typeof(string).IsAssignableFrom(propertyType))
            {
                valueModel = new DtoValueViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, string.Format("value:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto))
                {
                    Value = (dto ?? string.Empty).ToString(),
                    Title = title,
                    Description = description,
                    Formatting = ExtractFormatting(parentProp, dto),
                    IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                    Root = root,
                    Data = dto,
                };
                if (!string.IsNullOrEmpty(background))
                {
                    valueModel.Background = background;
                }
            }
            else if (typeof(DateTime).IsAssignableFrom(propertyType))
            {
                valueModel = new DtoValueViewModel(dependencies, dataCtx, parent, fileOpener, tmpService, string.Format("value:{0}.{1} = {2}", parentProp.DeclaringType, parentProp.Name, dto))
                {
                    Value = dto != null ? ((DateTime)dto).ToShortDateString() : string.Empty,
                    Title = title,
                    Description = description,
                    Formatting = ExtractFormatting(parentProp, dto),
                    IsPrintableRoot = ExtractIsPrintableRoot(parentProp, dto),
                    Root = root,
                    Data = dto,
                };
                if (!string.IsNullOrEmpty(background))
                {
                    valueModel.Background = background;
                }
            }
            else
            {
                valueModel = BuildFrom(root, parentProp, dto, dependencies, dataCtx, parent, fileOpener, tmpService);
            }

            if (valueModel == null)
            {
                Logging.Client.WarnFormat("Unable to format a value from dto '{0}' of type '{1}' contained in property {2}",
                                dto,
                                dto.GetType().Name,
                                string.Format("{0}.{1}", parentProp.DeclaringType.Name, parentProp.Name));
            }

            return valueModel;
        }

        private static void ExtractProps(Type type, List<PropertyInfo> dataProps, Dictionary<string, PropertyInfo> percentProps)
        {
            foreach (var prop in type.GetProperties())
            {
                if (prop.Name.EndsWith("Percent") || prop.Name.EndsWith("Prozent")) // TODO: Remove german version
                {
                    var name = prop.Name.Substring(0, prop.Name.Length - 7);
                    percentProps[name] = prop;
                }
                else
                {
                    // only add if browsable
                    var browsable = prop.GetCustomAttributes(typeof(BrowsableAttribute), false);
                    if (browsable != null && (browsable.Length == 0 || browsable.Contains(BrowsableAttribute.Yes)))
                    {
                        dataProps.Add(prop);
                    }
                }
            }
        }

        /// <summary>
        /// If a prop is specified, the GuiTitleAttribute will be extracted from the prop.
        /// Then, if the specified object has a GuiClassTitle attributed property, its value extracted.
        /// Else, the GuiClassTitleAttribute of the class is used.
        /// If none is found, the name of the class is used.
        /// </summary>
        private static string ExtractTitle(PropertyInfo prop, object dto)
        {
            var result = ExtractAnything<GuiTitleAttribute, GuiClassTitleAttribute, string>(prop, dto, gta => gta.Title);

            if (string.IsNullOrEmpty(result))
            {
                if (prop != null)
                {
                    result = prop.Name;
                }
                else if (dto != null)
                {
                    result = dto.GetType().Name;
                }
            }

            return result;
        }

        /// <summary>
        /// If a prop is specified, the DescriptionAttribute will be extracted from the prop.
        /// Then, if the specified object has a GuiDescription attributed property, its value extracted.
        /// Else, the DescriptionAttribute of the class is used.
        /// If none is found, the empty string is returned.
        /// </summary>
        private static string ExtractDescription(PropertyInfo prop, object dto)
        {
            return ExtractAnything<DescriptionAttribute, GuiDescriptionAttribute, string>(prop, dto, gta => gta.Description)
                ?? string.Empty;
        }

        /// <summary>
        /// If a prop is specified, the GuiBackgroundAttribute will be extracted from the prop.
        /// Then, if the specified object has a GuiBackground attributed property, its value is extracted.
        /// Else, the GuiBackgroundAttribute of the class is used.
        /// If none is found, the empty string is returned.
        /// </summary>
        private static string ExtractBackground(PropertyInfo prop, object dto)
        {
            return ExtractAnything<GuiBackgroundAttribute, GuiClassAlternateBackgroundAttribute, string>(prop, dto, gta => gta.Color)
                ?? string.Empty;
        }

        /// <summary>
        /// This function extracts the alternate background value, if available:
        /// If a prop is specified, the GuiBackgroundAttribute will be extracted from the prop.
        /// Then, if the specified object has a GuiBackground attributed property, its value is extracted.
        /// Else, the GuiBackgroundAttribute of the class is used.
        /// If none is found, the empty string is returned.
        /// </summary>
        private static string ExtractAlternateBackground(PropertyInfo prop, object dto)
        {
            return ExtractAnything<GuiBackgroundAttribute, GuiClassAlternateBackgroundAttribute, string>(prop, dto, gta => gta.AlternateColor)
                ?? ExtractBackground(prop, dto);
        }

        private static TResult ExtractAnything<TAttr, TClassAttr, TResult>(PropertyInfo prop, object dto, Func<TAttr, TResult> extractor)
            where TAttr : Attribute
            where TClassAttr : Attribute
        {
            var result = default(TResult);

            // Check whether the property itself has an attribtute, extract its value if available
            if (prop != null)
            {
                result = prop.GetCustomAttributes(typeof(TAttr), true)
                    .OfType<TAttr>()
                    .Select(extractor)
                    .SingleOrDefault();
            }

            // if no value was extracted and we have a data object, try to extract from there
            if (object.Equals(result, default(TResult)) && dto != null)
            {
                var type = dto.GetType();
                var elementType = typeof(IEnumerable).IsAssignableFrom(type)
                    ? type.FindElementTypes().SingleOrDefault(t => t != typeof(object)) ?? type
                    : type;

                var clsDescriptionProp = elementType.GetProperties().SingleOrDefault(p => p.GetCustomAttributes(typeof(TClassAttr), true).Length > 0);
                if (clsDescriptionProp != null)
                {
                    object element = dto;
                    if (elementType == type && element != null)
                    {
                        result = element.GetPropertyValue<TResult>(clsDescriptionProp.Name);
                    }
                }
                else
                {
                    // the contained element do not supply data, so try their class instead
                    result = elementType.GetCustomAttributes(typeof(TAttr), true).OfType<TAttr>().Select(extractor).SingleOrDefault();
                }
            }

            return result;
        }

        /// <summary>
        /// If a prop is specified, the GuiBackgroundAttribute will be extracted from the prop.
        /// Then, if the specified object has a GuiBackground attributed property, its value is extracted.
        /// Else, the GuiBackgroundAttribute of the class is used.
        /// If none is found, the empty string is returned.
        /// </summary>
        private static Formatting ExtractFormatting(PropertyInfo parentProp, object dto)
        {
            return ExtractAnything<GuiFormattingAttribute, GuiClassFormattingAttribute, Formatting?>(parentProp, dto, gta => gta.Formatting)
                ?? Formatting.None;
        }

        private static bool ExtractIsPrintableRoot(PropertyInfo parentProp, object dto)
        {
            return ExtractAnything<GuiPrintableRootAttribute, GuiClassPrintableRootAttribute, bool?>(parentProp, dto, gta => gta.IsPrintableRoot)
                ?? false;
        }

        public static Tdto Combine<Tdto>(Tdto a, Tdto b)
            where Tdto : DtoBaseViewModel
        {
            if (a == null) throw new ArgumentNullException("a");
            if (b == null) throw new ArgumentNullException("b");

            if (a.GetType() == b.GetType())
            {
                a.ApplyChangesFrom(b);
                return a;
            }
            else
            {
                return b;
            }
        }

        public static void Merge<Tdto>(ObservableCollection<Tdto> a, ObservableCollection<Tdto> b)
            where Tdto : DtoBaseViewModel
        {
            if (a == null) throw new ArgumentNullException("a");
            if (b == null) throw new ArgumentNullException("b");

            int idx = 0;
            for (; idx < a.Count && idx < b.Count; idx++)
            {
                a[idx] = DtoBuilder.Combine(a[idx], b[idx]);
            }

            // delete from last element downwards
            if (b.Count < a.Count)
            {
                int deleteIdx = a.Count - 1;
                while (deleteIdx > idx)
                {
                    a.RemoveAt(deleteIdx);
                    deleteIdx -= 1;
                }
            }
            else if (b.Count > a.Count)
            {
                // add leftover items
                while (idx < b.Count)
                {
                    a.Add(b[idx]);
                    idx += 1;
                }
            }
        }
    }
}
