﻿using System;
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
            localization();

            Equalizer EQWindow = new Equalizer();
            EQWindow.Top = 20;
            EQWindow.Left = 730;
            EQWindow.Show();

            Library LibraryWindow = new Library();
            LibraryWindow.Top = 500;
            LibraryWindow.Left = 20;
            LibraryWindow.Show();
        }

        private void localization()
        {
            //TODO: Implement localization
            string language;
            
        }
    }
}
