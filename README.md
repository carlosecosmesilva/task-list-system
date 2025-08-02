# ✅ Sistema de Lista de Tarefas / 📝 Task List System

Projeto fullstack com backend em **.NET 8** e banco de dados **PostgreSQL**, que permite o **cadastro, listagem, edição, exclusão e reordenação** de tarefas de forma interativa.

A fullstack project with **.NET 8 backend** and **PostgreSQL database**, allowing users to **create, list, edit, delete and reorder** tasks interactively.

---

## 📌 Funcionalidades / Features

-   📋 **Listar tarefas** com ordenação personalizada  
    **List tasks** with custom `DisplayOrder`

-   ➕ **Incluir nova tarefa** com nome único  
    **Add new task** with unique name

-   ✏️ **Editar tarefa** (nome, custo e data limite)  
    **Edit task** (name, cost and due date)

-   ❌ **Excluir tarefa** com confirmação  
    **Delete task** with confirmation

-   🔼🔽 **Reordenar tarefas** com botões ou drag-and-drop  
    **Reorder tasks** using buttons or drag-and-drop

-   💰 **Destaque visual** para tarefas com custo ≥ R$ 1.000,00  
    **Visual highlight** for tasks with cost ≥ R$ 1,000.00

---

## 🧱 Estrutura do Projeto / Project Structure

```bash
task-list-system/
├── client/                      # Frontend Angular / Angular frontend
├── server/TaskListApp/
│   ├── TaskList.API/           # Web API (Controllers)
│   ├── TaskList.Application/   # Regras de negócio / Business logic
│   ├── TaskList.Domain/        # Entidades e interfaces / Entities & interfaces
│   ├── TaskList.Infrastructure/ # Acesso a dados / Data access (EF Core)
│   └── docker-compose.yml     # Container PostgreSQL
├── .gitignore
└── README.md
```

---

## 🚀 Tecnologias / Technologies

### Backend

-   **.NET 8** Web API
-   **Entity Framework Core 9.0** - ORM
-   **PostgreSQL 16** - Banco de dados / Database
-   **Npgsql** - Provider PostgreSQL para .NET
-   **Docker Compose** - Containerização / Containerization
-   **Swagger/OpenAPI** - Documentação da API / API documentation

### Frontend (Planejado / Planned)

-   **Angular** - Framework frontend
-   **TypeScript** - Linguagem / Language
-   **Bootstrap** - Estilização / Styling

### Arquitetura / Architecture

-   **Clean Architecture** (simplificada / simplified)
-   **Repository Pattern**
-   **Dependency Injection**

---

## 🏗️ Esquema do Banco / Database Schema

### Tabela Tarefas / Tasks Table

-   **Id** (int) - Chave primária / Primary key
-   **Nome** (varchar) - Nome único / Unique name (required)
-   **Custo** (decimal) - Valor monetário / Cost value (required)
-   **DataLimite** (timestamp) - Data limite / Due date (required)
-   **OrdemExibicao** (int) - Ordem única / Unique display order (required)

---

## 🐳 Como Executar / How to Run

### Pré-requisitos / Prerequisites

-   **.NET 8 SDK**
-   **Docker & Docker Compose**
-   **PostgreSQL** (opcional se usar Docker / optional if using Docker)

### 1. Clonar o repositório / Clone repository

```bash
git clone https://github.com/carlosecosmesilva/task-list-system.git
cd task-list-system
```

### 2. Subir o banco PostgreSQL / Start PostgreSQL database

```bash
cd server/TaskListApp
docker compose up -d
```

### 3. Restaurar pacotes / Restore packages

```bash
dotnet restore
```

### 4. Aplicar migrations / Apply migrations

```bash
dotnet ef database update -p TaskList.Infrastructure -s TaskList.API
```

### 5. Executar a API / Run API

```bash
dotnet run --project TaskList.API
```

### 6. Acessar a aplicação / Access application

**API estará disponível em / API will be available at:**

-   `https://localhost:5001`
-   `http://localhost:5000`

**Documentação Swagger / Swagger Documentation:**

-   `https://localhost:5001/swagger`

---

## 📚 Endpoints da API / API Endpoints

### Tarefas / Tasks

-   `GET /api/tasks` - Listar todas / List all tasks
-   `GET /api/tasks/{id}` - Buscar por ID / Get by ID
-   `POST /api/tasks` - Criar nova / Create new task
-   `PUT /api/tasks/{id}` - Atualizar / Update task
-   `DELETE /api/tasks/{id}` - Excluir / Delete task
-   `PATCH /api/tasks/{id}/reorder` - Reordenar / Reorder task

---

## 🎯 Regras de Negócio / Business Rules

### Validações / Validations

-   ✅ **Nome único** - Não pode haver tarefas com mesmo nome  
    **Unique name** - No duplicate task names allowed

-   ✅ **Campos obrigatórios** - Nome, Custo, Data Limite  
    **Required fields** - Name, Cost, Due Date

-   ✅ **Ordem única** - Cada tarefa tem posição única na lista  
    **Unique order** - Each task has unique position in list

-   ✅ **Edição segura** - Nome editado não pode conflitar  
    **Safe editing** - Edited name cannot conflict

### Funcionalidades / Features

-   🔄 **Reordenação automática** ao mover tarefas  
    **Auto-reordering** when moving tasks

-   💡 **Destaque visual** para custos altos (≥ R$ 1.000)  
    **Visual highlight** for high costs (≥ R$ 1,000)

-   ⚠️ **Confirmação** antes de excluir  
    **Confirmation** before deletion

---

## 🧪 Desenvolvimento / Development

### Adicionar nova migration / Add new migration

```bash
dotnet ef migrations add NomeDaMigration -p TaskList.Infrastructure -s TaskList.API
```

### Atualizar banco / Update database

```bash
dotnet ef database update -p TaskList.Infrastructure -s TaskList.API
```

### Executar testes / Run tests

```bash
dotnet test
```

---

## 📦 Roadmap / TODOs

### Backend ✅

-   [x] Configurar Clean Architecture
-   [x] Implementar entidades e DbContext
-   [x] Criar Controllers e endpoints
-   [x] Configurar Swagger
-   [x] Implementar validações

### Frontend 🚧

-   [ ] Configurar projeto Angular
-   [ ] Criar componentes de lista
-   [ ] Implementar formulários
-   [ ] Adicionar drag-and-drop
-   [ ] Estilizar interface

### Melhorias Futuras / Future Improvements ⭐

-   [ ] Autenticação de usuários / User authentication
-   [ ] Testes unitários / Unit tests
-   [ ] Testes de integração / Integration tests
-   [ ] Cache e performance / Caching & performance
-   [ ] Logs estruturados / Structured logging
-   [ ] Deploy automatizado / Automated deployment

---

## 🤝 Contribuição / Contributing

1. **Fork** o projeto / Fork the project
2. Crie uma **branch** para sua feature (`git checkout -b feature/MinhaFeature`)
3. **Commit** suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. **Push** para a branch (`git push origin feature/MinhaFeature`)
5. Abra um **Pull Request**

---

## 📄 Licença / License

Este projeto é licenciado sob a **MIT License**.  
This project is licensed under the **MIT License**.

---

## 👨‍💻 Autor / Author

**Carlos Eduardo**

-   GitHub: [@carlosecosmesilva](https://github.com/carlosecosmesilva)

---

## 🙏 Agradecimentos / Acknowledgments

-   Equipe do **.NET** pelo excelente framework
-   Comunidade **PostgreSQL** pelo banco robusto
-   Equipe **Angular** pelo framework frontend
