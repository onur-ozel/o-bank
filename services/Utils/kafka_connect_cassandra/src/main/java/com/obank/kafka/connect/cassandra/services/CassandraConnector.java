package com.obank.kafka.connect.cassandra.services;

import com.datastax.driver.core.Cluster;
import com.datastax.driver.core.Session;

/**
 * CassandraConnector
 */
public class CassandraConnector {

    private Cluster cluster;
    private Session session;

    public void connect(final String host, final Integer port) {
        this.cluster = Cluster.builder().addContactPoint(host).withPort(port).build();

        session = cluster.connect();
    }

    public Session getSession() {
        return this.session;
    }

    public void close() {
        this.cluster.close();
    }
}