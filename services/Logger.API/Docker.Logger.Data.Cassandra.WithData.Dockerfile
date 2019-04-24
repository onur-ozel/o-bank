FROM cassandra:latest

COPY [ "./infrastructure/dataseeds/", "/" ]

ENTRYPOINT ["/bootstrap.sh"]

CMD ["cassandra", "-f"]