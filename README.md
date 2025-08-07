# ✅ Sistema de Lista de Tarefas / 📝 Task List System

Projeto fullstack com backend em **.NET 8** e banco de dados **PostgreSQL**, que permite o **cadastro, listagem, edição, exclusão e reordenação** de tarefas de forma interativa.

A fullstack project with **.NET 8 backend** and **PostgreSQL database**, allowing users to **create, list, edit, delete and reorder** tasks interactively.

---

## 🌐 **Links de Produção / Production Links**

-   **🖥️ Aplicação Web / Web App:** https://tasklist-frontend-lovat.vercel.app/tasks
-   **📚 API Documentation (Swagger):** https://task-list-system.onrender.com/swagger/index.html
-   **🔗 Repositório GitHub / GitHub Repository:** https://github.com/carlosecosmesilva/task-list-system

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
├── client/tasklist-frontend/    # Frontend Angular / Angular frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── tasks/          # Módulo de tarefas / Tasks module
│   │   │   ├── shared/         # Componentes compartilhados / Shared components
│   │   │   └── core/           # Serviços core / Core services
│   │   └── environments/       # Configurações de ambiente / Environment configs
│   ├── angular.json
│   └── package.json
├── server/TaskListApp/
│   ├── TaskList.API/          # Web API (Controllers)
│   ├── TaskList.Application/  # Regras de negócio / Business logic
│   ├── TaskList.Domain/       # Entidades e interfaces / Entities & interfaces
│   ├── TaskList.Infrastructure/ # Acesso a dados / Data access (EF Core)
│   ├── Dockerfile             # Container para deploy / Deploy container
│   └── docker-compose.yml     # Container PostgreSQL local / Local PostgreSQL
├── .gitignore
└── README.md
```

---

## 🚀 Tecnologias / Technologies

### Frontend ✅

-   **Angular 18** - Framework frontend
-   **Angular Material** - Componentes UI / UI Components
-   **TypeScript** - Linguagem / Language
-   **SCSS** - Estilização / Styling
-   **RxJS** - Programação reativa / Reactive programming

### Backend ✅

-   **.NET 8** Web API
-   **Entity Framework Core 9.0** - ORM
-   **PostgreSQL 16** - Banco de dados / Database
-   **Npgsql** - Provider PostgreSQL para .NET
-   **Docker** - Containerização / Containerization
-   **Swagger/OpenAPI** - Documentação da API / API documentation

### Deploy / Hosting ✅

-   **Frontend:** [Vercel](https://vercel.com) - Deploy automático / Automated deployment
-   **Backend:** [Render](https://render.com) - Container deployment
-   **Database:** Render PostgreSQL - 500MB gratuito / 500MB free tier

### Arquitetura / Architecture

-   **Clean Architecture** (simplificada / simplified)
-   **Repository Pattern**
-   **Dependency Injection**
-   **CORS** configurado para produção / CORS configured for production

---

## 🏗️ Esquema do Banco / Database Schema

### Tabela Tarefas / Tasks Table

-   **Id** (int) - Chave primária / Primary key
-   **Nome** (varchar) - Nome único / Unique name (required)
-   **Custo** (decimal) - Valor monetário / Cost value (required)
-   **DataLimite** (timestamp) - Data limite / Due date (required)
-   **OrdemExibicao** (int) - Ordem única / Unique display order (required)

---

## 🌍 **Configuração de Ambiente / Environment Setup**

### **Produção / Production**

```typescript
// environment.prod.ts
export const environment = {
	production: true,
	apiUrl: "https://task-list-system.onrender.com/api",
	apiTimeout: 10000,
	enableLogging: false,
	appName: "Task List System",
	version: "1.0.0",
};
```

### **Desenvolvimento / Development**

```typescript
// environment.development.ts
export const environment = {
	production: false,
	apiUrl: "http://localhost:5000/api",
	apiTimeout: 30000,
	enableLogging: true,
	appName: "Task List System - Dev",
	version: "1.0.0",
};
```

---

## 🐳 Como Executar Localmente / How to Run Locally

### Pré-requisitos / Prerequisites

-   **.NET 8 SDK**
-   **Node.js 18+**
-   **Angular CLI**
-   **Docker & Docker Compose**
-   **PostgreSQL** (opcional se usar Docker / optional if using Docker)

### 1. Clonar o repositório / Clone repository

```bash
git clone https://github.com/carlosecosmesilva/task-list-system.git
cd task-list-system
```

### 2. Backend Setup

```bash
cd server/TaskListApp

# Subir o banco PostgreSQL / Start PostgreSQL database
docker compose up -d

# Restaurar pacotes / Restore packages
dotnet restore

# Aplicar migrations / Apply migrations
dotnet ef database update -p TaskList.Infrastructure -s TaskList.API

# Executar a API / Run API
dotnet run --project TaskList.API
```

### 3. Frontend Setup

```bash
cd client/tasklist-frontend

# Instalar dependências / Install dependencies
npm install

# Executar em modo de desenvolvimento / Run in development mode
ng serve

# Acessar: http://localhost:4200
```

### 4. Acessar aplicações / Access applications

**API Backend:**

-   `https://localhost:5001`
-   `http://localhost:5000`
-   **Swagger:** `https://localhost:5001/swagger`

**Frontend:**

-   `http://localhost:4200`

---

## 🚀 **Deploy em Produção / Production Deployment**

### **Frontend (Vercel)**

```bash
cd client/tasklist-frontend

# Instalar Vercel CLI / Install Vercel CLI
npm i -g vercel

# Deploy
vercel --prod
```

**Configurações automáticas / Auto configurations:**

-   ✅ Build command: `npm run build`
-   ✅ Output directory: `dist/tasklist-frontend`
-   ✅ HTTPS automático / Auto HTTPS
-   ✅ CDN global / Global CDN

### **Backend (Render)**

**Via GitHub Integration:**

1. Conectar repositório no [Render](https://render.com)
2. Criar **Web Service**
3. Configurações:
    - **Root Directory:** `server/TaskListApp`
    - **Environment:** Docker
    - **Plan:** Free

**Variáveis de ambiente / Environment variables:**

```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:10000
ConnectionStrings__DefaultConnection=postgresql://user:pass@host:port/db
```

### **Database (Render PostgreSQL)**

1. Criar **PostgreSQL** no Render
2. Copiar **Internal Database URL**
3. Configurar no Web Service

---

## 📚 Endpoints da API / API Endpoints

### Tarefas / Tasks

| Método | Endpoint         | Descrição / Description       |
| ------ | ---------------- | ----------------------------- |
| GET    | `/api/task`      | Listar todas / List all tasks |
| GET    | `/api/task/{id}` | Buscar por ID / Get by ID     |
| POST   | `/api/task`      | Criar nova / Create new task  |
| PUT    | `/api/task/{id}` | Atualizar / Update task       |
| DELETE | `/api/task/{id}` | Excluir / Delete task         |

**📚 Documentação completa / Full documentation:**  
https://task-list-system.onrender.com/swagger/index.html

---

## 🧪 **Testar a Aplicação / Test the Application**

### **Testar API em Produção / Test Production API**

```bash
# Listar tarefas / List tasks
curl https://task-list-system.onrender.com/api/task

# Criar tarefa / Create task
curl -X POST https://task-list-system.onrender.com/api/task \
  -H "Content-Type: application/json" \
  -d '{"title":"Nova Tarefa","description":"Descrição","cost":100}'
```

### **Testar Frontend**

Acesse: https://tasklist-frontend-lh9avykr5-carlosecosmesilvas-projects.vercel.app/tasks

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

## 📈 **Status dos Serviços / Service Status**

-   ✅ **Frontend:** Ativo no Vercel / Active on Vercel
-   ✅ **Backend:** Ativo no Render / Active on Render
-   ✅ **Database:** PostgreSQL operacional / PostgreSQL operational
-   ✅ **CORS:** Configurado corretamente / Properly configured
-   ✅ **SSL:** HTTPS automático / Auto HTTPS

### **Limitações do Plano Gratuito / Free Tier Limitations**

-   ⏰ **Cold Start:** Primeira requisição ~30 segundos / First request ~30 seconds
-   💤 **Sleep Mode:** Serviços dormem após 15-30min inatividade / Services sleep after 15-30min inactivity
-   💾 **Database:** 500MB PostgreSQL gratuito / 500MB free PostgreSQL
-   🔄 **Deploy:** Automático via Git / Auto deploy via Git

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
-   [x] Deploy no Render

### Frontend ✅

-   [x] Configurar projeto Angular
-   [x] Criar componentes de lista
-   [x] Implementar formulários
-   [x] Estilizar interface com Material
-   [x] Deploy no Vercel

### Melhorias Futuras / Future Improvements ⭐

-   [ ] Adicionar drag-and-drop para reordenar
-   [ ] Autenticação de usuários / User authentication
-   [ ] Testes unitários / Unit tests
-   [ ] Testes de integração / Integration tests
-   [ ] Cache e performance / Caching & performance
-   [ ] Logs estruturados / Structured logging
-   [ ] Notificações push / Push notifications

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
-   Email: [carlos.eduardo.cs@outlook.com](carlos.eduardo.cs@outlook.com)

---

## 🙏 Agradecimentos / Acknowledgments

-   Equipe do **.NET** pelo excelente framework
-   Comunidade **PostgreSQL** pelo banco robusto
-   Equipe **Angular** pelo framework frontend
-   **Vercel** e **Render** pelos serviços gratuitos de hospedagem

---

**🎉 Projeto totalmente funcional e em produção! / Fully functional project in production!**
