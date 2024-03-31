Documentação API

Dotnet 8.0.3
Banco de dados em memória

Rotas:

# url = http://localhost:####

# Restrito = Precisa adicionar Token ao Bearer

===================================================

Coleção: Todo

---

Request: GET
Restrito: Sim
Info: Busca todas as todo lista do usuário
Rota: /api/todos

---

Request: POST
Restrito: Sim
Info: Cria uma nova todo lista
Rota:/api/todos
Body:
-JSON-
{
"titulo": "string",
"desc": "string",
"dt_criacao": "2024-03-31T16:09:07.625Z",
"dt_conclusao": null
}

---

Request: GET
Restrito: Sim
Info: Busca dados de uma todo lista específica - {id} = Código do item desejado
Rota:/api/todos/{id} -

---

Request: PUT
Restrito: Sim
Info: Atualizar Todo Lista - {id} Código do item da Todo Lista
Rota:/api/todos/{id}
Body:
-JSON-
{
"titulo": "string",
"desc": "string",
"dt_criacao": "2024-03-31T16:18:11.739Z",
"dt_conclusao": "2024-03-31T16:18:11.739Z",
}

---

Request: DELETE
Restrito: Sim
Info: Deletar item da Todo Lista do usuário- {id} Código do item da Todo Lista
Rota:/api/todos/{id}

===================================================

Coleção: Usuário

---

Request: GET
Restrito: Sim
Info: Busca dados do usuário logado
Rota: /api/usuario/dados

---

Request: POST
Restrito: Sim
Info: Cria um novo Usuário
Rota: /api/usuario/novo/usuario
Body:
-JSON-
{
"name": "string",
"email": "string",
"senha": "string"
}

---

Request: POST
Restrito: Sim
Info: Faz login na API e retorna um Token
Rota: /api/usuario/verificalogin
Body:
-JSON-
{
"email": "string",
"senha": "string"
}

===================================================
