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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.Calendar;
using Zetbox.Client.WPF.Toolkit;
using Zetbox.Client.WPF.Converter;

namespace Zetbox.Client.WPF.View.Calendar
{
    /// <summary>
    /// Interaction logic for CalendarDay.xaml
    /// </summary>
    /// <remarks>Based on http://www.codeproject.com/KB/WPF/WPFOutlookCalendar.aspx </remarks>
    public partial class CalendarDay : UserControl, IHasViewModel<DayCalendarViewModel>
    {
        public CalendarDay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();

            panelCalendarDay.SizeChanged += new SizeChangedEventHandler(panelCalendarDay_SizeChanged);
            DataContextChanged += new DependencyPropertyChangedEventHandler(CalendarDay_DataContextChanged);
        }

        void CalendarDay_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.ActualWidth = panelCalendarDay.ActualWidth;
                ViewModel.PropertyChanged += new PropertyChangedEventHandler(ViewModel_PropertyChanged);
            }
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DayItems")
            {
                var borderThickness = new Thickness(1, 1, 1, 1);
                var selectedBorderThickness = new Thickness(3, 3, 3, 3);
                var borderBrush = new SolidColorBrush(Color.FromRgb(0x5d, 0x8c, 0xc9));
                borderBrush.Freeze();
                var borderPadding = new Thickness(5, 3, 3, 3);
                var selectedBorderPadding = new Thickness(3, 1, 1, 1);
                var isSelectedStyle = new Style()
                {
                    TargetType = typeof(Border),
                    Setters =
                    {
                        new Setter 
                        {
                            Property = Border.BorderThicknessProperty,
                            Value = borderThickness,
                        },
                        new Setter 
                        {
                            Property = Border.BorderBrushProperty,
                            Value = borderBrush,
                        },
                        new Setter 
                        {
                            Property = Border.PaddingProperty,
                            Value = borderPadding,
                        }, 
                    },
                    Triggers = 
                    {
                        new DataTrigger()
                        {
                            Binding = new Binding() { Path = new PropertyPath("IsSelected") },
                            Value = true,
                            Setters =
                            {
                                new Setter 
                                {
                                    Property = Border.BorderThicknessProperty,
                                    Value = selectedBorderThickness,
                                },
                                new Setter 
                                {
                                    Property = Border.BorderBrushProperty,
                                    Value = Brushes.Black,
                                },
                                new Setter 
                                {
                                    Property = Border.PaddingProperty,
                                    Value = selectedBorderPadding,
                                },
                            }
                        }
                    }
                };

                items.Children.Clear();
                items.BeginInit();
                try
                {
                    foreach (var item in ViewModel.DayItems)
                    {
                        var itemColor = (Color)ColorConverter.ConvertFromString(item.Color);
                        var borderBackground = new LinearGradientBrush(LighterShadeConverter.MakeLighter(itemColor), itemColor, 0.0);
                        borderBackground.Freeze();

                        var border = new Border()
                        {
                            Height = item.Height,
                            Width = item.Width,
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                            SnapsToDevicePixels = true,
                            Background = borderBackground,
                            DataContext = item,
                            Style = isSelectedStyle,
                        };
                        Canvas.SetLeft(border, item.Position.X);
                        Canvas.SetTop(border, item.Position.Y);
                        border.MouseLeftButtonDown += calendarItem_MouseLeftButtonDown;
                        border.Child = new StackPanel()
                        {
                            Children =
                            {
                                new TextBlock() 
                                {
                                    Text = item.Summary,
                                    FontWeight = FontWeights.Bold,
                                },
                                new TextBlock() 
                                {
                                    Text = item.FromToText,
                                }
                            }
                        };

                        items.Children.Add(border);
                    }
                }
                finally
                {
                    items.EndInit();
                }
            }
        }

        void panelCalendarDay_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.ActualWidth = panelCalendarDay.ActualWidth;
                foreach (var border in items.Children.OfType<Border>())
                {
                    var item = (CalendarItemViewModel)border.DataContext;
                    border.Width = item.Width;
                    Canvas.SetLeft(border, item.Position.X);
                }
            }
        }

        #region calendarItem
        void calendarItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                var fe = (FrameworkElement)sender;
                var vmdl = fe.DataContext as CalendarItemViewModel;
                if (vmdl != null)
                {
                    if (e.ClickCount == 1)
                    {
                        ViewModel.WeekCalendar.SelectedItem = vmdl.EventViewModel;
                    }
                    else if (e.ClickCount == 2)
                    {
                        ViewModel.WeekCalendar.NotifyOpen(vmdl.EventViewModel);
                    }
                }
            }
        }
        #endregion

        #region timeslot
        void timeslot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement)
            {
                var fe = (FrameworkElement)sender;
                var vmdl = fe.DataContext as TimeSlotItemViewModel;
                if (vmdl != null)
                {
                    if (e.ClickCount == 1)
                    {
                        ViewModel.WeekCalendar.SelectedItem = null;
                    }
                    else if (e.ClickCount == 2)
                    {
                        ViewModel.WeekCalendar.NotifyNew(vmdl.DateTime);
                    }
                }
            }
        }
        #endregion

        #region IHasViewModel<DayCalendarViewModel> Members

        public DayCalendarViewModel ViewModel
        {
            get { return (DayCalendarViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
    }
}
