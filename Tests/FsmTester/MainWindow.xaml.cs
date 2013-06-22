using System;
using System.Collections.Generic;
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
using System.Timers;
using System.Windows.Media.Animation;

namespace FsmTester
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static double diameter = 0.6;
        static double C = Math.PI * diameter;
        Storyboard story;

        public void SetSpeed(double speed)
        {
            var old = story.SpeedRatio;
            var news = speed / old;
            story.SetSpeedRatio(news);
        }

        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimation sb = new DoubleAnimation();
            story = (Storyboard)canvas1.FindResource("spin");
            story.Begin();
            story.SetSpeedRatio(0);
        }

        private void gas_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void gas_button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void gas_button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox2.Text = "down";
        }

        private void gas_button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textBox2.Text = "up";
        }

    }
}
