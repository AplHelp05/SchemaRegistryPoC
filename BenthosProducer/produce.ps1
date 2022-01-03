docker rm BenthosProducer -f
docker run --name BenthosProducer --env-file ./variables.env --rm `
    -v "$pwd/config:/config/"  `
    --network dependencies_kafka-net `
    jeffail/benthos `
    -c /config/main.yml