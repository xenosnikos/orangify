using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orangify
{

    public class LibViewModel 
    {
        public ICommand LibCommand { get; set; }
        public event EventHandler CanExcecuteSong;
        public LibViewModel()
        {
            LibCommand = new Command(LoadSong, canExcecuteSong);
        }
        private bool canExcecuteSong(object parameter)
        {
            return true;
        }
        private void LoadSong(object currentSelectedSong)
        {
            Song ss = new Song();
            var currentSelectedSongPath = ss.songPath;
            Sample_BASS.BassEngine.Instance.OpenFile(currentSelectedSongPath);
        }
    }

}
