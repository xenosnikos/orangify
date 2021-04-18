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
using System.Windows.Threading;
using WPFSoundVisualizationLib;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private bool userIsDraggingSlider = false;

        public MainWindow()
        {

            try
            {
                InitializeComponent();
                DispatcherTimer timer = new DispatcherTimer();
                Sample_BASS.BassEngine engine = Sample_BASS.BassEngine.Instance;
                engine.PropertyChanged += BassEngine_PropertyChanged;

                //Audio Controls
                Sample_BASS.UIHelper.Bind(engine, "CanPlay", PlayButton, Button.IsEnabledProperty);

                Sample_BASS.UIHelper.Bind(engine, "CanPause", PauseButton, Button.IsEnabledProperty);

                //Dynamic 
<<<<<<< Updated upstream
                

=======
>>>>>>> Stashed changes
                spectrumAnalyzer.RegisterSoundPlayer(engine);
                waveformTimeline.RegisterSoundPlayer(engine);

            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Fatal error: Database connection failed:\n" + ex.Message);

            }

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
                        string songStringTitle = engine.FileTag.Tag.Title.ToString();
                        string songStringAlbumArtist = $"{ engine.FileTag.Tag.Album.ToString()},{engine.FileTag.Tag.Performers[0].ToString()}";
                        lblTitle.Content = songStringTitle;
                        lblArtistAlbum.Content = songStringAlbumArtist;
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
                    clockDisplay.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    break;
                default:
                    // Do Nothing
                    break;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void sliProgress_ValueChanged(object sender, RoutedEventArgs e)
        {

        }

        private void sliProgress_DragStarted(object sender, RoutedEventArgs e)
        {

        }
        private void sliProgress_DragCompleted(object sender, RoutedEventArgs e)
        {

        }

        private void PlayViewModel_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }

    }
}
