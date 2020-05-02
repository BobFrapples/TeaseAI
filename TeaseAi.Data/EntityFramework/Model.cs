namespace TeaseAI.Data.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.SQLite;
    using TeaseAI.Common.Data;

    public class Model : DbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TeaseAi.Data.EntityFramework.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public Model(SQLiteConnection sqliteConnection)
            : base(sqliteConnection, true)
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ImageMetaData> ImageMetaDatas { get; set; }
        public virtual DbSet<ItemTag> ItemTags { get; set; }
        public virtual DbSet<ImageTagMap> ImageTagMaps { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediaContainer> MediaContainers { get; set; }
    }
}