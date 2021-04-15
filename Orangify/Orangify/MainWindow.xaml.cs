using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Orangify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        private void OpenFile()
        {

            Library lib = new Library();
            Song currentSelectedSong = (Song)lib.lvSongs.SelectedItem;
            var currentSelectedSongPath = currentSelectedSong.songPath;


        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private void PlayButton_Click_1(object sender, RoutedEventArgs e)
        {
            Library lib = new Library();
            Settings set = new Settings();

            //OpenFile();
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader();
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();

        }
    }
}
