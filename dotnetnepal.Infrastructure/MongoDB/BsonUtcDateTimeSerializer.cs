using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetnepal.Infrastructure.MongoDB
{
    public class BsonUtcDateTimeSerializer : DateTimeSerializer
    {
        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new DateTime(base.Deserialize(context, args).Ticks, DateTimeKind.Unspecified);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var utcValue = new DateTime(value.Ticks, DateTimeKind.Utc);
            base.Serialize(context, args, utcValue);
        }
    }
}
