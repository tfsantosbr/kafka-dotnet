# Comandos Kafka

Entrar em um dos nós do cluster Kafka:

```bash
docker exec -it kafka-cluster_kafka-1_1 bash
```

## Tópicos

Criar um tópico no Kafka

```bash
kafka-topics --create --bootstrap-server localhost:29092 --replication-factor 3 --partitions 3 --topic meutopico
```

Listar tópicos

```bash
kafka-topics --list --bootstrap-server localhost:29092
```

## Producer

Iniciar um produtor de mensagens

```bash
kafka-console-producer --broker-list localhost:29092 --topic meutopico
```

## Consumer

Iniciar um consumer de mensagens

```bash
kafka-console-consumer --bootstrap-server localhost:29092 --topic meutopico
```

Ler as mensagens desde o início

```bash
kafka-console-consumer --bootstrap-server localhost:29092 --topic meutopico --from-beginning
```

Especificando um grupo para escalar os consumers. Dica: Executar em pelo menos uns 3 consumers para testar a escalabilidade.

```bash
kafka-console-consumer --bootstrap-server localhost92 --topic meutopico --group a
```

## Info

Descrever informações em relação ao tópico a fim de saber número de réplicas e partições:

```bash
kafka-topics --describe --bootstrap-server localhost:29092 --topic meutopico
```

Descrever um grupo de consumidores:

```bash
kafka-consumer-groups --group a --bootstrap-server localhost:29092 --describe
```