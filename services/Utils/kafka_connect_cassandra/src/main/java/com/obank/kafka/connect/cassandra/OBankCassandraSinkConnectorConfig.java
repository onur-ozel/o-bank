package com.obank.kafka.connect.cassandra;

import org.apache.kafka.common.config.AbstractConfig;
import org.apache.kafka.common.config.ConfigDef;
import org.apache.kafka.common.config.ConfigDef.Type;
import org.apache.kafka.common.config.ConfigDef.Importance;

import java.util.Map;

public class OBankCassandraSinkConnectorConfig extends AbstractConfig {

  public static final String CASSANDRA_HOST = "cassandra.host";
  private static final String CASSANDRA_HOST_DOC = "cassandra host name";

  public static final String CASSANDRA_PORT = "cassandra.port";
  private static final String CASSANDRA_PORT_DOC = "cassandra port number";

  public static final String CASSANDRA_KEYSPACE = "cassandra.keyspace";
  private static final String CASSANDRA_KEYSPACE_DOC = "cassandra keyspace name";

  public static final String CASSANDRA_TABLE = "cassandra.table";
  private static final String CASSANDRA_TABLE_DOC = "cassandra table name";

  public OBankCassandraSinkConnectorConfig(ConfigDef config, Map<String, String> parsedConfig) {
    super(config, parsedConfig);
  }

  public OBankCassandraSinkConnectorConfig(Map<String, String> parsedConfig) {
    this(conf(), parsedConfig);
  }

  public static ConfigDef conf() {
    return new ConfigDef().define(CASSANDRA_HOST, Type.STRING, Importance.HIGH, CASSANDRA_HOST_DOC)
        .define(CASSANDRA_PORT, Type.INT, Importance.HIGH, CASSANDRA_PORT_DOC)
        .define(CASSANDRA_KEYSPACE, Type.STRING, Importance.HIGH, CASSANDRA_KEYSPACE_DOC)
        .define(CASSANDRA_TABLE, Type.STRING, Importance.HIGH, CASSANDRA_TABLE_DOC);
  }

  public String getCassandraHost() {
    return this.getString(CASSANDRA_HOST);
  }

  public Integer getCassandraPort() {
    return this.getInt(CASSANDRA_PORT);
  }

  public String getCassandraKeyspace() {
    return this.getString(CASSANDRA_KEYSPACE);
  }

  public String getCassandraTable() {
    return this.getString(CASSANDRA_TABLE);
  }
}
