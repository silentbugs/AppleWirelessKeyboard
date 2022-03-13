﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AppleWirelessKeyboardCore.Services;

namespace AppleWirelessKeyboardCore.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ShowOff<TGlyph>(bool valueBar = false, int value = 0) where TGlyph : UserControl
        {
            if (SettingsService.Default.EnableOverlay)
                App.Window.Dispatcher.Invoke(() =>
                {
                    DataContext = new { Glyph = Activator.CreateInstance<TGlyph>() };

                    ValueBar.Visibility = valueBar ? Visibility.Visible : Visibility.Collapsed;

                    MakeValue(value);

                    Show();

                    var fade = new DoubleAnimationUsingKeyFrames();
                    fade.Duration = new Duration(TimeSpan.FromSeconds(1));
                    fade.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(0)));
                    fade.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromPercent(0.5)));
                    fade.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromPercent(1)));
                    BeginAnimation(OpacityProperty, fade);
                });
        }
        public void MakeValue(int value)
        {
            ValueBar.Children.Clear();

            for (var i = 0; i <= value; i++)
            {
                var rect = new Rectangle();
                rect.Fill = new SolidColorBrush(Colors.White);
                rect.Width = 6;
                rect.Height = 6;
                rect.Margin = new Thickness(0, 0, 3, 0);
                ValueBar.Children.Add(rect);
            }
        }
    }
}
