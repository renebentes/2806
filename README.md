# 2806 - Acesso à dados com C#, .NET 5, Dapper e SQL Server

> Repositório do Curso 2806 - Acesso à dados com C#, .NET 5, Dapper e SQL Server disponível na plataforma [balta.io](https://balta.io)

Contém códigos de exemplos desenvolvidos durante as aulas.

Você pode encontrar os originais em:

- [balta.io](https://balta.io/cursos/acesso-dados-csharp-net-dapper-sql-server)
- [github.com](https://github.com/balta-io/2806)

## Tecnologias Utilizadas

- [.Net](https://dotnet.microsoft.com/)
- [Visual Studio Code](https://code.visualstudio.com)
- [Docker](https://www.docker.com)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio)

## Divisão do Código

Há links específicos para cada commit de finalização de uma aula.

### Lista de Aulas

| Aulas                             | Descrição                                           |
| --------------------------------- | --------------------------------------------------- |
| [aula-1-5](../../commit/a3ac0be)  | Introdução - Connection String                      |
| [aula-1-6](../../commit/6798067)  | Introdução - Microsoft.Data.SqlClient               |
| [aula-1-7](../../commit/362c911)  | Introdução - SqlConnection                          |
| [aula-1-8](../../commit/ce5206f)  | Introdução - SqlCommand e SqlDataReader             |
| [aula-2-1](../../commit/8250327)  | Dapper - Instalação                                 |
| [aula-2-2](../../commit/765714c)  | Dapper - Primeira Consulta                          |
| [aula-2-4](../../commit/3e0e211)  | Dapper - Iniciando um Insert                        |
| [aula-2-6](../../commit/7dee61f)  | Dapper - SqlParameter                               |
| [aula-2-7](../../commit/0ffe935)  | Dapper - Update                                     |
| [aula-3-1](../../commit/0c3a6e4)  | Imersão - Execute Many                              |
| [aula-3-2](../../commit/58247e3)  | Imersão - Executando Procedures                     |
| [aula-3-3](../../commit/ff60338)  | Imersão - Lendo Procedures                          |
| [aula-3-4](../../commit/ee1b91a)  | Imersão - ExecuteScalar                             |
| [aula-3-5](../../commit/422cab4)  | Imersão - Views                                     |
| [aula-3-6](../../commit/15912dd)  | Imersão - OneToOne (Parte 1)                        |
| [aula-3-7](../../commit/28f0eef)  | Imersão - OneToOne (Parte 2)                        |
| [aula-3-8](../../commit/0dd8c7f)  | Imersão - OneToMany (Parte 1)                       |
| [aula-3-9](../../commit/861cae2)  | Imersão - OneToMany (Parte 2)                       |
| [aula-3-10](../../commit/11744a8) | Imersão - QueryMultiple                             |
| [aula-3-11](../../commit/7001166) | Imersão - SelectIn                                  |
| [aula-3-12](../../commit/32c5cbe) | Imersão - Like                                      |
| [aula-3-13](../../commit/cdc40c8) | Imersão - Transaction                               |
| [aula-4-3](../../commit/0bc0d47)  | Mão na Massa - Criando as tabelas                   |
| [aula-4-4](../../commit/005ba7d)  | Mão na Massa - Criando o projeto                    |
| [aula-4-6](../../commit/8df8f3e)  | Mão na Massa - CRUD                                 |
| [aula-4-8](../../commit/dc8c85a)  | Mão na Massa - User Repository                      |
| [aula-4-9](../../commit/8e6f2c6)  | Mão na Massa - Otimizando o código (UserRepository) |
| [aula-4-11](../../commit/0c3da08) | Mão na Massa - Role Repository                      |
| [aula-4-12](../../commit/4936880) | Mão na Massa - Otimizando o código (RoleRepository) |
| [aula-4-13](../../commit/acb504b) | Mão na Massa - Criando os Modelos                   |
| [aula-4-14](../../commit/25e250f) | Mão na Massa - Repositório Genérico                 |
| [aula-4-15](../../commit/baad7fd) | Mão na Massa - Utilizando o repositório genérico    |
| [aula-4-16](../../commit/097e427) | Mão na Massa - Implementando One To Many            |
| [aula-4-19](../../commit/dc0ef3d) | Mão na Massa - Populando listas                     |

### Desafios

- [x] Cadastrar um usuário
- [ ] Cadastrar um perfil
- [ ] Vincular um usuário a um perfil
- [ ] Cadastrar uma categoria
- [x] Cadastrar uma tag
- [ ] Cadastrar um post
- [ ] Vincular um post a uma tag
- [ ] Listar os usuários (nome, e-mail e perfis separados por vírgulas)
- [ ] Listar categorias com quantidade de posts
- [ ] Listar tags com quantidade de posts
- [ ] Listar os posts de uma categoria
- [ ] Listar os posts com sua categoria
- [ ] Listar os posts com suas tags (separadas por vírgulas)

## Autor

[Rene Bentes Pinto](http://github.com/renebentes)

## Licença

Copyright (c) 2022 Rene Bentes Pinto

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
