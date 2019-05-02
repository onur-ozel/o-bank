package com.obank.kafka.connect.cassandra;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.kafka.common.config.ConfigDef;
import org.apache.kafka.connect.connector.Task;
import org.apache.kafka.connect.errors.ConnectException;
import org.apache.kafka.connect.sink.SinkConnector;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class OBankCassandraSinkConnector extends SinkConnector {
  private static Logger log = LoggerFactory.getLogger(OBankCassandraSinkConnector.class);
  private OBankCassandraSinkConnectorConfig config;
  private Map<String, String> configProperties;

  @Override
  public String version() {
    return VersionUtil.getVersion();
  }

  @Override
  public void start(Map<String, String> map) {
    config = new OBankCassandraSinkConnectorConfig(map);
    configProperties = map;
    // TODO: Add things you need to do to setup your connector.

    /**
     * This will be executed once per connector. This can be used to handle
     * connector level setup.
     */

  }

  @Override
  public Class<? extends Task> taskClass() {
    // TODO: Return your task implementation.
    return OBankCassandraSinkTask.class;
  }

  @Override
  public List<Map<String, String>> taskConfigs(int maxTasks) {
    List<Map<String, String>> taskConfigs = new ArrayList<>();
    Map<String, String> taskProps = new HashMap<>();

    taskProps.putAll(configProperties);

    for (int i = 0; i < maxTasks; i++) {
      taskConfigs.add(taskProps);
    }

    return taskConfigs;

    // TODO: Define the individual task configurations that will be executed.

    /**
     * This is used to schedule the number of tasks that will be running. This
     * should not exceed maxTasks.
     */
  }

  @Override
  public void stop() {
    // TODO: Do things that are necessary to stop your connector.
  }

  @Override
  public ConfigDef config() {
    return OBankCassandraSinkConnectorConfig.conf();
  }
}
