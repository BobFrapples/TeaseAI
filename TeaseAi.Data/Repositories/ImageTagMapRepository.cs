using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TeaseAI.Common;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Data.Repositories
{
    public class ImageTagMapRepository : IImageTagMapRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public ImageTagMapRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<ImageTagMap> Create(ImageTagMap item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    var newItem = model.ImageTagMaps.Add(item);
                    model.SaveChanges();
                    return Result.Ok(newItem);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<ImageTagMap>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result Delete(ImageTagMap item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {

                    model.ImageTagMaps.Attach(item);
                    model.ImageTagMaps.Remove(item);
                    model.SaveChanges();
                    return Result.Ok();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result SetTagsForImage(int imageId, IEnumerable<int> tags)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    var tagMaps = model.ImageTagMaps.Where(itm => itm.ImageId == imageId);
                    model.ImageTagMaps.RemoveRange(tagMaps);

                    var newTagMaps = tags.Select(tid => new ImageTagMap
                    {
                        ImageId = imageId,
                        ItemTagId = (ItemTagId)tid,
                    });

                    model.ImageTagMaps.AddRange(newTagMaps);
                    model.SaveChanges();
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public List<ImageTagMap> GetTagMapsForImage(int imageId)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.ImageTagMaps.Where(itm => itm.ImageId == imageId).ToList();
            }
        }

        public List<ImageTagMap> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.ImageTagMaps.ToList();
            }
        }

        public Result<ImageTagMap> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result<ImageTagMap> Update(ImageTagMap item)
        {
            throw new NotImplementedException();
        }
    }
}
