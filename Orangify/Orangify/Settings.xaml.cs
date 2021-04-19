using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    /// 
    public partial class Settings : Window
    {

        private static Settings instance;



        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Settings();
                    return instance;
                }
                return instance;
            }
        }

        public List<string> pathList = new List<string>();
        public string setLanguage = "";

        public Settings()
        {

            try
            {

                InitializeComponent();
                Settings main;

                lvSettingsPaths.ItemsSource = pathList;
                cbSettingsLanguage.SelectedItem = setLanguage;
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

            Library lib = Library.Instance;
            for (int i = 0; i < pathList.Count; i++)
            {
                var tfile = TagLib.File.Create(pathList[i]);
                string title = tfile.Tag.Title;
                string artist;
                try
                {
                    artist = tfile.Tag.Performers[0];
                }
                catch (IndexOutOfRangeException ex)
                {
                    System.Windows.Forms.MessageBox.Show("There is a problem with that file");
                    this.Close();
                    return;
                }
                using (var context = new orangifyEntities1())
                {
                    existingArtist = context.Artists.Where<Artist>(S => Name == artist) as Artist;

                    if (existingArtist == null)
                    {
                        existingArtist = new Artist { Name = artist };
                    }


                }

                string album = tfile.Tag.Album;
                using (var context = new orangifyEntities1())
                {
                    existingAlbum = context.Albums.Where<Album>(S => Name == album) as Album;

                    if (existingAlbum == null)
                    {
                        existingAlbum = new Album { Name = album };
                    }


                }

                string filePath = pathList[i];
                TimeSpan length = tfile.Properties.Duration;
                Console.WriteLine("Title: {0}, duration: {1}", title, length);

                Album albumObj = new Album { Name = album };
                byte[] songArtwork;

                try
                {
                    MemoryStream ms = new MemoryStream(tfile.Tag.Pictures[0].Data.Data);
                    songArtwork = ms.ToArray();
                }
                catch (IndexOutOfRangeException ex)
                {
                    songArtwork = null;
                }





                long yearReleased = tfile.Tag.Year;



                DateTime dt = DateTime.FromBinary(yearReleased);

                Song song = new Song { Title = title, Artist = existingArtist, Album = existingAlbum, Length = length, YearReleased = dt, songPath = filePath, Artwork = songArtwork };

                tfile.Save();
                Globals.ctx.Songs.Add(song);
                Globals.ctx.SaveChanges();
                cbSettingsLanguage.SelectedValue = setLanguage;

                lvSettingsPaths.Items.Refresh();
                this.DialogResult = true;
                //this.Close();
            }
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



        private void cbSettingsLanguage_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (cbSettingsLanguage.SelectedValue.ToString() == "Francais")
            {
                setLanguage = "Francais";
                cbSettingsLanguage.SelectedValue = "Francais";

                lblSettingsTitle.Content = "Paramètres";
                lblSettingsLanguage.Content = "Langue";
                lblSettingsLibrarySource.Content = "Contenu";
                btSettingsAddFolder.Content = "Ajouter Dossier";
                btSettingsAddSong.Content = "Ajouter Chanson";
                btSettingsAccept.Content = "Accepter";

                Library.Instance.colSong.Header = "Chanson";
                Library.Instance.colArtist.Header = "Artiste";
                Library.Instance.colLength.Header = "Durée";
                Library.Instance.colYear.Header = "Année";
                Library.Instance.miFile.Header = "Fichier";
                Library.Instance.miSettings.Header = "Paramètres";
                Library.Instance.miExit.Header = "Quitter";


            }
            if (cbSettingsLanguage.SelectedValue.ToString() == "English")
            {
                setLanguage = "English";
                cbSettingsLanguage.SelectedValue = "English";

                lblSettingsTitle.Content = "Settings";
                lblSettingsLanguage.Content = "Language";
                lblSettingsLibrarySource.Content = "Library Source";
                btSettingsAddFolder.Content = "Add Folder to Library";
                btSettingsAddSong.Content = "Add song to Library";
                btSettingsAccept.Content = "Accept";

                Library.Instance.colSong.Header = "Song";
                Library.Instance.colArtist.Header = "Artist";
                Library.Instance.colLength.Header = "Length";
                Library.Instance.colYear.Header = "Year";
                Library.Instance.miFile.Header = "File";
                Library.Instance.miSettings.Header = "Settings";
                Library.Instance.miExit.Header = "Quit";


            }

            cbSettingsLanguage.SelectedValue = setLanguage;


        }
    }
}
