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
using Nemerle.Statechart.Tests;

namespace AlarmClockWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlarmClock fsm = new AlarmClock();

        public MainWindow()
        {
            InitializeComponent();
            fsm.TransitionCompleted += (x, y) => Dispatcher.Invoke(new Action(fsm_TransitionCompleted));
            fsm.Initiate();
        }

        void fsm_TransitionCompleted()
        {
            if (status.Items.Count == 2) status.Items.RemoveAt(1);
            status.Items.Add(fsm.ToString());
        }

        private void hour_button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            fsm.push_hour();
        }

        private void minute_button_Click(object sender, RoutedEventArgs e)
        {
            fsm.push_min();
        }

        private void mode_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            switch ((int)e.NewValue)
            {
                case 0: fsm.time_set(); break;
                case 1: fsm.run(); break;
                case 2: fsm.alarm_set(); break;
            }
            e.Handled = true;
        }

    }
}
