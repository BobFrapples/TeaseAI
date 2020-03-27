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
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfigurationAccessor _configurationAccessor;

        public GenreRepository(IConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        public Result<Genre> Create(Genre item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    model.Genres.Add(item);

                    model.SaveChanges();
                    return Result.Ok(item);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<Genre>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Result Delete(Genre item)
        {
            throw new System.NotImplementedException();
        }

        public List<Genre> Get()
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return model.Genres.ToList();

            }
        }

        public Result<Genre> Get(int id)
        {
            var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
            using (var model = new EntityFramework.Model(sqliteConnection))
            {
                return Result.Ok(model.Genres.SingleOrDefault(i => i.Id == id))
                    .Ensure(i => i != null, "No genre with id " + id.ToString() + " was found.");
            }
        }

        public Result<Genre> Update(Genre item)
        {
            try
            {
                var sqliteConnection = new SQLiteConnection(_configurationAccessor.GetDatabaseConnectionString());
                using (var model = new EntityFramework.Model(sqliteConnection))
                {
                    var existing = model.Genres.SingleOrDefault(i => i.Id == item.Id);
                    if (existing == null)
                        return Result.Fail<Genre>("No genre with id " + item.Id.ToString() + " was found.");
                    existing.Name = item.Name;
                    model.SaveChanges();
                    return Result.Ok(item);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<Genre>(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
