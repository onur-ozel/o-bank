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
