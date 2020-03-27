using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces.Accessors;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Data.Repositories
{
    public class ItemTagRepository : IItemTagRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public ItemTagRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<ItemTag> Create(ItemTag itemTag)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    model.ItemTags.Add(itemTag);
                    model.SaveChanges();
                    return Result.Ok(itemTag);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<ItemTag>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result Delete(ItemTag item)
        {
            throw new NotImplementedException();
        }

        public List<ItemTag> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.ItemTags.ToList();
            }
        }

        public Result<ItemTag> Get(int tagId)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                var data = model.ItemTags.Where(it => it.Id == tagId).ToList();
                if (data.Any())
                    return Result.Ok(data.First());
                return Result.Fail<ItemTag>("No tag found with Id of " + tagId.ToString());
            }

        }

        public List<ItemTag> GetTagsForImage(int imageId)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                var tags = model.ImageTagMaps
                    .Where(itm => itm.ImageId == imageId)
                    .Select(itm => itm.ItemTagId)
                    .ToList();

                return model.ItemTags
                    .Where(it => tags.Contains(it.Id))
                    .ToList();
            }
        }

        public Result<ItemTag> Update(ItemTag itemTag)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    var existing = model.ItemTags.FirstOrDefault(it => it.Id == itemTag.Id);
                    if (existing == null)
                        return Result.Fail<ItemTag>("Item tag " + itemTag.Name + " was not found");

                    existing.Name = itemTag.Name;
                    existing.Description = itemTag.Description;

                    model.SaveChanges();
                    return Result.Ok(itemTag);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<ItemTag>(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
