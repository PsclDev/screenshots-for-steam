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

            Functions.SortScreenshots(ImpPath);
            Functions.ConvertAndExport(SteamPath);
        }
    }
}
