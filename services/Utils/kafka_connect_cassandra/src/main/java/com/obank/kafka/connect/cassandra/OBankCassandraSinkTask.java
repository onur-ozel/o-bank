package com.obank.kafka.connect.cassandra;

import org.apache.kafka.clients.consumer.OffsetAndMetadata;
import org.apache.kafka.common.TopicPartition;
import org.apache.kafka.connect.errors.ConnectException;
import org.apache.kafka.connect.sink.SinkRecord;
import org.apache.kafka.connect.sink.SinkTask;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;
import java.util.Collection;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

import com.obank.kafka.connect.cassandra.services.CassandraService;
import com.obank.kafka.connect.cassandra.services.CassandraServiceImpl;

public class OBankCassandraSinkTask extends SinkTask {
  private static Logger log = LoggerFactory.getLogger(OBankCassandraSinkTask.class);
  private CassandraService cassandraService;

  @Override
  public String version() {
    return VersionUtil.getVersion();
  }

  @Override
  public void start(Map<String, String> map) {
    cassandraService = new CassandraServiceImpl(new OBankCassandraSinkConnectorConfig(map));
    // TODO: Create resources like database or api connections here.
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
