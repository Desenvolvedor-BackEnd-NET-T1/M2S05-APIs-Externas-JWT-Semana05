# Pizzaria Byte, semana 05 (M2S05): projeto da turma

Requisitos: .NET 10 SDK e internet (para o ViaCEP).

```
dotnet build PizzariaByte.slnx
```

| Projeto | O que é | Como rodar |
|---|---|---|
| `PizzariaByte.MotoByte` | API da transportadora parceira (a nossa "API externa"). Não precisa mexer nela. | `dotnet run` (http://localhost:5300) |
| `PizzariaByte.Integracoes` | Exemplos e exercícios das aulas | `dotnet run -- <exemplo>` |
| `PizzariaByte.Api` | Web API da pizzaria consumindo o ViaCEP e a MotoByte | `dotnet run` (http://localhost:5227/swagger) |

**Deixe a MotoByte rodando em um terminal** enquanto faz os exercícios.

## Exercícios (PizzariaByte.Integracoes)

| Aula | Rode com | Arquivo |
|---|---|---|
| 1 | `dotnet run -- pratica1` | Aula1/A03_Pratica.cs |
| 2 | `dotnet run -- pratica2` | Aula2/B04_Pratica.cs (e a propriedade Ddd em Modelos/Endereco.cs) |
| 2 | `dotnet run -- desafio2` | Aula2/B08_Desafio.cs |
| 3 | `dotnet run -- pratica3` | Aula3/C06_Pratica.cs |

Os outros exemplos já estão prontos: rode `dotnet run` sem nome para ver a lista.

## User Secrets (aula 3)

A rota `api/entregas` da API precisa do segredo da MotoByte. Ele não fica no
código:

```
cd PizzariaByte.Api
dotnet user-secrets set "MotoByte:ClientSecret" "mb-7f3a9c2e"
```

Depois, reinicie a API.
