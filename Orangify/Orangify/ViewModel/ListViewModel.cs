using Orangify.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Orangify.ViewModel
{
    class ListViewModel
    {
        public Bridge listView { get; set; }

        public ListViewModel()
        {
            listView = new Bridge(this);
        }

        public void OnExecute()
        {
            MessageBox.Show("Song is playing, BITCHES!");
        }
    }
}
