# 🎮 FCG Tech Challenge - Fase 1
## FIAP Cloud Games Platform

### 📋 Requisitos Implementados

#### ✅ Funcionalidades Obrigatórias
- **Cadastro de usuários** com nome, email e senha
- **Validação de email** e **senha segura** (mínimo 8 caracteres com números, letras e caracteres especiais)
- **Autenticação JWT** com dois níveis de acesso:
  - 👤 **Usuário** - Acesso à plataforma e biblioteca de jogos
  - 👑 **Administrador** - Cadastrar jogos, administrar usuários
- **Biblioteca de jogos** adquiridos
- **API REST** em .NET 8 com Entity Framework Core

#### 🏗️ Arquitetura
- **DDD** (Domain-Driven Design)
- **Clean Architecture** com separação em camadas
- **Entity Framework Core** com SQLite
- **FluentValidation** para validações
- **JWT Authentication**

### 🚀 Como Executar

#### Pré-requisitos
- .NET 8 SDK
- Visual Studio Code ou IDE de sua preferência

#### Execução Local
```bash
# 1. Restaurar pacotes
dotnet restore

# 2. Build do projeto
./build.sh

# 3. Executar a API
./run.sh

# 4. Acessar a API
# Swagger: https://localhost:7000/swagger
# API: https://localhost:7000/api
