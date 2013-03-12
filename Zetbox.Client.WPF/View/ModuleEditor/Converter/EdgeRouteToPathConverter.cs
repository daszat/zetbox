using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Zetbox.Client.Presentables.ModuleEditor;
using Zetbox.App.Extensions;

namespace Zetbox.Client.WPF.View.ModuleEditor.Converter
{
    /// <summary>
    /// From the GraphSharp Library, modified
    /// Converts the position and sizes of the source and target points, and the route informations
    /// of an edge to a path.
    /// The edge can bend, or it can be straight line.
    /// </summary>
    public class EdgeRouteToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Debug.Assert(values != null && values.Length == 10, "EdgeRouteToPathConverter should have 10 parameters: pos (1,2), size (3,4) of source; pos (5,6), size (7,8) of target; routeInformation (9); Tag (10).");

            #region Get the inputs
            //get the position of the source
            Point sourcePos = new Point()
            {
                X = (values[0] != DependencyProperty.UnsetValue ? (double)values[0] : 0.0),
                Y = (values[1] != DependencyProperty.UnsetValue ? (double)values[1] : 0.0)
            };
            //get the size of the source
            Size sourceSize = new Size()
            {
                Width = (values[2] != DependencyProperty.UnsetValue ? (double)values[2] : 0.0),
                Height = (values[3] != DependencyProperty.UnsetValue ? (double)values[3] : 0.0)
            };
            //get the position of the target
            Point targetPos = new Point()
            {
                X = (values[4] != DependencyProperty.UnsetValue ? (double)values[4] : 0.0),
                Y = (values[5] != DependencyProperty.UnsetValue ? (double)values[5] : 0.0)
            };
            //get the size of the target
            Size targetSize = new Size()
            {
                Width = (values[6] != DependencyProperty.UnsetValue ? (double)values[6] : 0.0),
                Height = (values[7] != DependencyProperty.UnsetValue ? (double)values[7] : 0.0)
            };

            //get the route informations
            object tag = values[9];
            #endregion

            //
            // Create the path
            //
            Point p1 = GraphConverterHelper.CalculateAttachPoint(sourcePos, sourceSize, targetPos);
            Point p2 = GraphConverterHelper.CalculateAttachPoint(targetPos, targetSize, sourcePos);
            var length = (p2 - p1).Length;

            PathFigureCollection pfc = new PathFigureCollection(2);

            if (tag is DiagramViewModel.InheritanceEdge)
            {
                Point pFirst = p2;
                Vector v = pFirst - p1;
                v = v / v.Length * 10;
                Vector n = new Vector(-v.Y, v.X) * 0.6;

                pfc.Add(new PathFigure(p1, new[] { new LineSegment(p2, true) }, false));
                pfc.Add(new PathFigure(p1, new[] { new LineSegment(p1 + v - n, true),
                                                   new LineSegment(p1 + v + n, true)}, true));
            }
            else if (tag is DiagramViewModel.RelationEdge)
            {
                Point pFirst = p2;
                Point pLast = p1;

                Vector vFirst = pFirst - p1;
                vFirst = vFirst / vFirst.Length * 10;
                Vector nFirst = new Vector(-vFirst.Y, vFirst.X) * 0.6;

                Vector vLast = pLast - p2;
                vLast = vLast / vLast.Length * 10;
                Vector nLast = new Vector(-vLast.Y, vLast.X) * 0.6;

                var rel = ((DiagramViewModel.RelationEdge)tag).Rel;
                pfc.Add(new PathFigure(p1 + (rel.A.Multiplicity.UpperBound() > 1 ? vFirst * 2 : vFirst),
                    new[] { new ArcSegment(p2 + (rel.B.Multiplicity.UpperBound() > 1 ? vLast * 2: vLast), 
                            new Size(length, length), 0, false, SweepDirection.Clockwise, true) }, false));

                pfc.Add(new PathFigure(p1,
                                         new[] { new LineSegment(p1 + vFirst - nFirst, true),
                                                 new LineSegment(p1 + vFirst + nFirst, true)}, true));
                pfc.Add(new PathFigure(p2,
                                         new[] { new LineSegment(p2 + vLast - nLast, true),
                                                 new LineSegment(p2 + vLast + nLast, true)}, true));

                if (rel.A.Multiplicity.UpperBound() > 1)
                {
                    pfc.Add(new PathFigure(p1 + vFirst,
                                             new[] { new LineSegment(p1 + (vFirst*2) - nFirst, true),
                                                     new LineSegment(p1 + (vFirst*2) + nFirst, true)}, true));
                }

                if (rel.B.Multiplicity.UpperBound() > 1)
                {
                    pfc.Add(new PathFigure(p2 + vLast,
                                             new[] { new LineSegment(p2 + (vLast*2) - nLast, true),
                                                     new LineSegment(p2 + (vLast*2) + nLast, true)}, true));
                }
            }

            return pfc;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}