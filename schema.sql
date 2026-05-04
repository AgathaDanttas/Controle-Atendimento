-- ============================================================
-- Controle de Atendimentos — W5i
-- Script de criação do banco de dados (SQLite)
-- Desenvolvido por: Agatha Dantas
-- ============================================================

CREATE TABLE IF NOT EXISTS "Prioridades" (
    "Id"                  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Nome"                TEXT    NOT NULL,
    "TempoEstimadoHoras"  INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS "Setores" (
    "Id"   INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Nome" TEXT    NOT NULL
);

CREATE TABLE IF NOT EXISTS "Chamados" (
    "Id"           INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
    "Titulo"       TEXT     NOT NULL,
    "Descricao"    TEXT     NOT NULL,
    "DataCriacao"  TEXT     NOT NULL,
    "DataInicio"   TEXT     NULL,
    "DataFim"      TEXT     NULL,
    "Solucao"      TEXT     NULL,
    "Status"       INTEGER  NOT NULL DEFAULT 0,
    "SetorId"      INTEGER  NOT NULL,
    "PrioridadeId" INTEGER  NOT NULL,
    CONSTRAINT "FK_Chamados_Setores_SetorId"
        FOREIGN KEY ("SetorId") REFERENCES "Setores" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Chamados_Prioridades_PrioridadeId"
        FOREIGN KEY ("PrioridadeId") REFERENCES "Prioridades" ("Id") ON DELETE CASCADE
);


CREATE INDEX IF NOT EXISTS "IX_Chamados_SetorId"      ON "Chamados" ("SetorId");
CREATE INDEX IF NOT EXISTS "IX_Chamados_PrioridadeId" ON "Chamados" ("PrioridadeId");


INSERT INTO "Prioridades" ("Nome", "TempoEstimadoHoras") VALUES
    ('Baixa',  8),
    ('Média',  4),
    ('Alta',   2),
    ('Crítica', 1);

INSERT INTO "Setores" ("Nome") VALUES
    ('TI'),
    ('Financeiro'),
    ('RH'),
    ('Comercial'),
    ('Operações');
