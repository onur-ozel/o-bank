package com.obank.kafka.connect.cassandra.services;

import java.util.Collection;

import org.apache.kafka.connect.sink.SinkRecord;

/**
 * CassandraService
 */
public interface CassandraService {
    void process(Collection<SinkRecord> records);
    void closeClient();
}