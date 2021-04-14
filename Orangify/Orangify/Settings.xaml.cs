using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    /// 
    public partial class Settings : Window
    {        
        public List<string> pathList = new List<string>();
        Sample_BASS.BassEngine engine;
        public Settings()
        {
            InitializeComponent();

            lvSettingsPaths.ItemsSource = pathList;

        }

        private void SettingsWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SettingsAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            // engine.OpenFile(dial);
            for (int i = 0; i < pathList.Count; i++)
            {
                Library lib = new Library();
                var tfile = TagLib.File.Create(pathList[i]);
                string title = tfile.Tag.Title;
                string artist = tfile.Tag.FirstAlbumArtist;
                string album = tfile.Tag.Album;
                TimeSpan length = tfile.Properties.Duration;
                Console.WriteLine("Title: {0}, duration: {1}", title, length);
                Artist artistObj = new Artist { Name = artist };
                Album albumObj = new Album { Name = album };
                long yearReleased = tfile.Tag.Year;
                
                DateTime dt = DateTime.FromBinary(yearReleased);
                Song song = new Song { Title = title,Artist= artistObj, Album = albumObj, Length = length, YearReleased=dt };
                lib.songList.Add(song);
                tfile.Save();
                Globals.ctx.Songs.Add(song);
                Globals.ctx.SaveChanges();

            }


            lvSettingsPaths.Items.Refresh();
            this.DialogResult = true;
            //this.Close();
        }

        public void scanForSongFiles()
        {
            List<string> mediaExtensions = new List<string> { "mp3", "mp4" };
            List<string> filesFound = new List<string>();

            void DirSearch(string sDir)
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, "*.*"))
                    {
                        if (mediaExtensions.Contains(System.IO.Path.GetExtension(f).ToLower()))
                            filesFound.Add(f);
                    }
                    DirSearch(d);
                }
            }
            
        }
        //TODO: Implement library folder selection
        private void SettingsAddFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                //TODO: create object for songs so that it can be added to the listview in the settings menu
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        pathList.Add(dialog.FileName);

                        lvSettingsPaths.Items.Refresh();
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine("Error loading path list: " + ex.Message);
                    }
                }
            }

        }
    }
}
