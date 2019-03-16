using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Customer.API.Infrastructure.EventBus {
    public class CustomerKafkaEventBusService : ICustomerEventBusService {
        private static Dictionary<string, object> producerConfig = new Dictionary<string, object> { { "bootstrap.servers", "localhost:9092" }
        };

        private static Producer<Null, string> producer = new Producer<Null, string> (producerConfig, null, new StringSerializer (Encoding.UTF8));

        public async void PublishAsync (string topic, string message) {
            await producer.ProduceAsync (topic, null, message);
        }
    }
}