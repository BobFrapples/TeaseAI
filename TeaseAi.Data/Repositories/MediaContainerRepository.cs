using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Data.Repositories
{
    public class MediaContainerRepository : IMediaContainerRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public MediaContainerRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<MediaContainer> Create(MediaContainer item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    model.MediaContainers.Add(item);

                    model.SaveChanges();
                    return Result.Ok(item);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<MediaContainer>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result Delete(MediaContainer item)
        {
            throw new NotImplementedException();
        }

        public List<MediaContainer> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.MediaContainers.ToList();
            }
        }

        public Result<MediaContainer> Get(int id)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return Result.Ok(model.MediaContainers.SingleOrDefault(i => i.Id == id))
                    .Ensure(i => i != null, "No media container with id " + id.ToString() + " was found.");
            }
        }

        public Result<MediaContainer> Update(MediaContainer item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    var existing = model.MediaContainers.SingleOrDefault(i => i.Id == item.Id);
                    if (existing == null)
                        return Result.Fail<MediaContainer>("No media container with id " + item.Id.ToString() + " was found.");
                    existing.Name = item.Name;
                    existing.GenreId = item.GenreId;
                    existing.IsEnabled  = item.IsEnabled;
                    existing.Path  = item.Path;
                    existing.UseSubFolders  = item.UseSubFolders;
                    existing.SourceId  = item.SourceId;
                    existing.MediaTypeId  = item.MediaTypeId;

                    model.SaveChanges();
                    return Result.Ok(item);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<MediaContainer>(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }

}
