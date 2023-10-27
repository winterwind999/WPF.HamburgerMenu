using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HamburgerMenuControl
{
    public class HamburgerMenu : Control
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(false, OnIsOpenPropertyChanged));

        private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HamburgerMenu hamburgerMenu)
            {
                hamburgerMenu.OnIsOpenPropertyChanged();
            }
        }

        private void OnIsOpenPropertyChanged()
        {
            if (IsOpen)
            {
                OpenMenuAnimated();
            }
            else
            {
                CloseMenuAnimated();
            }
        }

        public static readonly DependencyProperty OpenClosedDurationProperty =
            DependencyProperty.Register("OpenClosedDuration", typeof(Duration), typeof(HamburgerMenu), new PropertyMetadata(Duration.Automatic));
        public Duration OpenClosedDuration
        {
            get { return (Duration)GetValue(OpenClosedDurationProperty); }
            set { SetValue(OpenClosedDurationProperty, value); }
        }

        private void OpenMenuAnimated()
        {
            double contentWidth = GetDesiredContentWidth();

            DoubleAnimation openingAnimation = new DoubleAnimation(contentWidth, OpenClosedDuration);
            BeginAnimation(WidthProperty, openingAnimation);
        }

        public static readonly DependencyProperty FallbackOpenWidthProperty =
                    DependencyProperty.Register("FallbackOpenWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(100.0));
        public double FallbackOpenWidth
        {
            get { return (double)GetValue(FallbackOpenWidthProperty); }
            set { SetValue(FallbackOpenWidthProperty, value); } 
        }

        private double GetDesiredContentWidth()
        {
            if (Content == null)
            {
                return FallbackOpenWidth;
            }

            Content.Measure(new Size(MaxWidth, MaxHeight));
            return Content.DesiredSize.Width;
        }

        private void CloseMenuAnimated()
        {
            DoubleAnimation closingAnimation = new DoubleAnimation(0, OpenClosedDuration);
            BeginAnimation(WidthProperty, closingAnimation);
        }

        public HamburgerMenu()
        {
            Width = 0;
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(HamburgerMenu), new PropertyMetadata(null));

        public FrameworkElement Content
        {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        static HamburgerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HamburgerMenu), new FrameworkPropertyMetadata(typeof(HamburgerMenu)));
        }
    }
}
