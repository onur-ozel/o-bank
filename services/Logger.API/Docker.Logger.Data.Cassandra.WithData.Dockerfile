FROM cassandra:latest

COPY [ "./resources/dataseeds/", "/" ]

ENTRYPOINT ["/bootstrap.sh"]

CMD ["cassandra", "-f"]