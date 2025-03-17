# CRUD API

Este projeto é uma API RESTful desenvolvida em .NET 8 utilizando Entity Framework Core e PostgreSQL. Ele fornece operações CRUD (Create, Read, Update, Delete) para gerenciar contatos.

## Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- PostgreSQL
- Swagger (Swashbuckle)
- CORS

## Como Executar o Projeto

### 1. Clonar o Repositório
```bash
git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio
```

### 2. Configurar o Banco de Dados
Certifique-se de ter o PostgreSQL instalado e crie um banco de dados. Em seguida, edite o arquivo **appsettings.json**:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=nome_do_banco;Username=seu_usuario;Password=sua_senha"
}
```

### 3. Restaurar as Dependências
```bash
dotnet restore
```

### 4. Aplicar Migrações
```bash
dotnet ef database update
```

### 5. Executar a API
```bash
dotnet run
```
A API será iniciada e estará disponível em: `https://localhost:5001` ou `http://localhost:5000`

## Documentação da API
Esta API utiliza Swagger para documentação interativa. Para acessar, execute a API e abra no navegador:
```
https://localhost:5001/swagger
```

## Endpoints

### 1. Obter Todos os Contatos
**GET** `/api/contact`

### 2. Obter Contato por ID
**GET** `/api/contact/{id}`

### 3. Criar um Novo Contato
**POST** `/api/contact`
```json
{
  "name": "Nome do Contato",
  "email": "email@exemplo.com",
  "phone": "(11) 99999-9999"
}
```

### 4. Atualizar Contato
**PUT** `/api/contact/{id}`
```json
{
  "id": 1,
  "name": "Nome Atualizado",
  "email": "email@exemplo.com",
  "phone": "(11) 99999-9999"
}
```

### 5. Excluir Contato
**DELETE** `/api/contact/{id}`

## Configurando CORS para o Frontend Angular
No arquivo **Program.cs**, já existe a seguinte configuração:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
```
Se precisar modificar as origens permitidas, altere o URL.

## Publicação no GitHub
1. Inicializar o repositório (se ainda não estiver versionado):
```bash
git init
git add .
git commit -m "Primeiro commit"
```
2. Criar um repositório no GitHub e vinculá-lo:
```bash
git remote add origin https://github.com/seu-usuario/nome-do-repositorio.git
git branch -M main
git push -u origin main
```

## Contribuição
Se quiser contribuir, abra uma _issue_ ou envie um _pull request_.

## Licença
Este projeto está licenciado sob a MIT License.

