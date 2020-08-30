# Detalhes
## Instruções detalhadas sobre o projeto

### Criando um novo projeto .net core

Para criar um novo projeto .net core utilizar o seguinte comando:

**dotnet new** *tipo-do-projeto* *nome-do-projeto*

Exemplo:
```
dotnet new web-api basic-webapi-identity-netcore
```

### Instalação de pacotes do **dotnet**

Para instalar um pacote dentro do projeto, utilizar o instalador do dotnet:

**dotnet add package** *nome-do-pacote*

Exemplo: 
```
dotnet add package Microsoft.AspNetCore.Identity
```

### Instalação **Identity Framework**

Para poder utilizar o *Identity Framework* dentro do projeto é necessário fazer a instalação do seu pacote através do instalador de pacotes do dotnet:

```
dotnet add package Microsoft.AspNetCore.Identity
```

O *Identity Framework* é o componente responsável por criar usuários, regras e perfis necessários para autorização e autenticação, ele já traz classes pré definidas para serem utilizadas, como por exemplo classes para usuários e regras. Mas nada impede que a criação de clasess customizadas ou adicionar propriedades as classes já existentes. 

Mais detalhes em: [Identity Asp.Net Core](https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio) 

### Instalação **Entity Framework**

Para poder utilizar o *Entity Framework* dentro do projeto é necessário fazer a instalação do seu pacote através do instalador de pacotes do dotnet:

```
dotnet add package Microsoft.EntityFrameworkCore
```

Além da instalação do *EntityFramework* é necessário fazer a instalação de outro pacote o **Microsoft.AspNetCore.Identity.EntityFrameworkCore** ele irá fazer a comunicação entre as classes do *Identity* e do *Entity*.

```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

O *Entity Framework* é o componente responsável pela comunicação com o banco de dados. Ele que traduz as classes na pasta *Models* para a criação de tabelas no banco de dados. 

Mais informações: [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)


### Instalação **Migrations**

Para poder utilizar o *Migrations* dentro do projeto é necessário fazer a instalação do seu pacote através do instalador de pacotes do dotnet:

```
dotnet add package Microsoft.EntityFrameworkCore.Design
```

O *Migrations* é responsável por fazer manter as atualizações no banco de dados, e versionar o banco de dados, com ele é possível fazer o controle toda vez que campos são adicionados nas tabelas, também é possível executar rotinas de preenchimento de dados em tabelas quando uma alteração é realizada.


Mais informações em: [Migrations Overview](https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)



### Instalação da CLI para Entity Framework

Para poder executar comandos atravé da linha de comandos (CLI)  para o *Entity Framework* é necessário fazer a instalação das ferramentas através do instalador do *net core*:

```
dotnet tool install --global dotnet-ef
```

Após a instalação dessa ferramenta será possível executar os comandos para atualizar o banco de dados e executar *Migrations*.
- Como por exemplo o comando **database update**, que irá aplicar as alterações no banco de dados, quando forem alteradas as classes.

- Criação de uma nova versão:
```
dotnet ef migrations add inicial
```

- Atualizando o banco de dados:
```
dotnet ef database update -v
```


### Instalação do pacote do PostgreSql

Para poder utilizar o banco de dados PostgreSql é necessário fazer a instalação do seu pacote através do instalador de pacotes do dotnet:

```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

Esse pacote irá fazer a comunicação com o banco de dados do tipo PostgreSQL, mas nada impede que outros bancos de dados sejam utilizados como o MySql, desde que sejam instalados os seus respecitvos pacotes.

- *(Opcional)* Para alterar o banco de dados, altere o arquivo *StartUp.cs*
```
services.AddDbContext<ApplicationDBContext>(o => o.UseNpgsql(connString));
```
- Não se esqueça também de alterar a connection string.
