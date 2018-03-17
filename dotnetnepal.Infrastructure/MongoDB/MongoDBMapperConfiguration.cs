using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();

                //ignore these Fields, an equivalent of [BsonIgnore]
                cm.UnmapMember(c => c.ProductType);
                cm.UnmapMember(c => c.IntervalUnitType);
                cm.UnmapMember(c => c.BackorderMode);
                cm.UnmapMember(c => c.DownloadActivationType);
                cm.UnmapMember(c => c.GiftCardType);
                cm.UnmapMember(c => c.LowStockActivity);
                cm.UnmapMember(c => c.ManageInventoryMethod);
                cm.UnmapMember(c => c.RecurringCyclePeriod);
            });

            BsonClassMap.RegisterClassMap<ProductAttributeCombination>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<ProductAttributeMapping>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
                cm.UnmapMember(c => c.AttributeControlType);
            });

            BsonClassMap.RegisterClassMap<ProductAttributeValue>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductAttributeMappingId);
                cm.UnmapMember(c => c.ProductId);
                cm.UnmapMember(c => c.AttributeValueType);
            });

            BsonClassMap.RegisterClassMap<ProductCategory>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<ProductManufacturer>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<ProductPicture>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<ProductSpecificationAttribute>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
                cm.UnmapMember(c => c.AttributeType);
            });

            BsonClassMap.RegisterClassMap<ProductTag>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<ProductWarehouseInventory>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<RelatedProduct>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId1);
            });

            BsonClassMap.RegisterClassMap<BundleProduct>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductBundleId);
            });

            BsonClassMap.RegisterClassMap<TierPrice>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ProductId);
            });

            BsonClassMap.RegisterClassMap<Address>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.CustomerId);
            });

            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.PasswordFormat);
            });

            BsonClassMap.RegisterClassMap<CustomerAction>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Condition);
                cm.UnmapMember(c => c.ReactionType);
            });

            BsonClassMap.RegisterClassMap<CustomerAction.ActionCondition>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.CustomerActionConditionType);
                cm.UnmapMember(c => c.Condition);
            });

            BsonClassMap.RegisterClassMap<CustomerAttribute>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.AttributeControlType);
            });

            BsonClassMap.RegisterClassMap<CustomerHistoryPassword>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.PasswordFormat);
            });

            BsonClassMap.RegisterClassMap<CustomerReminder>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Condition);
                cm.UnmapMember(c => c.ReminderRule);
            });

            BsonClassMap.RegisterClassMap<CustomerReminder.ReminderCondition>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ConditionType);
                cm.UnmapMember(c => c.Condition);
            });

            BsonClassMap.RegisterClassMap<CustomerReminderHistory>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.ReminderRule);
                cm.UnmapMember(c => c.HistoryStatus);
            });

            
        }
    }
}
