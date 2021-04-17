using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for Equalizer.xaml
    /// </summary>
    public partial class Equalizer : Window
    {
        public Equalizer()
        {
            InitializeComponent();
        }

        private void EQWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Calculate the volume that's being set
            double newVolume = ushort.MaxValue * e.NewValue / 10.0;

            uint v = ((uint)newVolume) & 0xffff;
            uint vAll = v | (v << 16);

            // Set the volume
            int retVal = NativeMethods.WaveOutSetVolume(IntPtr.Zero, vAll);

            Debug.WriteLine(retVal);
        }

        static class NativeMethods
        {
            [DllImport("winmm.dll", EntryPoint = "waveOutSetVolume")]
            public static extern int WaveOutSetVolume(IntPtr hwo, uint dwVolume);

        }
    }
}
