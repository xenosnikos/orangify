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
        public ICommand PauseCommand { get; set; }

        public event EventHandler CanExcecuteSong;
        
        public PlayViewModel()
        {
            PlayCommand = new Command(PlaySong, canExcecuteSong);
            PauseCommand = new Command(PauseSong, canExcecuteSong);
        }
        private bool canExcecuteSong(object parameter)
        {
            return true;
        }
        private void PlaySong(object currentSelectedSong)
        {


            if (Sample_BASS.BassEngine.Instance.CanPlay);
                Sample_BASS.BassEngine.Instance.Play();


        }
        private void PauseSong(object currentSelectedSong)
        {


            if (Sample_BASS.BassEngine.Instance.CanPause);
                Sample_BASS.BassEngine.Instance.Pause();


        }

    }
}
