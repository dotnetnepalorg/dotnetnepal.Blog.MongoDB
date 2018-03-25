using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Blog.MongoDB.Models;

namespace WebApplication.Blog.MongoDB.Repository
{
    /// <summary>
    /// MongoDB repository
    /// </summary>
    public class MongoDBRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly IConfiguration _configuration;

        public MongoDBRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Fields

        /// <summary>
        /// Gets the collection
        /// </summary>
        protected IMongoCollection<T> _collection;
        public IMongoCollection<T> Collection
        {
            get
            {
                return _collection;
            }
        }

        /// <summary>
        /// Mongo Database
        /// </summary>
        protected IMongoDatabase _database;
        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MongoDBRepository()
        {
            string connectionString = _configuration.GetValue<string>("Database:DataConnectionString");

            if (!string.IsNullOrEmpty(connectionString))
            {
                var client = new MongoClient(connectionString);
                var databaseName = new MongoUrl(connectionString).DatabaseName;
                _database = client.GetDatabase(databaseName);
                _collection = _database.GetCollection<T>(typeof(T).Name);
            }
        }
        public MongoDBRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            _database = client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public MongoDBRepository(IMongoClient client)
        {
            string connectionString = _configuration.GetValue<string>("Database:DataConnectionString");
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            _database = client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public MongoDBRepository(IMongoClient client, IMongoDatabase mongodatabase)
        {
            _database = mongodatabase;
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(string id)
        {
            return this._collection.Find(e => e.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Insert(T entity)
        {


            this._collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual T Update(T entity)
        {
            this._collection.ReplaceOne(x => x.Id == entity.Id, entity, new UpdateOptions() { IsUpsert = false });
            return entity;

        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            this._collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this._collection.FindOneAndDeleteAsync(e => e.Id == entity.Id);
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IMongoQueryable<T> Table
        {
            get { return this._collection.AsQueryable(); }
        }

        /// <summary>
        /// Get collection by filter definitions
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IList<T> FindByFilterDefinition(FilterDefinition<T> query)
        {
            return this._collection.Find(query).ToList();
        }

        #endregion

    }
}
