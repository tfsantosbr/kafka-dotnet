# kafka-cluster

## Resumo

Estudos de um kafka cluster, executado em ambiente Docker, com 3 nós e comandos via terminal para envio e recebimento de mensagens

## Tutoriais e Exemplos

https://www.youtube.com/watch?v=PppMhofKzy4
https://medium.com/better-programming/a-simple-apache-kafka-cluster-with-docker-kafdrop-and-python-cf45ab99e2b9
https://www.codemag.com/Article/2201061/Working-with-Apache-Kafka-in-ASP.NET-6-Core
https://github.com/confluentinc/confluent-kafka-dotnet/blob/master/examples/Web/RequestTimeConsumer.cs

## Links

https://github.com/confluentinc/cp-docker-images
https://github.com/lbrack1/kafka-tutorial/blob/master/docker-compose.yml

## Servidor Kafka

Abaixos comandos para interagir com o cluster Kafka rodando em Docker

```bash
# Docker

    # Entrar em um dos nós do cluster Kafka:
    docker exec -it kafka-server_kafka1_1 bash

# Tópicos

    # Criar um tópico no Kafka
    kafka-topics --create --bootstrap-server kafka1:19091 --replication-factor 3 --partitions 3 --topic meutopico

    # Listar tópicos
    kafka-topics --list --bootstrap-server kafka1:19091

    # Alterar Partições de um Tópico
    kafka-topics --alter --bootstrap-server kafka1:19091 --topic orders-order-created --partitions 10

    # Deletar Topico
    kafka-topics --bootstrap-server kafka1:19091 --delete --topic someTopic

# Producer

    # Iniciar um produtor de mensagens
    kafka-console-producer --broker-list kafka1:19091 --topic meutopico

# Consumer

    # Iniciar um consumer de mensagens
    kafka-console-consumer --bootstrap-server kafka1:19091 --topic meutopico

    # Ler as mensagens desde o início
    kafka-console-consumer --bootstrap-server kafka1:19091 --topic meutopico --from-beginning

    # Especificando um grupo para escalar os consumers. Dica: Executar em pelo menos uns 3 consumers para testar a escalabilidade.
    kafka-console-consumer --bootstrap-server kafka1:19091 --topic meutopico --group a

# Info

    # Descrever informações em relação ao tópico a fim de saber número de réplicas e partições:
    kafka-topics --describe --bootstrap-server kafka1:19091 --topic meutopico

    # Descrever um grupo de consumidores:
    kafka-consumer-groups --group a --bootstrap-server kafka1:19091 --describe
```

## Aplicações .NET

Abaixo os comandos para executar aplicações .NET de exemplos de conexões com o Kafka

```bash
# Executando um producer .NET Console App
dotnet run -p apps/producers/dotnet/KafkaProducer.ConsoleApp "meutopico"

# Executando um consumer .NET Console App
dotnet run -p apps/consumers/dotnet/KafkaConsumer.ConsoleApp "meutopico" "grupo-a"
```

