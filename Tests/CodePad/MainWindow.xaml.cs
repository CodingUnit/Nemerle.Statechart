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

namespace CodePadExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CodePad fsm = new CodePad();

        public MainWindow()
        {
            InitializeComponent();
            fsm.lock_door += () => Dispatcher.Invoke(new Action(fsm_lock_door));
            fsm.unlock_door += () => Dispatcher.Invoke(new Action(fsm_unlock_door));
            fsm.Show += x => Dispatcher.Invoke(new Action<string>(fsm_Show), x);
            fsm.TransitionCompleted += (x, y) => Dispatcher.Invoke(new EventHandler(fsm_TransitionCompleted), x, y);
            fsm.Initiate();
        }

        void fsm_TransitionCompleted(object sender, EventArgs e)
        {
            if (statusBar.Items.Count == 2) statusBar.Items.RemoveAt(1);
            statusBar.Items.Add(fsm.ToString());
        }

        void fsm_Show(string obj)
        {
            display.Text = obj;
        }

        void fsm_unlock_door()
        {
            door_status.Text = "Unlocked";
        }

        void fsm_lock_door()
        {
            door_status.Text = "Locked";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(1);
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(0);
        }

        private void key_button_Click(object sender, RoutedEventArgs e)
        {
            fsm.key();
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            fsm.cancel();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(3);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(5);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(6);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(7);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(8);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            fsm.num(9);
        }

        private void ibutton_Click(object sender, RoutedEventArgs e)
        {
            fsm.magnetic_key();
        }

        private void inside_button_Click(object sender, RoutedEventArgs e)
        {
            fsm.Inside_button();
        }

        private void homephone_open_Click(object sender, RoutedEventArgs e)
        {
            fsm.enter_permitted();
        }

        private void homephone_answer_button_Click(object sender, RoutedEventArgs e)
        {
            fsm.answer();
        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {
            var str = e.Text;
            var ch = str[0];
            if (ch >= '0' && ch <= '9') 
            {
                fsm.num(int.Parse(str));
                e.Handled = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                fsm.key();
                e.Handled = true;
            }
            else
                if (e.Key == Key.Escape)
                {
                    fsm.cancel();
                    e.Handled = true;
                }
        }

    }
}
