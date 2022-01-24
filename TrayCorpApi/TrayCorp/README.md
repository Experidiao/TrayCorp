# Sobre o projeto TrayCorp

Este é um back-End simples para cadastro de produto. Tendo todas as funcionalidades CRUD.

# Tecnologia utilizada
## Back End

- .Net core 5.0
- Framework AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
- System.ComponentModel.Annotations
- Microsoft.Extensions.DependencyInjection.Abstractions
- Dapper
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.Configuration
- FluentValidation.AspNetCore
- Swashbuckle.AspNetCore
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Abstractions

## Bando de dados
- Sql Server 

## Diagrama
![Diagrama](https://github.com/Experidiao/TrayCorp/blob/main/TrayCorpApi/TrayCorp/Diagrama.png)


# Estrutura do projeto
- Application
  - TrayCorp.Application
    - AutoMapper
    - DTO
    - Interface
    - Services
- Domain
  - TrayCorp.Domain
    - Interfaces
    - Models
- Infra
    - CrossCutting
      - TrayCorp.Infra.CrossCutting.IoC
        - InjetorDependencias
    - Data
      - TrayCorp.Infra.Data
        - Context
        - Mappings
        - Repository
        - UoW
- Service Api
  - TrayCorp.Services.Api
    - Controllers
    - Validation

