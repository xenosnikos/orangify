using Orangify.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Orangify.ViewModel
{
    class PlayButton 
    {
        public Bridge playButton { get; set; }

        public PlayButton()
        {
            playButton = new Bridge(this);
        }

        public void OnExecute()
        {
            MessageBox.Show("Song is playing, BITCHES!");
        }
    }
}
