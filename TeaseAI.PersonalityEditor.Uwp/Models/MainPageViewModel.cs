using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace TeaseAI.PersonalityEditor.Models
{
    public class MainPageViewModel : BindableBase.BindableBase
    {
        public MainPageViewModel()
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

        public void LoadedEvent(object s, object e) => SelectBaseFolder();

        public void PersonalityChangedEvent(object s, object e) => PersonalityChanged();

        public void ScriptClickedEvent(object s, ItemClickEventArgs e) => ScriptClicked((ScriptMetaData)e.ClickedItem);

        public async void SaveScriptEvent(object s, object e)
        {
            var saveScript = Result.Ok()
                .Ensure(() => !string.IsNullOrWhiteSpace(CurrentScript?.MetaData?.Key), "No script is loaded")
                .OnSuccess(() =>
                {
                    CurrentScript = new Script(CurrentScript.MetaData, CurrentScriptText.Split(Environment.NewLine).ToList());
                    return _scriptService.Save(CurrentScript);
                });
            if (saveScript.IsFailure)
            {
                await _notifyUser.ModalMessageAsync(saveScript.Error.Message);
            }
        }

        public async void TestScriptEvent(object s, object e) => await TestScript(CurrentScript);
        #endregion

        public ObservableCollection<Personality> Personalities => _personalities ?? (_personalities = new ObservableCollection<Personality>());
        public Personality SelectedPersonality
        {
            get => _selectedPersonality;
            set => SetPropertyValue(ref _selectedPersonality, value);
        }

        public Script CurrentScript
        {
            get => _currentScript;
            set => SetPropertyValue(ref _currentScript, value);
        }

        public string CurrentScriptText
        {
            get => _currentScriptText;
            set => SetPropertyValue(ref _currentScriptText, value);
        }

        public ObservableCollection<ScriptMetaData> StartupScripts
        {
            get => _startupScripts ?? (_startupScripts = new ObservableCollection<ScriptMetaData>());
            set => SetPropertyValue(ref _startupScripts, value);
        }

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
            allScripts.Where(smd => smd.SessionPhase == SessionPhase.Start).ToList()
                .ForEach(smd => StartupScripts.Add(smd));
            CurrentScript = new Script(new ScriptMetaData(), new List<string>());
            CurrentScriptText = string.Empty;
        }

        private void ScriptClicked(ScriptMetaData clickedItem)
        {
            CurrentScript = _scriptService.GetScript(clickedItem).GetResultOrDefault(new Script(new ScriptMetaData(), new List<string>()));
            CurrentScriptText = string.Join(Environment.NewLine, CurrentScript.Lines.ToList());
        }

        private async Task TestScript(Script currentScript)
        {
            var errors = new List<string>();
            var commandProcessors = _getCommandProcessorsService.CreateCommandProcessors();
            var lineNumber = 1;

            foreach (var line in currentScript.Lines)
            {
                foreach (var command in commandProcessors.Keys)
                {
                    if (commandProcessors[command].IsRelevant(line))
                    {
                        var processCommand = commandProcessors[command].ParseCommand(currentScript, _selectedPersonality.Id, line);
                        if (processCommand.IsFailure)
                        {
                            errors.Add(lineNumber.ToString() + " : " + processCommand.Error.Message);
                        }
                    }
                }
                lineNumber++;
            }

            var errorMessage = string.Join(Environment.NewLine, errors);
            await _notifyUser.ModalMessageAsync(errorMessage);
        }
        #endregion

        private XamlUICommand CreateCommand(TypedEventHandler<XamlUICommand, ExecuteRequestedEventArgs> commandAction)
        {
            var command = new XamlUICommand();
            command.ExecuteRequested += commandAction;
            return command;
        }

        private ObservableCollection<Personality> _personalities;
        private Personality _selectedPersonality;
        private ObservableCollection<ScriptMetaData> _startupScripts;
        private string _currentScriptText;
        private Script _currentScript;
        private readonly IPersonalityService _personalityService;
        private readonly IScriptAccessor _scriptService;

        public IGetCommandProcessorsService _getCommandProcessorsService { get; }

        private readonly INotifyUser _notifyUser;
    }
}
