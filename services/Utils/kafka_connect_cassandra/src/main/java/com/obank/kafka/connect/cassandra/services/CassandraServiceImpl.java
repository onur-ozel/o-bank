package com.obank.kafka.connect.cassandra.services;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.stream.Collectors;

import com.datastax.driver.core.PreparedStatement;
import com.obank.kafka.connect.cassandra.OBankCassandraSinkConnectorConfig;

import org.apache.kafka.connect.data.Field;
import org.apache.kafka.connect.data.Struct;
import org.apache.kafka.connect.sink.SinkRecord;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * CassandraServiceImpl
 */
public class CassandraServiceImpl implements CassandraService {

    private final CassandraConnector client = new CassandraConnector();
    private OBankCassandraSinkConnectorConfig config;
    static final Logger log = LoggerFactory.getLogger(CassandraServiceImpl.class);
    static HashMap<String, PreparedStatement> preparedStatements = new HashMap<String, PreparedStatement>();

    private String host;
    private Integer port;

    public CassandraServiceImpl(OBankCassandraSinkConnectorConfig config) {
        this.config = config;

        host = config.getCassandraHost();
        port = config.getCassandraPort();

    }

    @Override
    public void connect() {
        client.connect(host, port);
    }

    @Override
    public void process(Collection<SinkRecord> records) {

        SinkRecord firstRecord;
        try {
            firstRecord = records.iterator().next();
        } catch (Exception e) {
            log.error(e.getMessage());
            return;
        }

        Struct firstRecordValue;

        try {
            firstRecordValue = (Struct) firstRecord.value();
        } catch (Exception e) {
            log.error(e.getMessage());
            return;
        }

        PreparedStatement preparedStatement;

        List<Field> fields = firstRecord.valueSchema().fields();
        List<Object> values = new ArrayList<>();
        for (Field field : fields) {
            Object value = firstRecordValue.get(field);
            values.add(value);
        }

        if (!client.isConnected()) {
            try {
                connect();
                preparedStatements.clear();
            } catch (Exception e) {
                log.error(e.toString());
                return;
            }
        }

        if (preparedStatements.get(firstRecord.valueSchema().name()) != null) {
            preparedStatement = preparedStatements.get(firstRecord.valueSchema().name());
        } else {
            StringBuilder baseQuery = new StringBuilder("INSERT INTO \"").append(config.getCassandraKeyspace())
                    .append("\".\"").append(config.getCassandraTable()).append("\"");

            String fieldsNames = String.join(",",
                    fields.stream().map(x -> new StringBuilder().append("\"").append(x.name()).append("\"").toString())
                            .collect(Collectors.toList()));

            log.error(fieldsNames);

            String fieldBinds = String.join(",", Collections.nCopies(fields.size(), "?"));

            baseQuery.append(" (").append(fieldsNames).append(")");

            baseQuery.append(" VALUES (").append(fieldBinds).append(")");

            log.error(baseQuery.toString());

            preparedStatement = client.getSession().prepare(baseQuery.toString());

            preparedStatements.put(firstRecord.valueSchema().name(), preparedStatement);
        }

        try {
            client.getSession().execute(preparedStatement.bind(values.toArray(new Object[values.size()])));

        } catch (Exception e) {
            log.error(e.getMessage());
        }

    }

    @Override
    public void closeClient() {
        client.close();
    }

}
