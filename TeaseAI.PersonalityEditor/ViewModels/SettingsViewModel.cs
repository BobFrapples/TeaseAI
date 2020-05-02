using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.PersonalityEditor.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        public SettingsViewModel()
        {
            _configurationSettings = ServiceFactory.CreateConfigurationAccessor();
        }

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));

        public ICommand PickPersonalityDirectoryCommand => _pickPersonalityDirectoryCommand ?? (_pickPersonalityDirectoryCommand = new DelegateCommand(PickPersonalityDirectory));

        public string PersonalityDirectory
        {
            get => _personalityDirectory;
            set => SetProperty(ref _personalityDirectory, value);
        }

        private void Loaded()
        {
            var applicationConfiguration = _configurationSettings.GetApplicationConfiguration();
            PersonalityDirectory = applicationConfiguration.BaseDataFolder;
        }

        private void PickPersonalityDirectory()
        {

            using (var dialog = new FolderBrowserDialog())
            {
                var result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                    return;
                PersonalityDirectory = dialog.SelectedPath;

                var applicationConfiguration = _configurationSettings.GetApplicationConfiguration();
                applicationConfiguration.BaseDataFolder = PersonalityDirectory;
                _configurationSettings.SaveApplicationConfiguration(applicationConfiguration);
            }
        }

        private DelegateCommand _pickPersonalityDirectoryCommand;
        private string _personalityDirectory;
        private IConfigurationAccessor _configurationSettings;
        private DelegateCommand _loadedCommand;
    }
}
