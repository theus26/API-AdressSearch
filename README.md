
# API-AdressSearch
API feita em .net 7, Encontra Cep, Traz todas as Uf´s dos estados e todas as cidades dos estados.

Essa API foi desenvolvida para estudo e prática usando a arquitetura DDD :```Domain-Drive-Design```.


Além disso esse sistema foi desenvolvido com o intuito de ajudar quem não sabe o seu CEP, e muitas vezes não sabe onde encontrar, essa api disponibiliza o endpoint especifico para realizar essa consulta.
## Tecnologias Utilizadas

* .NET 7
* HttpClientFactory
* API-IBGE

## Pré-requisitos

* [.NET 7 SDK](https://markdownlivepreview.com/).


## Configuração

1. Clone o repositório:

```
git clone https://github.com/theus26/API-AdressSearch
```

2. Acesse o diretório do projeto:

```
cd seu-repositorio
```

4. Execute os seguintes comandos para restaurar as dependências e iniciar a API:

```
dotnet restore
dotnet run
```

5. Acesse a API em http://localhost:porta, onde "porta" é a porta configurada para a sua API.

## Funcionalidades

A API tem por finalidade encontrar o seu cep, mas tras algumas funcionalidades a mais, tais como, encontrar as UF´S de todos os estados brasileiros e trazer todas as cidades dos estados brasileiros.


Exemplo:

* `GET /AdressSearch/HealthCheck`: Endpoint teste para verificar o funcionamento da api.

* `GET /AdressSearch/GetAdressForInfo`: Pegar o CEP do usuario apartir de algumas propriedades passadas, são elas: "UF", "CITY", "LOGRE".

* `GET /AdressSearch/GetInfoState`: Consumindo a api do IBGE encontra as uf´s de todos os estados.

* `GET /AdressSearch/GetInfoCity/{UF}`: Encontra todas as cidades do estado especifico passado na url.


