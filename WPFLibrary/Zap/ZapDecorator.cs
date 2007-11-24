using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Microsoft.Samples.KMoore.WPFSamples.Zap
{


    public class ZapDecorator : Decorator
    {
        public ZapDecorator()
        {
            this.Unloaded += ZapDecorator_Unloaded;
            this.Loaded += ZapDecorator_Loaded;
        }

        private void ZapDecorator_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void ZapDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            //SUPER IMPORTANT!
            //If we don't un-register, this instance will leak!
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }        

        //TODO: be smart about item changes...make sure the control doesn't jump around
        //TODO: be smart about wiring/unwiring this callback. Expensive!!
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (this.Child != _zapPanel)
            {
                _zapPanel = this.Child as ZapPanel;
                _zapPanel.RenderTransform = _traslateTransform;
            }
            if (_zapPanel != null)
            {
                int actualTargetIndex = Math.Max(0, Math.Min(_zapPanel.Children.Count - 1, TargetIndex));

                double targetPercentOffset = -actualTargetIndex / (double)_zapPanel.Children.Count;
                double currentPercent = _percentOffset;
                double deltaPercent = targetPercentOffset - currentPercent;

                if (!double.IsNaN(deltaPercent) && !double.IsInfinity(deltaPercent))
                {
                    _velocity *= .9;
                    _velocity += deltaPercent * .01;

                    if (Math.Abs(_velocity) > DIFF || Math.Abs(deltaPercent) > DIFF)
                    {
                        _percentOffset += _velocity;
                    }
                    else
                    {
                        _percentOffset = targetPercentOffset;
                        _velocity = 0;
                    }
                }

                double targetPixelOffset = _percentOffset * (this.RenderSize.Width * _zapPanel.Children.Count);
                

                _traslateTransform.X = targetPixelOffset;

            }
        }

        public static readonly DependencyProperty TargetIndexProperty = DependencyProperty.Register("TargetIndex",
            typeof(int), typeof(ZapDecorator));

        public int TargetIndex
        {
            get { return (int)GetValue(TargetIndexProperty); }
            set { SetValue(TargetIndexProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UIElement element1 = this.Child;
            if (element1 != null)
            {
                element1.Measure(constraint);
            }
            return new Size();
        }

        double _velocity = 0;
        double _percentOffset = 0;

        ZapPanel _zapPanel;
        TranslateTransform _traslateTransform = new TranslateTransform();

        private const double DIFF = .0001;
    }
}
