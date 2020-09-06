using System;
using TeaseAI.Common.Interfaces;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data;
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
                CreateBookmarkService(),
                CreateMediaContainerService(),
                CreateTimeService(),
                CreateLineCollectionFilter()
            );
        }

        private static ILineCollectionFilter CreateLineCollectionFilter() => new LineCollectionFilter();

        private static ITimeService CreateTimeService() => new TimeService(CreateSettingsAccessor());

        internal static IGetCommandInformationAccessor CreateGetCommandInformationService() => new GetCommandInformationAccessor();

        private static IBookmarkService CreateBookmarkService() => new BookmarkService();

        public static IPathsAccessor CreatePathsAccessor() => new PathsAccessor(CreateConfigurationAccessor());

        public static ISettingsAccessor CreateSettingsAccessor() => new SettingsAccessor(CreateConfigurationAccessor());

        public static IRandomNumberService CreateRandomNumberService() => new RandomNumberService();

        public static ITauntAccessor CreateTauntAccessor() => new TauntAccessor(CreateConfigurationAccessor());

        public static IVariableAccessor CreateVariableAccessor() => new VariableAccessor(CreateConfigurationAccessor());

        public static IVideoAccessor CreateVideoAccessor() => new VideoAccessor(CreateConfigurationAccessor());

        public static IImageAccessor CreateImageAccessor() => new ImageAccessor(CreateConfigurationAccessor()
            , CreatePathsAccessor()
            , RepositoryFactory.CreateImageMetaDataRepository(CreateConfigurationAccessor())
            , CreateMediaContainerService());

        public static LineService CreateLineService() => new LineService();

        public static IFlagAccessor CreateFlagAccessor() => new FlagAccessor(CreateConfigurationAccessor());

        public static IItemTagService CreateItemTagService()
        {
            var itemTagRepository = RepositoryFactory.CreateItemTagRepository(CreateConfigurationAccessor());
            return new ItemTagService(itemTagRepository);
        }

        public static IGenreService CreateGenreService()
        {
            var genreRepository = RepositoryFactory.CreateGenreRepository(CreateConfigurationAccessor());
            return new GenreService(genreRepository);
        }

        public static IMediaContainerService CreateMediaContainerService()
        {
            var repository = RepositoryFactory.CreateMediaContainerRepository(CreateConfigurationAccessor());
            var genreRepository = RepositoryFactory.CreateGenreRepository(CreateConfigurationAccessor());
            return new MediaContainerService(repository, genreRepository);
        }
    }
}
