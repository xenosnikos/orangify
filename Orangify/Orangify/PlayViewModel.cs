using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orangify
{
    public class PlayViewModel
    {
        public ICommand PlayCommand { get; set; }
        
        public event EventHandler CanExcecuteSong;
        
        public PlayViewModel()
        {
            PlayCommand = new Command(PlaySong, canExcecuteSong);
        }
        private bool canExcecuteSong(object parameter)
        {
            return true;
        }
        private void PlaySong(object currentSelectedSong)
        {


            if (Sample_BASS.BassEngine.Instance.CanPlay)
                Sample_BASS.BassEngine.Instance.Play();


        }

    }
}
