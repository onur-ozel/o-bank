

    



// using Avro.Generic;
// using Confluent.Kafka;
// using Confluent.Kafka.Serialization;
// using System;
// using System.Collections.Generic;
// using System.Text;

// namespace KafkaConsumer
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var config = new Dictionary<string, object>
//             {
//                 { "group.id", "messageConsumer6" },
//                 { "bootstrap.servers", "localhost:9092" },
//                 { "enable.auto.commit", "true"},
//                 { "auto.offset.reset", "earliest" }
//             };

//             using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
//             {
//                 consumer.Subscribe(new string[] { "test_topic" });

//                 consumer.OnMessage += (_, msg) =>
//                 {
//                     Console.WriteLine($"Message: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
//                     //consumer.CommitAsync(msg);
//                 };

//                 while (true)
//                 {
//                     consumer.Poll(100);
//                 }
//             }




//             // var config = new Dictionary<string, object>
//             // {
//             //    { "group.id", "messageConsumer" },
//             //    { "bootstrap.servers", "localhost:9092" },
//             //    { "enable.auto.commit", "false"},
//             //    { "auto.offset.reset", "beginning" },
//             //    { "schema.registry.url", "localhost:5011" },
//             // };

//             // using (var consumer = new Consumer<Null, GenericRecord>(config, null, new AvroDeserializer<GenericRecord>()))
//             // {
//             //     consumer.Subscribe(new string[] { "log-messages" });

//             //     consumer.OnMessage += (_, msg) =>
//             //     {
//             //         Console.WriteLine($"Message: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
//             //         consumer.CommitAsync(msg);
//             //     };

//             //     while (true)
//             //     {
//             //         consumer.Poll(100);
//             //     }
//             // }
//         }
//     }
// }





// using Avro;
// using Avro.Generic;
// using Confluent.Kafka;
// using Confluent.Kafka.Serialization;
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Text;

// namespace KafkaProducer
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             // var config = new Dictionary<string, object>
//             // {
//             //     { "bootstrap.servers", "localhost:9092" }
//             // };

//             // using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
//             // {
//             //     string text = null;

//             //     while (text != "quit")
//             //     {
//             //         Console.Write("Add Message: ");
//             //         text = Console.ReadLine();
//             //         producer.ProduceAsync("message", null, text);
//             //     }
//             //     producer.Flush(100);
//             // }







//             var config = new Dictionary<string, object>
//             {
//                { "bootstrap.servers", "localhost:9092" },
//                { "schema.registry.url", "localhost:5011" }
//             };

//             using (var producer = new Producer<Null, GenericRecord>(config, null, new AvroSerializer<GenericRecord>()))
//             {
//                 var logLevelSchema = (EnumSchema)Schema.Parse(
//                 File.ReadAllText("LogLevel.asvc"));

//                 var logMessageSchema = (RecordSchema)Schema
//                 .Parse(File.ReadAllText("LogMessage.V1.asvc")
//                     .Replace(
//                         "MessageTypes.LogLevel",
//                         File.ReadAllText("LogLevel.asvc")));

//                 var record = new GenericRecord(logMessageSchema);
//                 record.Add("IP", "127.0.0.1");
//                 record.Add("Message", "a test log message");
//                 record.Add("Severity", new GenericEnum(logLevelSchema, "Error"));
//                 producer.ProduceAsync("log-messages", null, record)
//                     .ContinueWith(dr => Console.WriteLine(dr.Result.Error
//                         ? $"error producing message: {dr.Result.Error.Reason}"
//                         : $"produced to: {dr.Result.TopicPartitionOffset}"));

//                 producer.Flush(TimeSpan.FromSeconds(30));
//             }
//         }
//     }
// }
