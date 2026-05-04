# 📋 DesafioControle de Atendimentos — W5i

Sistema de gerenciamento de chamados de suporte e solicitações internas, desenvolvido em **C# com ASP.NET Core 8** e banco de dados **SQLite**.

---

## 🧰 Tecnologias Utilizadas

| Tecnologia | Versão |
|---|---|
| C# / ASP.NET Core | .NET 8 |
| Entity Framework Core | 8.0 |
| SQLite | via EF Core |
| Swagger / OpenAPI | 6.6.2 |

---

## 📁 Estrutura do Projeto

```
ControleAtendimento.API/
├── Controllers/          # Endpoints da API (Setores, Prioridades, Chamados)
├── Models/               # Entidades do domínio
├── Data/                 # Contexto do banco (EF Core)
├── Migrations/           # Migrations geradas automaticamente
├── appsettings.json      # Configurações da aplicação
└── Program.cs            # Ponto de entrada e configuração de serviços
```

---

## 🚀 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado
- Git (opcional)

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/w5i-controle-atendimento.git
cd w5i-controle-atendimento

# 2. Acesse a pasta da API
cd ControleAtendimento.API

# 3. Execute as migrations para criar o banco de dados
dotnet ef database update

# 4. Inicie a aplicação
dotnet run
```

> O banco de dados `atendimentos.db` (SQLite) será criado automaticamente na pasta da API.

### Acessar o Swagger

Com a aplicação rodando, acesse no navegador:

```
https://localhost:{porta}/swagger
```

A porta será exibida no terminal após o `dotnet run`.

---

## 🗄️ Script SQL

Caso prefira criar o banco manualmente (sem usar as Migrations), utilize o arquivo [`schema.sql`](./schema.sql) na raiz do repositório.

```bash
# Exemplo com SQLite CLI
sqlite3 atendimentos.db < schema.sql
```

---

## 📡 Endpoints da API

### Setores

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/setores` | Cadastrar novo setor |
| `GET` | `/setores` | Listar todos os setores |

**Exemplo — Criar setor:**
```json
POST /setores
{
  "nome": "TI"
}
```

---

### Prioridades

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/prioridades` | Cadastrar nova prioridade |
| `GET` | `/prioridades` | Listar todas as prioridades |

**Exemplo — Criar prioridade:**
```json
POST /prioridades
{
  "nome": "Alta",
  "tempoEstimadoHoras": 2
}
```

---

### Chamados

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/chamados` | Criar novo chamado (status inicial: Aberto) |
| `GET` | `/chamados` | Listar chamados com status, tempo e alerta de atraso |
| `PUT` | `/chamados/{id}/iniciar` | Check-in: registra data/hora de início |
| `PUT` | `/chamados/{id}/finalizar` | Check-out: registra data/hora de fim e solução |

**Exemplo — Criar chamado:**
```json
POST /chamados
{
  "titulo": "Sistema fora do ar",
  "descricao": "O sistema de vendas está inacessível",
  "setorId": 1,
  "prioridadeId": 3
}
```

**Exemplo — Finalizar chamado:**
```json
PUT /chamados/1/finalizar
"Reinicialização do servidor resolveu o problema."
```

**Exemplo — Resposta da listagem:**
```json
[
  {
    "id": 1,
    "titulo": "Sistema fora do ar",
    "setor": "TI",
    "prioridade": "Alta",
    "status": "EmAtendimento",
    "tempoTotalHoras": 1.5,
    "atrasado": false
  }
]
```

---

## 🔄 Fluxo do Chamado

```
[Criação] → Aberto
[Iniciar]  → EmAtendimento   ✋ Bloqueado se: Finalizado ou Cancelado
[Finalizar]→ Finalizado       ✋ Bloqueado se: não foi iniciado
```

---

## ⚙️ Regras de Negócio

- ✅ Todo chamado é criado com status **Aberto**
- ✅ Não é possível iniciar um chamado **Finalizado** ou **Cancelado**
- ✅ Só é possível finalizar chamados que foram **iniciados**
- ✅ A listagem exibe o **tempo total** de atendimento (em horas)
- ✅ Chamados que **ultrapassaram o tempo estimado** da prioridade são marcados como `atrasado: true`
- ✅ Chamados **em andamento** também são monitorados para atrasos em tempo real

---

## 👩‍💻 Desenvolvido por

**Agatha Dantas** — Processo Seletivo W5i · Estágio 2026
