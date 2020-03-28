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
    public class ImageMetaDataRepository : IImageMetaDataRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public ImageMetaDataRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result Create(IEnumerable<ImageMetaData> images)
        {
            try
            {

                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    model.ImageMetaDatas.AddRange(images);
                    model.SaveChanges();
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result Update(IEnumerable<ImageMetaData> images)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    foreach (var img in images)
                    {
                        var existing = model.ImageMetaDatas.Single(imd => imd.Id == img.Id);
                        existing.FullFileName = img.FullFileName;
                        existing.GenreId = img.GenreId;
                        existing.SourceId = img.SourceId;
                        existing.ItemName = img.ItemName;
                    }
                    model.SaveChanges();
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public List<ImageMetaData> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.ImageMetaDatas.ToList();
            }
        }

        public List<ImageMetaData> Get(ImageSource? source, ImageGenre? genre)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.ImageMetaDatas
                    .Where(imd => (!source.HasValue || imd.SourceId == source.Value) && (!genre.HasValue || imd.GenreId == genre.Value))
                    .ToList();
            }
        }

        public Result<ImageMetaData> Create(ImageMetaData item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    model.ImageMetaDatas.Add(item);
                    model.SaveChanges();
                }
                return Result.Ok(item);
            }
            catch (Exception ex)
            {
                return Result.Fail<ImageMetaData>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result<ImageMetaData> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result<ImageMetaData> Update(ImageMetaData item)
        {
            throw new NotImplementedException();
        }

        public Result Delete(ImageMetaData item)
        {
            throw new NotImplementedException();
        }

        public List<ImageMetaData> GetImagesWithTag(ItemTagId itemTagId)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return (from imd in model.ImageMetaDatas
                        join itm in model.ImageTagMaps on imd.Id equals itm.ImageId
                        where itm.ItemTagId == itemTagId
                        select imd).ToList();
            }
        }
    }
}
