using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nemerle.Statechart.Tests;
using Nemerle.Statechart;

namespace FsmTester
{
    class Car
    {
        CarFsm fsm = new CarFsm();
        MainWindow window;

        public Car(MainWindow win)
        {
            window = win;
            window.stackPanel1.DataContext = fsm;
            window.key_button.Click   += new System.Windows.RoutedEventHandler(key_button_Click);
            window.gas_button.Click   += new System.Windows.RoutedEventHandler(gas_button_Click);
            window.break_button.Click += new System.Windows.RoutedEventHandler(break_button_Click);
            fsm.ChangeBind("speed", () => window.Dispatcher.Invoke(new Action(() => window.SetSpeed(fsm.speed))));
            fsm.start += new Action(fsm_start);
        }

        void fsm_start()
        {
            window.Dispatcher.Invoke(new Action(() => { window.car_starting.Stop(); window.car_starting.Play(); }));
        }

        void break_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            fsm.BREAK_PEDAL();
        }

        void gas_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            fsm.GAS_PEDAL();
        }

        void key_button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            fsm.TURN_KEY();
        }

        public void Init()
        {
            fsm.Initiate();
        }

        public void Stop()
        {
            fsm.Terminate();
        }
    }
}
