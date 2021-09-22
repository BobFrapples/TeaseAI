using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;

namespace TeaseAI.Data.Repositories
{
    public class SlideshowRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public SlideshowRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }


        public List<string> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                var localMediaContainers = model.MediaContainers.Where(mc => mc.SourceId == ImageSource.Local && mc.IsEnabled).Select(mc => mc.Id).ToList();
                var imageFiles = model.ImageMetaDatas.Where(imd => localMediaContainers.Contains(imd.MediaContainerId)).Select(imd => imd.FullFileName).ToList();
                return imageFiles.Select(i => System.IO.Path.GetDirectoryName(i)).ToList();
            }
        }
    }
}
