docker rm BenthosConsumer -f
docker run --name BenthosConsumer --env-file ./variables.env --rm `
    -v "$pwd/config:/config/"  `
    --network dependencies_kafka-net `
    jeffail/benthos `
    -c /config/main.yml