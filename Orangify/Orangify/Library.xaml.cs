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
        private static Library instance;

        public static Library Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Library();
                    return instance;
                }
                return instance;
            }
        }

        private List<Song> songList;
        public List<Song> SongList
        {
            get
            {
                return songList;
            }
            set
            {
                songList = value;
                lvSongs.ItemsSource = songList;
            }

        }

        public int CurrentSongIndex { get; set; }

        public Song CurrentSong
        {
            get
            {
                return SongList[CurrentSongIndex];
            }
        }

        private Library()
        {
            try
            {
                InitializeComponent();
                Globals.ctx = new orangifyEntities1();
                SongList = Globals.ctx.Songs.ToList<Song>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing components: " + ex.Message);
            }
        }

        public void PlayNext()
        {
            CurrentSongIndex = (CurrentSongIndex + 1) % SongList.Count;
            lvSongs.SelectedItem = CurrentSong;
            PlaySong();
        }

        public void PlayPrevious()
        {
            var index = CurrentSongIndex - 1;
            if (index < 0)
            {
                index = SongList.Count - 1;
            }
            CurrentSongIndex = index;
            lvSongs.SelectedItem = CurrentSong;
            PlaySong();
        }

        private void PlaySong()
        {
            Sample_BASS.BassEngine.Instance.OpenFile(CurrentSong.songPath);
            if (Sample_BASS.BassEngine.Instance.CanPlay)
                Sample_BASS.BassEngine.Instance.Play();
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
            SongList = Globals.ctx.Songs.ToList<Song>();

            Settings.Instance.cbSettingsLanguage.SelectedItem = Settings.Instance.setLanguage;
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

        private void LibraryWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private void anotherWayToClick(object sender, RoutedEventArgs e)
        {
            CurrentSongIndex = ((System.Windows.Controls.ListView)sender).SelectedIndex;
            PlaySong();
        }

        private void clickclick(object sender, RoutedEventArgs e)
        {
            CurrentSongIndex = ((System.Windows.Controls.ListView)sender).SelectedIndex;
            PlaySong();
        }

        private void miContextDelete_Click(object sender, RoutedEventArgs e)
        {
            Song selSong = (Song)lvSongs.SelectedItem;
            Globals.ctx.Songs.Remove(selSong);
            Globals.ctx.SaveChanges();
            lvSongs.ItemsSource = (from t in Globals.ctx.Songs select t).ToList<Song>();
            lvSongs.Items.Refresh();

        }

        private void clickEdit(object sender, RoutedEventArgs e)
        {
            EditTag et = new EditTag((Song)lvSongs.SelectedItem);
            et.Owner = this;
            if (et.ShowDialog() == true)
            {


            }


        }
    }
}




