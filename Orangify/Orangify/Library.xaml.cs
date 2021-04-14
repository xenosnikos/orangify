using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for Library.xaml
    /// </summary>
    public partial class Library : Window
    {

        Settings set = new Settings();
        public List<Song> songList = new List<Song>();
        public Library()
        {
            try
            {
                InitializeComponent();
                Globals.ctx = new orangifyEntities();
                lvSongs.ItemsSource = Globals.ctx.Songs.ToList<Song>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing components: " + ex.Message);
            }
        }

        private void miSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Owner = this;
            //settings.ShowDialog();

            if (settings.ShowDialog() == true)
            {
                Settings set = new Settings();
                set.scanForSongFiles();
            }
            lvSongs.ItemsSource = Globals.ctx.Songs.ToList<Song>();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to quit?", "Really exit??", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Environment.Exit(0);
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        private void Library_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
