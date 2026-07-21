# Sistema de Controle de Gastos Residenciais

Sistema desenvolvido para gerenciamento de gastos residenciais, permitindo o cadastro de pessoas, registro de transações financeiras e consulta de totais por pessoa e geral.

## Tecnologias Utilizadas

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger

### Frontend
- React 19
- TypeScript
- Vite
- React Router DOM
- Axios

---

# Funcionalidades

## Cadastro de Pessoas

- Cadastrar pessoas
- Listar pessoas cadastradas
- Excluir pessoas
- Exclusão em cascata das transações da pessoa

## Cadastro de Transações

- Cadastro de receitas
- Cadastro de despesas
- Listagem das transações

### Regra de negócio

Pessoas menores de 18 anos podem cadastrar apenas despesas.

---

## Consulta de Totais

Para cada pessoa são apresentados:

- Total de Receitas
- Total de Despesas
- Saldo

Também é exibido:

- Total Geral de Receitas
- Total Geral de Despesas
- Saldo Geral

---

# Estrutura do Projeto

```
controle-gastos
│
├── backend
│   └── ControleGastos.Api
│
└── frontend
    └── controle-gastos-web
```

---

# Requisitos

Instalar:

- Visual Studio 2022 (17.14 ou superior)
- .NET SDK 10
- Node.js 22 LTS
- Git

Opcional:

- Visual Studio Code

---

# Clonando o Projeto

```bash
git clone https://github.com/thiagosilva-devbr/controle-gastos.git
```

Entre na pasta

```bash
cd controle-gastos
```

---

# Executando o Backend

Entre na pasta da API

```bash
cd backend/ControleGastos.Api
```

Restaurar dependências

```bash
dotnet restore
```

Executar

```bash
dotnet run
```

ou

```bash
dotnet watch
```

A API ficará disponível em:

```
https://localhost:5216
```

ou

```
http://localhost:5215
```

(As portas podem variar conforme o arquivo launchSettings.json.)

---

## Swagger

Após iniciar a API, acessar:

```
https://localhost:5216/swagger
```

---

# Executando o Frontend

Entrar na pasta

```bash
cd frontend/controle-gastos-web
```

Instalar dependências

```bash
npm install
```

Executar

```bash
npm run dev
```

A aplicação ficará disponível normalmente em

```
http://localhost:5173
```

---

# Configuração do Frontend

No projeto React existe o arquivo:

```
.env
```

Configure a URL da API.

Exemplo:

```env
VITE_API_URL=https://localhost:5216/api
```

Caso utilize HTTP:

```env
VITE_API_URL=http://localhost:5215/api
```

---

# Banco de Dados

O projeto utiliza SQLite.

Caso o banco não exista, execute as migrations.

Criar migration

```bash
dotnet ef migrations add InitialCreate
```

Atualizar banco

```bash
dotnet ef database update
```

---

# Publicação da API

Gerar publicação

```bash
dotnet publish -c Release
```

Será criada a pasta

```
bin/Release/net10.0/publish
```

Copiar todo o conteúdo para o servidor IIS.

---

# Configuração do IIS

## 1. Instalar

- IIS
- ASP.NET Core Hosting Bundle .NET 10

## 2. Criar um Site

Apontar para a pasta

```
publish
```

## 3. Application Pool

Configurar:

```
No Managed Code
```

Pipeline:

```
Integrated
```

## 4. Permissões

Conceder leitura e execução para:

```
IIS_IUSRS
```

e

```
IUSR
```

---

# Configuração do Frontend no IIS

Gerar build

```bash
npm run build
```

Será criada a pasta

```
dist
```

Copiar todo o conteúdo da pasta **dist** para um novo Site do IIS ou configurar como aplicação dentro do mesmo site.

---

## Rewrite para React

Criar um arquivo **web.config** na pasta **dist**

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>

  <system.webServer>

    <rewrite>

      <rules>

        <rule name="React Routes" stopProcessing="true">

          <match url=".*" />

          <conditions logicalGrouping="MatchAll">

            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true"/>

            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true"/>

          </conditions>

          <action type="Rewrite" url="/" />

        </rule>

      </rules>

    </rewrite>

  </system.webServer>

</configuration>
```

---

# Padrões Utilizados

- Arquitetura em camadas
- REST API
- DTOs
- Entity Framework Core
- React Router
- Componentização
- Tipagem com TypeScript
- Consumo da API utilizando Axios

---

# Autor

Thiago Luiz Alves da Silva

GitHub

https://github.com/thiagosilva-devbr

LinkedIn

https://www.linkedin.com/in/thiago-silva-151126348/

---

# Licença

Projeto desenvolvido para fins de estudo e demonstração técnica.
