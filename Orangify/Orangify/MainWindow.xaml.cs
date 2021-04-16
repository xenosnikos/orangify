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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Sample_BASS.BassEngine engine = Sample_BASS.BassEngine.Instance;
            engine.PropertyChanged += BassEngine_PropertyChanged;
            Sample_BASS.UIHelper.Bind(engine, "CanPlay", PlayButton, Button.IsEnabledProperty);
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void BassEngine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Sample_BASS.BassEngine engine = Sample_BASS.BassEngine.Instance;
            switch (e.PropertyName)
            {
                case "FileTag":
                    if (engine.FileTag != null)
                    {
                        TagLib.Tag tag = engine.FileTag.Tag;
                        if (tag.Pictures.Length > 0)
                        {
                            using (MemoryStream albumArtworkMemStream = new MemoryStream(tag.Pictures[0].Data.Data))
                            {
                                try
                                {
                                    BitmapImage albumImage = new BitmapImage();
                                    albumImage.BeginInit();
                                    albumImage.CacheOption = BitmapCacheOption.OnLoad;
                                    albumImage.StreamSource = albumArtworkMemStream;
                                    albumImage.EndInit();
                                    //albumArtPanel.AlbumArtImage = albumImage;
                                }
                                catch (NotSupportedException)
                                {
                                   // albumArtPanel.AlbumArtImage = null;
                                    // System.NotSupportedException:
                                    // No imaging component suitable to complete this operation was found.
                                }
                                albumArtworkMemStream.Close();
                            }
                        }
                        else
                        {
                           // albumArtPanel.AlbumArtImage = null;
                        }
                    }
                    else
                    {
                        //albumArtPanel.AlbumArtImage = null;
                    }
                    break;
                case "ChannelPosition":
                    //clockDisplay.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    break;
                default:
                    // Do Nothing
                    break;
            }

        }


        private void OpenFile()
        {

            Library lib = new Library();
            Song currentSelectedSong = (Song)lib.lvSongs.SelectedItem;
            var currentSelectedSongPath = currentSelectedSong.songPath;
            Sample_BASS.BassEngine.Instance.OpenFile(currentSelectedSongPath);
               
            
        }

        private void PlayButton_Click_1(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            OpenFile();
            if (Sample_BASS.BassEngine.Instance.CanPlay)
                Sample_BASS.BassEngine.Instance.Play();
=======
            //Library lib = new Library();
            //Settings set = new Settings();

            //OpenFile();
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                //Song selectedSong = (Song)lib.lvSongs.SelectedItem;
                audioFile = new AudioFileReader(Library.);
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();

>>>>>>> Stashed changes
        }
    }
}
