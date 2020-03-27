using System;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data.Interfaces;
using TeaseAI.Data.Repositories;

namespace TeaseAI.Data
{
    public static class RepositoryFactory
    {
        public static IItemTagRepository CreateItemTagRepository(IConfigurationAccessor configurationAccessor)
        {
            return new ItemTagRepository(configurationAccessor);
        }

        public static IImageMetaDataRepository CreateImageMetaDataRepository(IConfigurationAccessor configurationAccessor)
        {
            return new ImageMetaDataRepository(configurationAccessor);
        }

        public static IMediaContainerRepository CreateMediaContainerRepository(IConfigurationAccessor configurationAccessor) => new MediaContainerRepository(configurationAccessor);

        public static IGenreRepository CreateGenreRepository(IConfigurationAccessor configurationAccessor) => new GenreRepository(configurationAccessor);

        public static IImageTagMapRepository CreateItemTagMapRepository(IConfigurationAccessor configurationAccessor) => new ImageTagMapRepository(configurationAccessor);
    }
}
