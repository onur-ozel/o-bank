package com.obank.kafka.connect.cassandra;

import java.util.Collection;
import java.util.Map;

import com.obank.kafka.connect.cassandra.services.CassandraService;
import com.obank.kafka.connect.cassandra.services.CassandraServiceImpl;

import org.apache.kafka.clients.consumer.OffsetAndMetadata;
import org.apache.kafka.common.TopicPartition;
import org.apache.kafka.connect.sink.SinkRecord;
import org.apache.kafka.connect.sink.SinkTask;

public class OBankCassandraSinkTask extends SinkTask {
  private CassandraService cassandraService;

  @Override
  public String version() {
    return VersionUtil.getVersion();
  }

  @Override
  public void start(Map<String, String> map) {
    cassandraService = new CassandraServiceImpl(new OBankCassandraSinkConnectorConfig(map));
  }

  @Override
  public void put(Collection<SinkRecord> collection) {
    cassandraService.process(collection);
  }

  @Override
  public void flush(Map<TopicPartition, OffsetAndMetadata> map) {

  }

  @Override
  public void stop() {
    cassandraService.closeClient();
  }
}
