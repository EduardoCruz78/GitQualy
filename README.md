# Projeto Desafio Qualyteam

## Descrição

Este projeto é uma aplicação que consiste em um backend em .NET 9.0 e um frontend utilizando React, que juntos fornecem uma interface para gerenciar Indicadores e Registros de Coleta. 

## Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (Versão recomendada: 18.x ou superior)
- [npm](https://www.npmjs.com/get-npm) (gerenciador de pacotes do Node.js)
- [SQLite](https://www.sqlite.org/download.html) (para manipulação do banco de dados)

## Instruções de execução

### 1. Backend (.NET 9.0)

1. Navegue até o diretório `Backend`.
2. Restaure os pacotes NuGet necessários para o backend com o comando:

```bash
 dotnet restore
```
3. Rode as migrações para o banco de dados SQLite com o comando:

```bash
 dotnet ef database update
```


### 2. Frontend (React)

1. Navegue até o diretório `qualyteam_frontend`.
2. Instale as dependências do projeto com o comando:

```bash
 npm install
```
3. Execute o projeto com os comandos:

```bash
 cd ..
 .\start-dev.ps1
```



## Rodando os testes unitários

1. Navegue até o diretório do projeto `Backend`.
2. Execute os testes com o comando:

```bash
 dotnet test
```

## Resetando o banco de dados

Para resetar o banco de dados e apagar todos os dados de `Indicadores` e `RegistrosDeColeta`, você pode executar os seguintes comandos diretamente no banco SQLite:

```sql
DELETE FROM Coletas;
DELETE FROM Indicadores;
```

