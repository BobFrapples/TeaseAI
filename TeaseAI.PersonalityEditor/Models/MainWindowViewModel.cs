using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;

namespace TeaseAI.PersonalityEditor.Models
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            _notifyUser = ServiceFactory.CreateNotifyUser();
            _personalityService = ServiceFactory.CreatePersonalityService();
            _scriptService = ServiceFactory.CreateScriptAccessor();
            _getCommandProcessorsService = ServiceFactory.CreateGetCommandProcessorsService();
        }

        #region Commands and events
        public void CreateDommePersonalityEvent(object s, object e)
        { }

        public void ExportDommePersonalityEvent(object s, object e)
        { }

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new DelegateCommand(SelectBaseFolder));
        public ICommand PersonalityChangedCommand => _personalityChangedCommand ?? (_personalityChangedCommand = new DelegateCommand(PersonalityChanged));
        public DelegateCommand<ScriptMetaData> ScriptClickedCommand => _scriptClickedCommand ?? (_scriptClickedCommand = new DelegateCommand<ScriptMetaData>(ScriptClicked));
        public ICommand TestScriptCommand => _testScriptCommand ?? (_testScriptCommand = new DelegateCommand(TestScript));

        public async void SaveScriptEvent(object s, object e)
        {
            var saveScript = Result.Ok()
                .Ensure(() => !string.IsNullOrWhiteSpace(CurrentScript?.MetaData?.Key), "No script is loaded")
                .OnSuccess(() =>
                {
                    CurrentScript = new Script(CurrentScript.MetaData, CurrentScriptText.Split(Environment.NewLine.ToCharArray()).ToList());
                    return _scriptService.Save(CurrentScript);
                });
            if (saveScript.IsFailure)
            {
                await _notifyUser.ModalMessageAsync(saveScript.Error.Message);
            }
        }
        #endregion

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

        public ObservableCollection<ScriptMetaData> StartupScripts => _startupScripts ?? (_startupScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> ModulesScripts => _modulesScripts ?? (_modulesScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> LinkScripts => _linkScripts ?? (_linkScripts = new ObservableCollection<ScriptMetaData>());
        public ObservableCollection<ScriptMetaData> EndScripts => _endScripts ?? (_endScripts = new ObservableCollection<ScriptMetaData>());

        #region Command Actions
        private void SelectBaseFolder()
        {
            var personalities = _personalityService.GetAllPersonalities();
            Personalities.Clear();
            personalities.ForEach(p => Personalities.Add(p));
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

        private void ScriptClicked(ScriptMetaData scriptMetaData)
        {
            CurrentScript = _scriptService.GetScript(scriptMetaData)
                .GetResultOrDefault(new Script(new ScriptMetaData(), new List<string>()));
            CurrentScriptText = string.Join(Environment.NewLine, CurrentScript.Lines.ToList());
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
        private readonly IPersonalityService _personalityService;
        private readonly IScriptAccessor _scriptService;

        public IGetCommandProcessorsService _getCommandProcessorsService { get; }

        private readonly INotifyUser _notifyUser;
    }
}
