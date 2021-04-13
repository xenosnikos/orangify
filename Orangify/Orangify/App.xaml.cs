using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Equalizer equalizer = new Equalizer();
            Library library = new Library();

            try
            {
                equalizer.Top = MainWindow.Top;
                equalizer.Show();
                library.Top = MainWindow.Top;
                library.Show();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Error initializing main windows: " + ex.Message);
            }
        }
    }
}
