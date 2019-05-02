package com.obank.kafka.connect.cassandra.services;

import java.io.IOException;
import java.util.Collection;
import java.util.UUID;

import com.obank.kafka.connect.cassandra.OBankCassandraSinkConnectorConfig;

import org.apache.kafka.connect.sink.SinkRecord;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * CassandraServiceImpl
 */
public class CassandraServiceImpl implements CassandraService {

    private static Logger log = LoggerFactory.getLogger(CassandraServiceImpl.class);
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
        String id = UUID.randomUUID().toString();

        StringBuilder sb = new StringBuilder("INSERT INTO ").append(config.getCassandraKeyspace()).append(".")
                .append(config.getCassandraTable()).append(" (id,onur) VALUES (?, ?)");

        String query = sb.toString();

        for (SinkRecord record : records) {
            client.getSession().execute(query, id, record.value());
        }

    }

    @Override
    public void closeClient() {
        client.close();
    }

}