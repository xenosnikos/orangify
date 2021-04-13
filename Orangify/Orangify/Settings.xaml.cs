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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        List<string> pathList = new List<string>();
        public Settings()
        {
            InitializeComponent();
            //TODO PHIL: Make pathlist persistent
            //lvSettingsPaths.ItemsSource = pathList;
        }

        private void SettingsWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void SettingsAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //TODO: Implement library folder selection
        private void SettingsAddFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                //TODO: create object for songs so that it can be added to the listview in the settings menu
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    lvSettingsPaths.Items.Add(dialog.SelectedPath);
                    lvSettingsPaths.Items.Refresh();
                }
            }

        }
    }
}
