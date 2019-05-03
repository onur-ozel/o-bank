package com.obank.kafka.connect.cassandra;

import java.util.List;
import java.util.Map;

import org.apache.kafka.common.config.ConfigDef;
import org.apache.kafka.connect.connector.Task;
import org.apache.kafka.connect.source.SourceConnector;

public class MySourceConnector extends SourceConnector {
  @Override
  public String version() {
    return VersionUtil.getVersion();
  }

  @Override
  public void start(Map<String, String> map) {
    new MySourceConnectorConfig(map);

    //TODO: Add things you need to do to setup your connector.
  }

  @Override
  public Class<? extends Task> taskClass() {
    //TODO: Return your task implementation.
    return MySourceTask.class;
  }

  @Override
  public List<Map<String, String>> taskConfigs(int i) {
    //TODO: Define the individual task configurations that will be executed.

    throw new UnsupportedOperationException("This has not been implemented.");
  }

  @Override
  public void stop() {
    //TODO: Do things that are necessary to stop your connector.
  }

  @Override
  public ConfigDef config() {
    return MySourceConnectorConfig.conf();
  }
}
