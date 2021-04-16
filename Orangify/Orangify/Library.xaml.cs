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
        public string loadSelected()
        {
            var currentSelectedSong = (Song)lvSongs.SelectedItem;
            return currentSelectedSong.songPath;
        }

        Settings set = new Settings();
        public List<Song> songList = new List<Song>();
<<<<<<< Updated upstream
        public  Library()
=======
        internal int selectedIndex = 0;

        private Song _selectedSong;

        public Song SelectedSong
        {
            get
            {
                return _selectedSong;
            }
            set
            {
                _selectedSong = (Song)lvSongs.SelectedItem;
                _selectedSong = value;
            }
        }
        public Library()
>>>>>>> Stashed changes
        {
            try
            {
                InitializeComponent();
                Globals.ctx = new orangifyEntities1();
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
                settings.lvSettingsPaths.ItemsSource = settings.pathList.ToList();
                settings.lvSettingsPaths.Items.Refresh();
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


        private void anotherWayToClick(object sender, RoutedEventArgs e)
        {

<<<<<<< Updated upstream

            Sample_BASS.BassEngine.Instance.OpenFile(loadSelected());





=======
            if (lvSongs.SelectedValue == null) // (lvToDoList.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show(this, "Please add an item", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Song song = (Song)lvSongs.SelectedItem;
            songPathSelected = song.songPath;
>>>>>>> Stashed changes
        }

        private void clickclick(object sender, RoutedEventArgs e)
        {
            Sample_BASS.BassEngine.Instance.OpenFile(loadSelected());
            if (Sample_BASS.BassEngine.Instance.CanPlay)
                Sample_BASS.BassEngine.Instance.Play();
        }
    }
}




