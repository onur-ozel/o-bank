FROM confluentinc/cp-kafka-connect-base:5.2.1

ENV CONNECT_PLUGIN_PATH="/usr/share/java,/usr/share/confluent-hub-components"
COPY /kafka_connect_cassandra/target/kafka_connect_cassandra-1.0-package/share/java/kafka_connect_cassandra /usr/share/confluent-hub-components/kafka_connect_cassandra

COPY [ "kafka_connect_cassandra_docker_utils/bootstrap.sh", "/" ]
COPY [ "kafka_connect_cassandra_docker_utils/wait-for-it.sh", "/" ]

ENTRYPOINT ["/bootstrap.sh"]

CMD ["/etc/confluent/docker/run"]