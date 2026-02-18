# CamaroteFoliaSync

Sistema de alta performance para controle de fluxo de foliões em camarotes de carnaval, desenvolvido com foco em Clean Architecture, CQRS e comunicação assíncrona via mensageria.

## Arquitetura

```
┌─────────────────────────────────────────────────────────────────────────┐
│                              CLIENTS                                     │
│                         (Swagger / Apps)                                 │
└─────────────────────────────────┬───────────────────────────────────────┘
                                  │
                                  ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                          API (Presentation)                              │
│                    FluxoController + Swagger                             │
└─────────────────────────────────┬───────────────────────────────────────┘
                                  │ MediatR
                                  ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                          APPLICATION                                     │
│              Commands (CQRS) │ Queries │ Handlers │ DTOs                │
└──────────────────┬──────────────────────────────────┬───────────────────┘
                   │                                  │
                   ▼                                  ▼
┌──────────────────────────────┐    ┌─────────────────────────────────────┐
│           DOMAIN             │    │          INFRASTRUCTURE             │
│  Entities │ Value Objects    │    │   EF Core │ MassTransit │ Repos     │
│  Events │ Exceptions         │    │                                     │
└──────────────────────────────┘    └──────────────┬──────────────────────┘
                                                   │
                              ┌────────────────────┴────────────────────┐
                              ▼                                         ▼
                    ┌──────────────────┐                    ┌───────────────────┐
                    │    SQL Server    │                    │     RabbitMQ      │
                    │   (Persistência) │                    │   (Mensageria)    │
                    └──────────────────┘                    └─────────┬─────────┘
                                                                      │
                                                                      ▼
                                                          ┌───────────────────┐
                                                          │      WORKER       │
                                                          │   (Consumers)     │
                                                          └───────────────────┘
```

## Tecnologias

| Camada         | Tecnologia               |
| -------------- | ------------------------ |
| API            | ASP.NET Core 9           |
| Documentação   | Swagger (Swashbuckle)    |
| CQRS           | MediatR                  |
| ORM            | Entity Framework Core 9  |
| Banco de Dados | SQL Server 2022          |
| Mensageria     | MassTransit 8 + RabbitMQ |
| Containers     | Docker Compose           |
| Testes         | xUnit + Moq              |

## Funcionalidades

- **Registro de Entrada**: Controla entrada de foliões via pulseira
- **Registro de Saída**: Controla saída de foliões
- **Consulta de Lotação**: Retorna lotação atual e percentual de ocupação
- **Validação de Capacidade**: Impede entrada quando camarote está cheio
- **Eventos em Tempo Real**: Publica eventos para RabbitMQ a cada entrada/saída
- **Worker Assíncrono**: Processa eventos para notificações, dashboards, etc.

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Como Executar

### 1. Subir a Infraestrutura

```bash
docker-compose up -d
```

Isso inicia:

- **SQL Server**: localhost:1433
- **RabbitMQ**: localhost:5672 (AMQP) / localhost:15672 (Dashboard)

### 2. Aplicar Migrations

```bash
dotnet ef database update --project CamaroteFoliaSync.Infrastructure --startup-project CamaroteFoliaSync.Api
```

### 3. Rodar a API

```bash
dotnet run --project CamaroteFoliaSync.Api
```

Acesse: http://localhost:5135/swagger

### 4. Rodar o Worker (outro terminal)

```bash
dotnet run --project CamaroteFoliaSync.Worker
```

### 5. Acessar o RabbitMQ Dashboard

- URL: http://localhost:15672
- Usuário: `guest`
- Senha: `guest`

## Endpoints

| Método | Endpoint                          | Descrição                  |
| ------ | --------------------------------- | -------------------------- |
| POST   | `/api/Fluxo/entrada`              | Registra entrada de folião |
| POST   | `/api/Fluxo/saida`                | Registra saída de folião   |
| GET    | `/api/Fluxo/lotacao/{camaroteId}` | Consulta lotação atual     |

### Exemplo de Request

```json
POST /api/Fluxo/entrada
{
  "camaroteId": "2291B729-C885-46D3-B3A2-56F8995BBD2A",
  "pulseiraId": "PULSEIRA-001"
}
```

### Exemplo de Response

```json
{
  "registroId": "87691d6b-8292-4713-afa5-fe5f01e0b6b0",
  "pulseiraId": "PULSEIRA-001",
  "tipoFluxo": "Entrada",
  "lotacaoAtual": 1,
  "capacidadeMaxima": 100,
  "dataHora": "2026-02-17T21:57:56.792Z"
}
```

## Testes

```bash
dotnet test
```

Cobertura:

- `RegistrarEntradaHandler` - Entrada válida
- `RegistrarEntradaHandler` - Folião já presente (erro)
- `RegistrarEntradaHandler` - Capacidade excedida (erro)

## Estrutura do Projeto

```
CamaroteFoliaSync/
├── CamaroteFoliaSync.Api/           # Controllers, Swagger, DI
├── CamaroteFoliaSync.Application/   # Commands, Queries, Handlers, DTOs
├── CamaroteFoliaSync.Domain/        # Entities, Value Objects, Events
├── CamaroteFoliaSync.Infrastructure/# EF Core, Repositories, MassTransit
├── CamaroteFoliaSync.Worker/        # Consumers de eventos
├── CamaroteFoliaSync.Tests/         # Testes unitários
└── docker-compose.yml               # SQL Server + RabbitMQ
```

## Padrões Utilizados

- **Clean Architecture**: Separação clara de responsabilidades
- **CQRS**: Commands e Queries separados via MediatR
- **Domain-Driven Design**: Entidades ricas, Value Objects, Domain Events
- **Repository Pattern**: Abstração do acesso a dados
- **Unit of Work**: Gerenciado pelo EF Core

## Autor

Desenvolvido como projeto de estudo para demonstrar arquitetura de software moderna com .NET.

---
