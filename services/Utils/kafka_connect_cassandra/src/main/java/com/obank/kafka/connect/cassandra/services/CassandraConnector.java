package com.obank.kafka.connect.cassandra.services;

import com.datastax.driver.core.Cluster;
import com.datastax.driver.core.Session;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * CassandraConnector
 */
public class CassandraConnector {

    private Cluster cluster;
    private Session session;
    static final Logger log = LoggerFactory.getLogger(CassandraConnector.class);

    public void connect(final String host, final Integer port) {
        try {
            this.cluster = Cluster.builder().addContactPoint(host).withPort(port).build();

            session = cluster.connect();
        } catch (Exception e) {
            log.error(e.toString());
            session = null;
            cluster = null;

            throw e;
        }

    }

    public boolean isConnected() {
        if (cluster != null) {
            return !cluster.isClosed();
        }

        return false;
    }

    public Session getSession() {
        return this.session;
    }

    public void close() {
        this.cluster.close();
    }
}