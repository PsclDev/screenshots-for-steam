using BespokeFusion;
using System;
using System.Windows;

namespace Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnPowerOff_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnImportPath_Click(object sender, RoutedEventArgs e)
        {
            TxtBxImportPath.Text = GetPath();
        }

        private void BtnSteamPath_Click(object sender, RoutedEventArgs e)
        {
            TxtBxSteamPath.Text = GetPath();
        }

        private string GetPath()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;

                return path;
            }

            return "";
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            string ImpPath = TxtBxImportPath.Text;
            string SteamPath = TxtBxSteamPath.Text;
            bool deleteFiles = Convert.ToBoolean(TglBtnDelteFiles.IsChecked);

            if (ImpPath != "" && SteamPath != "")
            {
                Functions.SortScreenshots(ImpPath);
                Functions.ConvertAndExport(SteamPath, deleteFiles);
            }
            else if(ImpPath == "" && SteamPath != "")
                MaterialMessageBox.Show("You need to Fill out import Field", "Empty Field");
            else if (SteamPath == "" && ImpPath != "")
                MaterialMessageBox.Show("You need to Fill out steam Field", "Empty Field");
            else if (ImpPath == "" && SteamPath == "")
                MaterialMessageBox.Show("Both Fields must be filled ", "Empty Fields");
        }
    }
}
