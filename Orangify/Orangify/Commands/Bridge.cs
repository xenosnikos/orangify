using Orangify.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Orangify.Commands
{
    class Bridge : ICommand
    {
        Action<object> excecuteMethod;
        Func<object, bool> canexcecuteMethod;

        public Bridge(Action<object> excecuteMethod, Func<object, bool> canexcecuteMethod)
        {
            this.excecuteMethod = excecuteMethod;
            this.canexcecuteMethod = canexcecuteMethod;
        }





        PlayButton _playButtonModel;

        public event EventHandler CanExecuteChanged;

        public Bridge(PlayButton viewModel)
        {
            _playButtonModel = viewModel;
        }

        public static class MediaCommands { }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _playButtonModel.OnExecute();
        }
    }
}
