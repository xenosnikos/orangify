using System;
using System.Collections.Generic;
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
           Sample_BASS.UIHelper.Bind(engine, "CanPlay", PlayButton, Button.IsEnabledProperty);
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
            Sample_BASS.BassEngine.Instance.OpenFile(currentSelectedSongPath);
               
            
        }

        private void PlayButton_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFile();
            if (Sample_BASS.BassEngine.Instance.CanPlay)
                Sample_BASS.BassEngine.Instance.Play();
        }
    }
}
