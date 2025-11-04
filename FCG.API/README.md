# FCG Tech Challenge - Fase 1

🎮 FIAP Cloud Games (FCG)

FIAP Cloud Games (FCG) é uma plataforma inovadora voltada para a venda de jogos digitais e a gestão de servidores para partidas online. O projeto tem como objetivo oferecer uma experiência completa para jogadores e administradores, integrando funcionalidades de compra, aluguel, autenticação e gerenciamento de conteúdo em nuvem.

🚀 Fase Atual do Projeto
Esta é a primeira fase de desenvolvimento da plataforma, com foco nas funcionalidades essenciais de cadastro e autenticação de usuários. Os principais recursos implementados até o momento incluem:
- ✅ Cadastro de usuários comuns e administradores
- 🔐 Autenticação segura utilizando JWT (JSON Web Token)
- 🧩 Estrutura inicial para controle de acesso e perfis
- 🛠️ É uma base para futuras integrações com módulos de jogos, biblioteca pessoal e sistema de pagamento


## 🍎 macOS Setup

### Pré-requisitos
- .NET 8 SDK
- Visual Studio 2022, Visual Studio Code ou Rider

### Estrutura Criada ✅

- ✅ Solução .NET 8
- ✅ 5 projetos organizados em DDD
- ✅ Dependências configuradas
- ✅ Pacotes NuGet instalados
- ✅ Banco de dados SQLite

### Primeiros Passos

```bash
# Restaurar pacotes
dotnet restore

# Build do projeto
dotnet build

# Executar API
dotnet run --project FCG.API

# Acesse: https://localhost:7000/swagger
