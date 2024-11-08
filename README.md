# Pokédex Back

O Pokédex Back é o resultado da segunda parte do meu teste técnico para a vaga de Desenvolvedor Full Stack na Sistema ESO; e permite aos usuários:
* criar uma conta;
* capturar Pokémons; e
* libertar pokemons.

Você pode acessá-la [neste link](https://pokedex-seven-inky.vercel.app)

## Tenologias Utilizadas
- **ASP.NET Core 6**: Framework para construir a API RESTful.
- **Entity Framework Core**: ORM para o mapeamento de dados e comunicação com o banco MySQL.
- **MySQL**: Banco de dados para armazenar informações usuários e capturas.
- **SignalR**: Biblioteca para comunicação em tempo real, utilizada para atualização de dados sem necessidade de recarregar a página.
- **JWT**: Autenticação baseada em tokens para proteger as rotas da API.

## Funcionalidades

  - **Cadastro e Login de Usuários**: Sistema de autenticação seguro com JWT, onde usuários podem se registrar e logar.
  - **Captura de Pokémon**: Usuários podem capturar Pokémon, com um limite de capturas definido por usuário.
  - **Visualização e Sincronização em Tempo Real**: Exibe em tempo real os Pokémon capturados por cada usuário, utilizando SignalR para atualizações automáticas na tela dos usuários.

## Endpoints

### rotas sem autenticação
- /auth/register: registro de usuários
- auth/login: login de usuários e obtenção de token JWT

### rotas protegidas
- /pokemon/captured-by/{id}: retorna os pokémons capturados por um usuário específico
- /pokemon/captured: retorna todos os pokémons capturados
- /user/update: atualiza dados do usuário
- /user/delete: exclui a conta do usuário (requer o envio da senha)

## Segurança
Este projeto utiliza JWT (JSON Web Tokens) para autenticação de rotas seguras, protegendo endpoints de CRUD e capturas, garantindo que apenas usuários autenticados possam acessar essas funcionalidades.

## Como Executar o Projeto

### Pré-requisitos
- .NET 6 SDK ou superior
- MySQL Server: Configurado para armazenar dados da Pokédex
- Ferramenta de Gerenciamento de Pacotes NuGet
- Ferramenta de Migração de Banco de Dados do Entity Framework

### Passos

1. Clone o repositório:
```bash
git clone https://github.com/BeatrizNeaime/pokedex-back.git
```

2. Configuração do Banco de Dados MySQL: Crie um banco de dados no MySQL para a aplicação e configure a string de conexão no appsettings.json:
```json
   {
     "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=pokedex_db;User Id=seu_usuario;Password=sua_senha;"
     },
     "JWT": {
        "Secret": "sua_chave_secreta_jwt",
        "Issuer": "pokedex-api"
     }
   }
```

3. Aplicação das Migrações: Execute as migrações para configurar as tabelas no banco de dados.
```bash
 dotnet ef migrations add tables
 dotnet ef database update
```

4. Execute a Aplicação
 ```bash
   dotnet watch run
 ```
