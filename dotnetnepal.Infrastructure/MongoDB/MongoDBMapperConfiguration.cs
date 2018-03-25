using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace dotnetnepal.Infrastructure.MongoDB
{
    public class MongoDBMapperConfiguration
    {
        /// <summary>
        /// Register MongoDB mappings
        /// </summary>
        /// <param name="config">Config</param>
        public static void RegisterMongoDBMappings(GrandConfig config)
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());

            //global set an equivalent of [BsonIgnoreExtraElements] for every Domain Model
            var cp = new ConventionPack();
            cp.Add(new IgnoreExtraElementsConvention(true));
            ConventionRegistry.Register("ApplicationConventions", cp, t => true);

            //BsonClassMap.RegisterClassMap<ProductCategory>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.UnmapMember(c => c.ProductId);
            //});

            //BsonClassMap.RegisterClassMap<ProductManufacturer>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.UnmapMember(c => c.ProductId);
            //});

            //BsonClassMap.RegisterClassMap<CustomerReminderHistory>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.UnmapMember(c => c.ReminderRule);
            //    cm.UnmapMember(c => c.HistoryStatus);
            //});
        }
    }
}