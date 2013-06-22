using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace FsmTester
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        Car car;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var win = new MainWindow();
            car = new Car(win);
            win.Loaded += new RoutedEventHandler(win_Loaded);
            base.Exit += new ExitEventHandler(App_Exit);
            MainWindow = win;
            win.Show();
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            car.Stop();
        }

        void win_Loaded(object sender, RoutedEventArgs e)
        {
            car.Init();
        }
    }
}
