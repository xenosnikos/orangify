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

        public ICommand NextSongCommand { get; set; }

        public ICommand PreviousSongCommand { get; set; }

        public event EventHandler CanExcecuteSong;

        public PlayViewModel()
        {
            PlayCommand = new Command(PlaySong, canExcecuteSong);
            PauseCommand = new Command(PauseSong, canExcecuteSong);
            NextSongCommand = new Command(NextSong, canExcecuteSong);
            PreviousSongCommand = new Command(PreviousSong, canExcecuteSong);
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
        private void PauseSong(object currentSelectedSong)
        {
            if (Sample_BASS.BassEngine.Instance.CanPause)
                Sample_BASS.BassEngine.Instance.Pause();
        }

        private void NextSong(object currentSelectedSong)
        {
            Library.Instance.PlayNext();
        }

        private void PreviousSong(object currentSelectedSong)
        {
            Library.Instance.PlayPrevious();
        }
    }
}
