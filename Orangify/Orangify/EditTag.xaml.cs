using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Orangify
{
    /// <summary>
    /// Interaction logic for EditTag.xaml
    /// </summary>
    public partial class EditTag : Window
    {
        Song currentSong { get; set; }

        public EditTag(Song song)
        {
            InitializeComponent();
            currentSong = song;
            tbTitle.Text = song.Title;
            tbArtist.Text = song.Artist.Name.ToString();
            tbAlbum.Text = song.Album.Name.ToString();
            tbYear.Text = song.YearReleased.Value.Year.ToString();
            
            
            
            imageViewer.Source = ByteArrayToBitmapImage(song.Artwork);
        }



        private void btnDialogUpdate_Click(object sender, RoutedEventArgs e)
        {
            

            currentSong.Title = tbTitle.Text;
            currentSong.Artist.Name = tbArtist.Text;
            currentSong.Album.Name = tbAlbum.Text;
            //MemoryStream ms = new MemoryStream(currentSong.Artwork);
            //byte[] songArtwork = ms.ToArray();
            //currentSong.Artwork = songArtwork;
           
            Globals.ctx.SaveChanges();
            this.Close();

        }

        private void btnDialogDone_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        byte[] currSongArtwork;
        
        public void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            // dlg.RestoreDirectory = true;

            if(dlg.ShowDialog() == true) { 
            
                try
                {
                    currSongArtwork = File.ReadAllBytes(dlg.FileName); // ex IOException
                    tbImage.Visibility = Visibility.Hidden; // hide text on the button
                    BitmapImage bitmap = ByteArrayToBitmapImage(currSongArtwork); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    System.Windows.MessageBox.Show(this, ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void tbTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDialogUpdate.IsEnabled = true;
        }

        private void tbArtist_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDialogUpdate.IsEnabled = true;
        }

        private void tbAlbum_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDialogUpdate.IsEnabled = true;
        }

        private void tbYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDialogUpdate.IsEnabled = true;
        }
    }
}
