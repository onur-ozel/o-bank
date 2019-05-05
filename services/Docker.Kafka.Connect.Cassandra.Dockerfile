#
# Copyright 2018 Confluent Inc.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

FROM confluentinc/cp-kafka-connect-base:5.2.1

ENV CONNECT_PLUGIN_PATH="/usr/share/java,/usr/share/confluent-hub-components"
COPY /Utils/kafka_connect_cassandra/target/kafka_connect_cassandra-1.0-package/share/java/kafka_connect_cassandra/. /usr/share/java

# RUN curl -X POST -H "Content-Type: application/json" --data '{"name": "local-file-sink", "config": {"connector.class":"FileStreamSinkConnector", "tasks.max":"1", "file":"test.sink.txt", "topics":"connect-test" }}' http://localhost:8083/connectors
# Or, to use a file containing the JSON-formatted configuration
# curl -X POST -H "Content-Type: application/json" --data @config.json http://localhost:8083/connectors


# RUN confluent-hub install --no-prompt confluentinc/kafka-connect-cassandra:latest



# FROM confluentinc/cp-kafka-connect

# ENV MYSQL_DRIVER_VERSION 5.1.39

# RUN curl -k -SL "https://dev.mysql.com/get/Downloads/Connector-J/mysql-connector-java-${MYSQL_DRIVER_VERSION}.tar.gz" \
#      | tar -xzf - -C /usr/share/java/kafka/ --strip-components=1 mysql-connector-java-5.1.39/mysql-connector-java-${MYSQL_DRIVER_VERSION}-bin.jar