using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeaseAI.Common.Data;
using TeaseAI.PersonalityEditor.ViewModels;

namespace TeaseAI.PersonalityEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainWindowViewModel => (MainWindowViewModel)DataContext;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void ScriptPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var smd = (ScriptMetaData)((StackPanel)sender).DataContext;

            MainWindowViewModel.ScriptClickedCommand.Execute(smd);
        }
    }
}
