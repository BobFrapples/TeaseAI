using System;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.PersonalityEditor.Services;
using TeaseAI.Services;
using TeaseAI.Services.Accessors;

namespace TeaseAI.PersonalityEditor
{
    public static class ServiceFactory
    {
        public static INotifyUser CreateNotifyUser() => new NotifyUser();

        public static IConfigurationAccessor CreateConfigurationAccessor() => new ConfigurationAccessor();

        public static IPersonalityService CreatePersonalityService() => new PersonalityService(CreatePathsAccessor());

        public static IScriptAccessor CreateScriptAccessor() => new ScriptAccessor(CreatePathsAccessor(), CreateCldAccessor());

        public static ICldAccessor CreateCldAccessor() => new CldAccessor();

        public static IGetCommandProcessorsService CreateGetCommandProcessorsService()
        {
            return new GetCommandProcessorsService(
                CreateScriptAccessor(),
                CreateFlagAccessor(),
                CreateLineService(),
                CreateImageAccessor(),
                CreateVideoAccessor(),
                CreateVariableAccessor(),
                CreateTauntAccessor(),
                CreateConfigurationAccessor(),
                CreateRandomNumberService(),
                CreateNotifyUser(),
                CreateSettingsAccessor(),
                CreatePathsAccessor(),
                CreateBookmarkService()
            );
        }

        internal static IGetCommandInformationAccessor CreateGetCommandInformationService() => new GetCommandInformationAccessor();

        private static IBookmarkService CreateBookmarkService() => new BookmarkService();

        public static IPathsAccessor CreatePathsAccessor() => new PathsAccessor(CreateConfigurationAccessor());

        public static ISettingsAccessor CreateSettingsAccessor() => new SettingsAccessor(CreateConfigurationAccessor());

        public static IRandomNumberService CreateRandomNumberService() => new RandomNumberService();

        public static ITauntAccessor CreateTauntAccessor() => new TauntAccessor(CreateConfigurationAccessor());

        public static IVariableAccessor CreateVariableAccessor() => new VariableAccessor(CreateConfigurationAccessor());

        public static IVideoAccessor CreateVideoAccessor() => new VideoAccessor(CreateConfigurationAccessor());

        public static IImageAccessor CreateImageAccessor() => new ImageAccessor(CreateConfigurationAccessor(), CreatePathsAccessor());

        public static LineService CreateLineService() => new LineService();

        public static IFlagAccessor CreateFlagAccessor() => new FlagAccessor(CreateConfigurationAccessor());
    }
}
