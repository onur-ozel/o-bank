- C:\\Users\\t32465\\Desktop\\kafka-connect\\kafka_cassandra_connect\\target\\kafka_cassandra_connect-1.0-package\\share\\java\\kafka_cassandra_connect:/opt/landoop/connectors/third-party/kafka-connect-file
http://localhost:3030/logs/connect-distributed.log
http://localhost:3030/kafka-connect-ui/#/cluster/fast-data-dev/connector/cassandraSinktest1
connector.class=com.obank.kafka.connect.cassandra.OBankCassandraSinkConnector
cassandra.url=cassandra
value.converter.schema.registry.url=http://kafka-cluster:8081
topics=test_topic
cassandra.table=test_table
value.converter.schemas.enable=false
cassandra.port=9042
cassandra.keyspace=test_keyspace
value.converter=io.confluent.connect.avro.AvroConverter

package com.obank.kafka.connect.cassandra.services;

import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;

import com.datastax.driver.core.PreparedStatement;
import com.obank.kafka.connect.cassandra.OBankCassandraSinkConnectorConfig;

import org.apache.kafka.connect.sink.SinkRecord;

/**
 * CassandraServiceImpl
 */
public class CassandraServiceImpl implements CassandraService {

    private final CassandraConnector client = new CassandraConnector();
    private OBankCassandraSinkConnectorConfig config;

    public CassandraServiceImpl(OBankCassandraSinkConnectorConfig config) {
        this.config = config;

        final String host = config.getCassandraHost();
        final Integer port = config.getCassandraPort();

        client.connect(host, port);
    }

    @Override
    public void process(Collection<SinkRecord> records) {
        StringBuilder baseQuery = new StringBuilder("INSERT INTO ").append(config.getCassandraKeyspace()).append(".")
                .append(config.getCassandraTable());

        SinkRecord firstRecord = records.iterator().next();
        HashMap<String, Object> firstRecordValue = (HashMap<String, Object>) firstRecord.value();

        String fields = String.join(",", firstRecordValue.keySet());
        String fieldBinds = String.join(",", Collections.nCopies(firstRecordValue.keySet().size(), "?"));

        baseQuery.append(" (").append(fields).append(")");

        baseQuery.append(" VALUES (").append(fieldBinds).append(")");

        PreparedStatement prepared = client.getSession().prepare(baseQuery.toString());

        for (SinkRecord record : records) {
            HashMap<String, Object> recordValue = (HashMap<String, Object>) record.value();

            client.getSession()
                    .execute(prepared.bind(recordValue.values().toArray(new Object[recordValue.values().size()])));
        }
    }

    @Override
    public void closeClient() {
        client.close();
    }

}
