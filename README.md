<p align="center">
  <h1 align="center">CBF Demo Api</h1>
  <p align="center">
    01/2022 - APIs e Web Services - Trabalho Final  
  </p>
  <p align="center">
  Trabalho final apresentado para a disciplina APIs e Web Services (AWS)
  </p>
</p>

________________

<br />

## Índice

1. [Sobre a aplicação](#sobre-a-aplicação)
    - [Principais tecnologias](#principais-tecnologias)
2. [Instalação](#instalação)
    - [Pré Requisitos](#pré-requisitos)
    - [Como instalar](#como-instalar)
    - [Como desinstalar](#como-desinstalar)
3. [Como utilizar](#como-utilizar)
4. [Build Local](#build-local)
    - [Pré Requisitos](#pré-requisitos)
    - [Como executar](#como-executar)
5. [Como contribuir](#como-contribuir)
6. [Licença](#licença)

## Sobre a aplicação

Foi desenvolvida uma API que possibilita o gerenciamento de pequenas ligas de futebol. É possível cadastrar vários Times e seus Jogadores, bem como as transferências realizadas entre times. Também é possível cadastrar campeonatos e programar a execução de partidas entre times. Para as partidas cadastradas é possível lançar cartões (advertências), gols, substituições, bem como início e fim das partidas e do intervalo. Quando buscamos os dados das partidas todos os dados dos eventos referentes a essas partidas são informados.


### Principais Tecnologias

A API está integrada a um serviço de filas que disponibiliza mensagens com os dados de atualização das partidas toda vez que algum evento é registrado ou caso uma partida seja atualizada.
O .NET 6.0 foi usado para desenvolver a API e uma fila baseada em RabbitMQ rodando em contêiner Docker. SQL Server foi escolhido como banco de dados, para o qual desenvolvemos um modelo relacional.

- [.NET 6.0](https://dotnet.microsoft.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Docker](https://www.docker.com/)
- [Visual Studio Community](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [Visual Studio Code](https://code.visualstudio.com/)

### Pré-Requisitos

- [.NET 6.0](https://dotnet.microsoft.com/) instaldo e configurado.
- Banco de dados rodando e configurado (Script de criação de tabelas disponível nesse repo)
- Docker configurado na máquina
- RabbitMQ rodando e configurado

As configurações de conex

### Como instalar

Passo a passo de como instalar a aplicação.

- Utilizando o npm

  ```sh
  npm i --save nome-da-aplicação
  ```

### Como desinstalar

Instruções para remover a aplicação.

## Como utilizar

Informações mais detalhadas sobre como e quando utilizar as funcionalidades da aplicação, como fazer configurações personalizadas e demais observações importantes relacionadas a esse contexto.

## Build local

[Modelo de dados](./.assets/Modelo_Banco.jpg)
Instruções sobre como executar a aplicação em um ambiente de desenvolvimento local.

### Pré-Requisitos

Liste, caso existam, as tecnologias que deverão ser instaladas para que o usuário consiga executar a aplicação em seu ambiente de desenvolvimento local. Mostre de forma clara e direta como instalar cada um dos pré requisitos.

### Como executar

Mostre o passo a passo necessário para executar a aplicação.

Exemplo:

1. Gere uma chave de acesso gratuita no site [https://example.com](https://example.com)
2. Clone o repositório
   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```
3. Installe as dependências
   ```sh
   npm install
   ```
4. Insira sua chave de acesso no arquivo `config.js`
   ```js
   const API_KEY = "ENTER YOUR API";
   ```
5. Inicialize a aplicação
   ```sh
   npm start
   ```

## Como contribuir

Forneça instruções para que as pessoas saibam como contribuir para o projeto. Você pode colocar um link para um guia de contribuição, por exemplo.

## Licença

Informe a licença de códido utilizada pelo projeto.
