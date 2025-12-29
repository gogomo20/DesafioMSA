<h1>Desafio MSA Tech</h1>

O desafio consiste em realizar uma api para cadastro e consulta de clientes.

#### 🔧 Pré-requisitos
  - .NET9 ou superior.
### 🪜 Passo a Passo
1. Configure a connection string do arquivo:
  -DesafioMSA.Presentation/appsettings.json
  - exemplo:
  ```
  {
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres"
    }
  }
  ```
2. Execute o comando
   ```dotnet run --project DesafioMSA.Presentation```
3. Acesse a documentação da aplicação:
    - http://localhost:5289/swagger
    - https://localhost:7000/swagger

### Informações adicionais e decisões técnica
  - Foi implementado no metodo de deleção e consulta um soft delete, assim quando o cliente é excluído o mesmo não é removido da base de dados e causando a perda definitiva de dados do cliente.
  - Foi utilizado um padrão chamado UnitOfWork para controle de transações com o banco de dados, no qual garante persistencia corretas dos dados inseridos no banco, pois caso ocorra algum erro durante o commit para o banco o mesmo não é executado para nenhum registro realizado no meio da transação.
  - Foi utilizado o padrão Mediator para facilitar a separação de responsabilidades de queries e commands(CQRS), o mesmo poderia ser implementado sem o padrão Mediator, mas utilizar ele traz uma padronização no codigo e reduz a quantidade de código utlizado nas controllers.

🧑‍💻 Autor 
Erick Allan Moraes de Oliveira<br>
💻 Desenvolvedor .NET & Angular<br>
📧 [LinkedIn](https://www.linkedin.com/in/erick-allan-moraes/)<br>
📍 Brasil
