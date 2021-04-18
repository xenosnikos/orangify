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

        public Settings()
        {
            try
            {

                InitializeComponent();

                lvSettingsPaths.ItemsSource = pathList;
            }
            catch (SystemException ex)
            {
                System.Windows.MessageBox.Show("something f'ed up" + ex.Message);
            }
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
           
            Artist existingArtist;
            Album existingAlbum;
            Library lib = new Library();
            for (int i = 0; i < pathList.Count; i++)
            {
                
                var tfile = TagLib.File.Create(pathList[i]);
                string title = tfile.Tag.Title;

                string artist = tfile.Tag.Performers[0];
               
                using (var context = new orangifyEntities1() )
                {
                     existingArtist = context.Artists.Where<Artist>(S => Name == artist)as Artist;
                    
                    if (existingArtist == null)
                    {
                        existingArtist = new Artist { Name = artist };
                    }
                    else
                    {
                        return;
                    }

                }

                string album = tfile.Tag.Album;
                using (var context = new orangifyEntities1())
                {
                    existingAlbum = context.Albums.Where<Album>(S => Name == album) as Album;

                    if (existingAlbum == null)
                    {
                        existingAlbum = new Album { Name = artist };
                    }
                    else
                    {
                        return;
                    }

                }

                string filePath = pathList[i];
                TimeSpan length = tfile.Properties.Duration;
                Console.WriteLine("Title: {0}, duration: {1}", title, length);
               
                Album albumObj = new Album { Name = album };
                
               
                long yearReleased = tfile.Tag.Year;
                
                DateTime dt = DateTime.FromBinary(yearReleased);
            
                Song song = new Song { Title = title, Artist = existingArtist, Album = existingAlbum, Length = length, YearReleased = dt, songPath = filePath };
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
        private void SettingsAddFileBtn_Click(object sender, RoutedEventArgs e)
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

        private void SettingsAddFolderBtn_Click(object sender, RoutedEventArgs e)
        {

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");

                    foreach (string s in files)
                    {
                        pathList.Add(s);
                        lvSettingsPaths.Items.Refresh();
                    }
                }
            }
        }

        private void SettingsXBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
