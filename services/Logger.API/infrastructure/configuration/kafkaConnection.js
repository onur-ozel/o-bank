const kafka = require('kafka-node');
const schemaRegister = require('avro-schema-registry');
let kafkaServiceInstance = null;

class KafkaService {

    constructor() {
        this.client = new kafka.KafkaClient({ kafkaHost: 'localhost:9092' })
        this.schemaRegistry = schemaRegister('http://localhost:8081');
        this.producer = new kafka.Producer(this.client)
        this.producer.on('ready', () => {
            console.log('Kafka Producer is connected and ready.')
            this.isReady = true
        })
        this.isReady = false

        this.producer.on('error', (error) => {
            console.error(error)
        })
    }

    sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms))
    }

    async sendRecord(topic, record, schema, callback) {
        let retries = 0

        while (!this.isReady && retries < 3) {
            retries += 1
            await this.sleep(100)
        }

        if (!this.isReady) {
            console.log('Kafka producer is not ready.  Try again later.')
            return false
        }

        this.schemaRegistry.encodeMessage(topic, schema, record)
            .then((msg) => {
                const payloads = [{
                    topic: topic,
                    key: record.id,
                    messages: msg,
                }]
                this.producer.send(payloads, callback)
            })

        return true
    }
}

function getKafkaServiceInstance() {
    if (!kafkaServiceInstance) {
        kafkaServiceInstance = new KafkaService()
    }
    return kafkaServiceInstance;
}

module.exports = getKafkaServiceInstance();