package com.obank.kafka.connect.cassandra;

import org.junit.Test;

public class OBankCassandraSinkConnectorConfigTest {
  @Test
  public void doc() {
    System.out.println(OBankCassandraSinkConnectorConfig.conf().toRst());
  }
}
