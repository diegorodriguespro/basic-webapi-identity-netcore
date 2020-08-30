# basic-webapi-identity-netcore
## Projeto de uma webapi básica utilizando identity com c# e net core

<h4 align="center"> 
	🚧  Projeto 🚀 Em construção...  🚧
</h4>

Tabela de conteúdos
=================
<!--ts-->
   * [Sobre](#Sobre)   
   * [Como usar](#como-usar)
      * [Pre Requisitos](#pré-requisitos)
      * [Instalação](#instalação)  
      * [Executar](#executar)  
   * [Tecnologias](#tecnologias)
   * [Estrutura do projeto](./docs/estrutura.md)
<!--te-->

## Sobre
- Login simples, usuário e senha
- Login com token JWT
    1. Efetua o login com usuário 
    2. Retorna o token JWT
- Registro de usuário
- Configuração de autenticação e autorização

## 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- Net Core
- C#
- IdentityFramework
- PostgreSql

## Como usar

### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
[Git](https://git-scm.com), [.Net Core](https://dotnet.microsoft.com/). 
O banco de dados utilizado foi o [Postgres](https://www.postgresql.org/).
Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/)

### Instalação

- Clonando o respositório

```bash
# Clone este repositório
$ git clone https://github.com/diegorodriguespro/basic-webapi-identity-netcore
```

- instalar a CLI do *EntityFramework*
- somente se ela já não estiver sido instalada
```bash
dotnet tool install --global dotnet-ef
```

- Acessar a pasta do projeto
```bash
# Acesse a pasta do projeto no terminal/cmd
$ cd basic-webapi-identity-netcore
```

- Reinstalar dependências

```bash
$ dotnet restore

$ dotnet build
```

- Criar e atualizar banco de dados
Crie um banco de dados no PostgreSQL, depois de criado, altere a string de conexão.

- Alterar a string de conexão
Alterar o arquivo **appsettings.json** onde fica armazenada a string de conexão. Alterar para a localização do seu banco de dados.
```JSON
"ConnectionStrings": {		    "Default": "Provider=PostgreSQL OLE DB Provider;Data Source=0.0.0.0;location=databasename;User ID=dbuser;password=dbuserpassword;timeout=1000;" 
 }
```

-  Atualize o banco de dados
-  Este comando irá criar a estrutura de tabelas de usuários no banco de dados. 
```bash
dotnet ef database update -v
```

### Executar
Executando a API

```bash
# Execute a aplicação em modo de desenvolvimento
$ dotnet run

# O servidor inciará na porta:5000 - acesse <http://localhost:5000>
```

### [Estrutura](./docs/estrutura.md)
Estrutura do projeto e descrição das classes 

### [Instalação](./docs/instalacao.md)
Preparação e instalação do ambiente



