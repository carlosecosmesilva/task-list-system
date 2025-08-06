# 🧠 Task List Frontend (Angular)

Este é o frontend do sistema de Lista de Tarefas, desenvolvido em **Angular 18** com **standalone components**, que consome uma API REST desenvolvida em **.NET 8**.

This is the frontend for the Task List system, built with **Angular 18** using **standalone components**, consuming a **.NET 8** REST API.

---Claro, Carlos! Aqui está um `README.md` para o **projeto Angular (`client/tasklist-frontend`)**, escrito em **português e inglês**, seguindo boas práticas, destacando como rodar, estrutura do projeto, comandos úteis e observações.

---

### 📄 `client/tasklist-frontend/README.md`

````markdown
# 🧠 Task List Frontend (Angular)

Este é o frontend do sistema de Lista de Tarefas, desenvolvido em **Angular**, que consome uma API REST desenvolvida em **.NET Core**.

This is the frontend for the Task List system, built with **Angular**, and consumes a **.NET Core** REST API.

---

## 🚀 Como executar / How to Run

### Pré-requisitos / Prerequisites

- Node.js (v18+ recomendado)
- Angular CLI
- API backend rodando (porta padrão: `https://localhost:5001`)

### Instalar dependências / Install dependencies

```bash
npm install
````

### Iniciar aplicação / Start application

```bash
ng serve
```

Acesse:
➡️ [http://localhost:4200](http://localhost:4200)

---

## 🗂️ Estrutura do Projeto / Project Structure

```
src/
├── app/
│   ├── tasks/
│   │   ├── pages/
│   │   │   ├── task-list/     # Lista de tarefas
│   │   │   └── task-form/     # Formulário de tarefas (novo/editar)
│   │   ├── services/          # Comunicação com a API
│   │   ├── models/            # Interfaces das entidades
│   │   ├── tasks.module.ts
│   │   └── tasks-routing-module.ts
│   ├── app.routes.ts          # Roteamento principal
│   └── app.ts / app.html      # Componente raiz
```

---

## 🔗 Comunicação com a API / API Communication

O `TaskService` realiza todas as chamadas HTTP:

* `GET /api/tasks` – listar tarefas
* `POST /api/tasks` – criar tarefa
* `PUT /api/tasks/{id}` – atualizar
* `DELETE /api/tasks/{id}` – excluir
* `PATCH /api/tasks/{id}/move?direction=up|down` – reordenar

> URL base da API: `https://localhost:5001/api/tasks`

---

## 💡 Funcionalidades / Features

* ✅ Listagem de tarefas ordenadas por prioridade
* ✅ Destaque para tarefas com custo ≥ R\$ 1.000,00
* ✅ Inclusão, edição e exclusão de tarefas
* ✅ Reordenação (subir/descer)
* ✅ Estilização simples com CSS

---

## 📦 Comandos úteis / Useful Commands

```bash
ng generate component tasks/pages/task-list
ng generate component tasks/pages/task-form
ng generate service tasks/services/task
ng generate module tasks --routing
```

---

## 📄 Licença / License

Projeto educacional sem fins comerciais.
Educational purpose only. No commercial license.

---

```

---

Se quiser, posso:
- **Salvar esse arquivo diretamente** no seu projeto
- **Adaptar para Markdown com formatação visual extra**
- Ou incluir seções de **build para produção (ng build)**

Deseja que eu gere ou salve esse arquivo para você agora?
```


## 🚀 Como executar / How to Run

### Pré-requisitos / Prerequisites

-   **Node.js** (v18+ recomendado / recommended)
-   **Angular CLI** (v18+)
-   **API backend** rodando na porta `https://localhost:5001`

### Instalar Angular CLI / Install Angular CLI

```bash
npm install -g @angular/cli
```

### Instalar dependências / Install dependencies

```bash
cd client/tasklist-frontend
npm install
```

### Iniciar aplicação / Start application

```bash
ng serve
```

ou / or

```bash
npm start
```

**Acesse:** ➡️ [http://localhost:4200](http://localhost:4200)

---

## 🗂️ Estrutura do Projeto / Project Structure

```
src/
├── app/
│   ├── tasks/
│   │   ├── pages/
│   │   │   ├── task-list/
│   │   │   │   ├── task-list-component.ts    # Lista de tarefas (standalone)
│   │   │   │   ├── task-list.html
│   │   │   │   └── task-list.css
│   │   │   └── task-form/
│   │   │       ├── task-form-component.ts    # Formulário (standalone)
│   │   │       ├── task-form.html
│   │   │       └── task-form.css
│   │   ├── services/
│   │   │   └── task-service.ts               # Comunicação com API
│   │   └── models/
│   │       └── task.model.ts                 # Interface Task
│   ├── app.routes.ts                         # Roteamento principal
│   ├── app.config.ts                         # Configuração da aplicação
│   ├── app.ts                                # Componente raiz (standalone)
│   └── app.html                              # Template principal
├── main.ts                                   # Bootstrap da aplicação
├── styles.css                                # Estilos globais
└── index.html
```

---

## 🔗 Comunicação com a API / API Communication

O `TaskService` realiza todas as chamadas HTTP para a API .NET:

### Endpoints disponíveis / Available endpoints:

| Método   | Endpoint                                 | Descrição / Description                  |
| -------- | ---------------------------------------- | ---------------------------------------- |
| `GET`    | `/api/task`                              | Listar todas as tarefas / List all tasks |
| `GET`    | `/api/task/{id}`                         | Buscar tarefa por ID / Get task by ID    |
| `POST`   | `/api/task`                              | Criar nova tarefa / Create new task      |
| `PUT`    | `/api/task/{id}`                         | Atualizar tarefa / Update task           |
| `DELETE` | `/api/task/{id}`                         | Excluir tarefa / Delete task             |
| `PATCH`  | `/api/task/{id}/move?direction=up\|down` | Reordenar tarefa / Reorder task          |

> **URL base da API:** `https://localhost:5001/api/task`

### Exemplo de uso / Usage example:

```typescript
// Listar tarefas
this.taskService.getAll().subscribe((tasks) => {
	console.log(tasks);
});

// Criar tarefa
const newTask = { name: "Nova tarefa", cost: 100, dueDate: "2025-12-31" };
this.taskService.create(newTask).subscribe();

// Mover tarefa
this.taskService.move(taskId, "up").subscribe();
```

---

## 💡 Funcionalidades / Features

### ✅ Implementadas / Implemented:

-   📋 Listagem de tarefas ordenadas por `displayOrder`
-   🎨 Destaque visual para tarefas com custo ≥ R$ 1.000,00
-   ➕ Criação de novas tarefas
-   ✏️ Edição de tarefas existentes
-   🗑️ Exclusão de tarefas com confirmação
-   🔄 Reordenação (mover para cima/baixo)
-   📱 Interface responsiva com Bootstrap 5
-   🚀 Arquitetura moderna com standalone components

### 🔄 Regras de Negócio / Business Rules:

-   Nome da tarefa deve ser único
-   Custo deve ser ≥ 0
-   Data limite é obrigatória
-   Ordem de exibição é automática

---

## 🛠️ Tecnologias Utilizadas / Technologies Used

-   **Angular 18** - Framework principal
-   **TypeScript** - Linguagem de programação
-   **Bootstrap 5** - Framework CSS
-   **RxJS** - Programação reativa
-   **Angular Router** - Roteamento
-   **Angular Forms** - Formulários reativos
-   **Standalone Components** - Arquitetura moderna

---

## 📦 Scripts Disponíveis / Available Scripts

```bash
# Desenvolvimento / Development
ng serve                    # Inicia servidor de desenvolvimento
ng build                   # Build para produção
ng test                     # Executa testes unitários
ng lint                     # Verifica qualidade do código

# Geração de código / Code generation
ng generate component nome  # Cria novo component
ng generate service nome    # Cria novo service
ng generate interface nome  # Cria nova interface
```

---

## 🔧 Configurações / Configuration

### Desenvolvimento / Development

O projeto está configurado para standalone components em `app.config.ts`:

```typescript
export const appConfig: ApplicationConfig = {
	providers: [
		provideZonelessChangeDetection(),
		provideRouter(routes),
		provideHttpClient(),
		provideClientHydration(withEventReplay()),
	],
};
```

### CORS Configuration

Certifique-se de que a API .NET permite CORS para `http://localhost:4200`.

---

## 🚨 Troubleshooting

### Problemas comuns / Common issues:

1. **Erro de CORS:**

    - Verificar se a API permite origem `http://localhost:4200`

2. **Erro 404 na API:**

    - Confirmar se a API está rodando em `https://localhost:5001`
    - Verificar se os endpoints estão corretos

3. **Erro de compilação:**
    ```bash
    ng serve --verbose  # Para debug detalhado
    ```

---

## 📖 Documentação Adicional / Additional Documentation

-   📚 [Documentação completa](../docs/README.md)
-   🔌 [API Documentation](../docs/api/README.md)
-   🏗️ [Arquitetura](../docs/Architecture.md)
-   🗄️ [Database Schema](../docs/database/schema.md)

---

## 🤝 Contribuição / Contributing

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📄 Licença / License

Projeto educacional sem fins comerciais.
Educational purpose only. No commercial license.

---

## 👨‍💻 Autor / Author

**Carlos Eduardo**  
🔗 [GitHub](https://github.com/carlosecosmesilva)

---
