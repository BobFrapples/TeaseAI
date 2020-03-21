using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TeaseAI.Common.Data;
using TeaseAI.PersonalityEditor.ViewModels;
using TeaseAI.PersonalityEditor.Views;

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
            var smd = (ScriptMetaData)((ScriptListItemView)sender).DataContext;

            MainWindowViewModel.ScriptClickedCommand.Execute(smd);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var command = (string)((ListView)sender).SelectedItem;

            MainWindowViewModel.ScriptCommandClickedCommand.Execute(command);
        }
    }
}
