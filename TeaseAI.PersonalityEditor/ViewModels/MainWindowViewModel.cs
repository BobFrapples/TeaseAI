using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.PersonalityEditor.Views;

namespace TeaseAI.PersonalityEditor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            _notifyUser = ServiceFactory.CreateNotifyUser();
            _personalityService = ServiceFactory.CreatePersonalityService();
            _scriptService = ServiceFactory.CreateScriptAccessor();
            _getCommandProcessorsService = ServiceFactory.CreateGetCommandProcessorsService();
            _getCommandInformationService = ServiceFactory.CreateGetCommandInformationService();
        }

        #region Commands and events
        public ICommand ExportPersonalityCommand => _exportPersonalityCommand ?? (_exportPersonalityCommand = new DelegateCommand(ExportPersonality));
        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));
        public ICommand NewPersonalityCommand => _newPersonalityCommand ?? (_newPersonalityCommand = new DelegateCommand(NewPersonality));
        public ICommand PersonalityChangedCommand => _personalityChangedCommand ?? (_personalityChangedCommand = new DelegateCommand(PersonalityChanged));
        public ICommand TestScriptCommand => _testScriptCommand ?? (_testScriptCommand = new DelegateCommand(TestScript));
        public ICommand SaveScriptCommand => _saveScriptCommand ?? (_saveScriptCommand = new DelegateCommand(SaveScript));
        public DelegateCommand<ScriptMetaData> ScriptClickedCommand => _scriptClickedCommand ?? (_scriptClickedCommand = new DelegateCommand<ScriptMetaData>(ScriptClicked));
        public DelegateCommand<string> ScriptCommandClickedCommand => _scriptCommandClickedCommand ?? (_scriptCommandClickedCommand = new DelegateCommand<string>(ScriptCommandClicked));

        public ICommand OpenSettingsCommand => _openSettingsCommand ?? (_openSettingsCommand = new DelegateCommand(OpenSettings));
        #endregion

        #region Properties
        public ObservableCollection<Personality> Personalities => _personalities ?? (_personalities = new ObservableCollection<Personality>());
        public Personality SelectedPersonality
        {
            get => _selectedPersonality;
            set => SetProperty(ref _selectedPersonality, value);
        }

        public Script CurrentScript
        {
            get => _currentScript;
            set => SetProperty(ref _currentScript, value);
        }

        public string CurrentScriptText
        {
            get => _currentScriptText;
            set => SetProperty(ref _currentScriptText, value);
        }

        public ObservableCollection<string> ScriptCommands => _scriptCommands ?? (_scriptCommands = new ObservableCollection<string>());

        public ObservableCollection<ScriptMetaData> StartupScripts => _startupScripts ?? (_startupScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> ModulesScripts => _modulesScripts ?? (_modulesScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> LinkScripts => _linkScripts ?? (_linkScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> EndScripts => _endScripts ?? (_endScripts = new ObservableCollection<ScriptMetaData>());

        public string CurrentScriptCommandHeader
        {
            get => _currentScriptCommandHeader;
            set => SetProperty(ref _currentScriptCommandHeader, value);
        }

        public string CurrentScriptCommandDescription
        {
            get => _currentScriptCommandDescription;
            set => SetProperty(ref _currentScriptCommandDescription, value);
        }

        public string CurrentScriptCommandExample
        {
            get => _currentScriptCommandExample;
            set => SetProperty(ref _currentScriptCommandExample, value);
        }

        public ScriptCommandInformation CurrentScriptCommandInformation
        {
            get => _currentScriptCommandInformation;
            set => SetProperty(ref _currentScriptCommandInformation, value);
        }
        #endregion

        #region Command Actions
        private void ExportPersonality()
        {
            var openFileDialog = new SaveFileDialog();
            openFileDialog.FileName = SelectedPersonality.Name + ".zip";
            if (!openFileDialog.ShowDialog().GetValueOrDefault())
                return;
            var sourceDirectoryName = SelectedPersonality.Id;
            ZipFile.CreateFromDirectory(sourceDirectoryName, openFileDialog.FileName);
        }

        private void NewPersonality()
        {
            throw new NotImplementedException();
        }

        private void OpenSettings()
        {
            var settingsDialog = new SettingsView();
            settingsDialog.ShowDialog();
        }

        private void PersonalityChanged()
        {
            var allScripts = _scriptService.GetAllScripts(SelectedPersonality.Name).GetResultOrDefault(new List<ScriptMetaData>());
            StartupScripts.Clear();
            StartupScripts.AddRange(allScripts.Where(smd => smd.SessionPhase == SessionPhase.Start));

            ModulesScripts.Clear();
            ModulesScripts.AddRange(allScripts.Where(smd => smd.SessionPhase == SessionPhase.Modules));

            LinkScripts.Clear();
            LinkScripts.AddRange(allScripts.Where(smd => smd.SessionPhase == SessionPhase.Link));

            EndScripts.Clear();
            EndScripts.AddRange(allScripts.Where(smd => smd.SessionPhase == SessionPhase.End));

            CurrentScript = new Script(new ScriptMetaData(), new List<string>());
            CurrentScriptText = string.Empty;
        }

        private async void SaveScript()
        {
            var saveScript = Result.Ok()
                .Ensure(() => !string.IsNullOrWhiteSpace(CurrentScript?.MetaData?.Key), "No script is loaded")
                .OnSuccess(() =>
                {
                    CurrentScript = new Script(CurrentScript.MetaData, CurrentScriptText.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList());
                    return _scriptService.Save(CurrentScript);
                });
            if (saveScript.IsFailure)
            {
                await _notifyUser.ModalMessageAsync(saveScript.Error.Message);
            }
        }

        private void ScriptClicked(ScriptMetaData scriptMetaData)
        {
            CurrentScript = _scriptService.GetScript(scriptMetaData)
                .GetResultOrDefault(new Script(new ScriptMetaData(), new List<string>()));
            CurrentScriptText = string.Join(Environment.NewLine, CurrentScript.Lines.ToList());
        }

        private void ScriptCommandClicked(string command)
        {
            var getCommandInformation = _getCommandInformationService.GetCommandInformation(command);
            if (getCommandInformation.IsSuccess)
                CurrentScriptCommandInformation = getCommandInformation.Value;
            else
            {
                CurrentScriptCommandInformation = new ScriptCommandInformation
                {
                    Command = command,
                    Description = getCommandInformation.Error.Message,
                    Example = string.Empty,
                };
            }
        }

        private void Loaded()
        {
            var personalities = _personalityService.GetAllPersonalities();
            Personalities.Clear();
            personalities.ForEach(p => Personalities.Add(p));

            var commandProcessors = _getCommandProcessorsService.CreateCommandProcessors();
            var commands = commandProcessors.Keys.ToList().OrderBy(cmd => cmd);
            ScriptCommands.Clear();
            ScriptCommands.AddRange(commands);

        }

        private async void TestScript()
        {
            var currentScript = CurrentScript;
            var errors = new List<string>();
            var commandProcessors = _getCommandProcessorsService.CreateCommandProcessors();
            var lineNumber = 1;

            foreach (var line in currentScript.Lines)
            {
                var workingLine = line;
                foreach (var command in commandProcessors.Keys)
                {
                    if (commandProcessors[command].IsRelevant(workingLine))
                    {
                        var processCommand = commandProcessors[command].ParseCommand(currentScript, _selectedPersonality.Id, workingLine);
                        if (processCommand.IsFailure)
                        {
                            errors.Add(lineNumber.ToString() + " : " + processCommand.Error.Message);
                        }
                        else
                        {
                            workingLine = commandProcessors[command].DeleteCommandFrom(workingLine);
                        }
                    }
                }

                if (line.Contains("@"))
                {
                    var possibleCommands = workingLine.Split(' ').Where(l => l.StartsWith("@")).ToList();
                    foreach (var uc in possibleCommands)
                    {
                        errors.Add(lineNumber.ToString() + " : " + uc + " may be an unknown command");
                    }
                }
                lineNumber++;
            }

            var errorMessage = string.Join(Environment.NewLine, errors);
            if (string.IsNullOrWhiteSpace(errorMessage))
                await _notifyUser.ModalMessageAsync("Script has no known errors");
            else
                await _notifyUser.ModalMessageAsync(errorMessage);
        }
        #endregion

        #region Private fields
        private ObservableCollection<Personality> _personalities;
        private Personality _selectedPersonality;
        private ObservableCollection<ScriptMetaData> _startupScripts;
        private ObservableCollection<ScriptMetaData> _modulesScripts;
        private ObservableCollection<ScriptMetaData> _linkScripts;
        private ObservableCollection<ScriptMetaData> _endScripts;
        private string _currentScriptText;
        private Script _currentScript;

        private DelegateCommand _loadedCommand;
        private DelegateCommand _personalityChangedCommand;
        private DelegateCommand<ScriptMetaData> _scriptClickedCommand;
        private DelegateCommand _testScriptCommand;
        private DelegateCommand _exportPersonalityCommand;
        private DelegateCommand _newPersonalityCommand;
        private DelegateCommand _saveScriptCommand;
        private DelegateCommand _openSettingsCommand;
        private ObservableCollection<string> _scriptCommands;
        private DelegateCommand<string> _scriptCommandClickedCommand;
        private string _currentScriptCommandDescription;
        private string _currentScriptCommandExample;
        private string _currentScriptCommandHeader;
        private ScriptCommandInformation _currentScriptCommandInformation;
        private readonly IPersonalityService _personalityService;
        private readonly IScriptAccessor _scriptService;
        private readonly IGetCommandProcessorsService _getCommandProcessorsService;
        private readonly IGetCommandInformationAccessor _getCommandInformationService;
        private readonly IConfigurationAccessor _settingsService;
        private readonly INotifyUser _notifyUser;
        #endregion
    }
}
