# Projeto MyPet

## Descrição

API para um gerenciar a localização de pets. Desenvolvida em [C# .NET](https://learn.microsoft.com/en-us/dotnet/csharp/). Com sistema **CRUD** (POST, GET, PUT e DELETE) utilizando a arquitetura **API-REST**(Representational State Transfer).

<details>
  <summary>💻 <strong>Tecnologias utilizadas</strong></summary><br />

- [.NET 6](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-6)
- [AspNetCore](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0)
- [xUnit](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/introduction)
- [QRCoderNetCore](https://fluentassertions.com/introduction)
- [Swashbuckle](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio): Swaggwer
</details>

## Endpoints

>  Use algum dos seguintes programas para fazer as requisições: [Postman](https://www.postman.com/) ou [Thunder Client](https://www.thunderclient.com/) ou [Insomnia](https://insomnia.rest/).

#### Endereco

- <details>
      <summary><strong>POST</strong> (cadastra endereço)</summary>

  - Url:
     - `/endereco`
     - Exemplo: `http://localhost:3000/Endereco`
  - Request:
    - Body:
      ```json
        {
            "cep": "01001-000",
            "logradouro": "Praça da Sé",
            "complemento": "lado ímpar",
            "bairro": "Sé",
            "localidade": "São Paulo",
            "uf": "SP",
        }
      ```
</details>

- <details>
      <summary><strong>GET</strong> (todos os endereços)</summary>

  - Url:
     - `/Endereco`
     - Exemplo: `http://localhost:3000/Endereco?page=1&row=5&orderBy=6`

</details>

- <details>
      <summary><strong>GET</strong> (endereço por cep)</summary>

  - Url:
     - `/Endereco/{cep}`
     - Exemplo: `http://localhost:3000/Endereco/01001000"`

</details>

- <details>
      <summary><strong>GET</strong> (endereço por localização)</summary>

  - Url:
     - `/Endereco/location/{latitude}/{longitude}`
     - Exemplo: `http://localhost:3000/Endereco/location/-22.544830/-43.134367`

  - Response sucesso:
    - Status: `200 Ok`
    - Body:
      ```json
      {
          "place_id": 342345036,
          "lat": "-22.5538559",
          "lon": "-43.0497497",
          "display_name": "Santo Aleixo, Magé, Região Geográfica Imediata do Rio de Janeiro, Região Metropolitana do Rio de Janeiro, Região Geográfica Intermediária do Rio de Janeiro, Rio de Janeiro, Região Sudeste, 25912-296, Brasil",
          "address": {
              "village": "Santo Aleixo",
              "city": "Magé",
              "municipality": "Região Geográfica Imediata do Rio de Janeiro",
              "county": "Região Metropolitana do Rio de Janeiro",
              "state_district": "Região Geográfica Intermediária do Rio de Janeiro",
              "state": "Rio de Janeiro",
              "ISO3166-2-lvl4": "BR-RJ",
              "region": "Região Sudeste",
              "postcode": "25912-296",
              "country": "Brasil",
              "country_code": "br"
          },
          "boundingbox": [
              "-22.6110895",
              "-22.4840381",
              "-43.1777958",
              "-43.013678"
          ]
      }

</details>

- <details>
      <summary><strong>DELETE</strong> (deleta endereço por cep)</summary>

  - Url:
     - `/Endereco/{cep}`
     - Exemplo: `http://localhost:3000/Endereco/01001000"`

</details>

#### Login

- <details>
      <summary><strong>POST</strong> (login)</summary>

  - Url:
     - `/login`
     - Exemplo: `http://localhost:3000/login`
  - Request:
    - Body:
      ```json
        {
            "email": "user@example.com",
            "password": "password"
        }
      ```

#### Pet

- <details>
      <summary><strong>POST</strong> (cadastra pet)</summary>

  - Url:
     - `/pet`
     - Exemplo: `http://localhost:3000/pet`
  - Request:
    - Body:
      ```json
      {
          "nome": "string",
          "porte": "string",
          "raca": "string",
          "tutorId": 0,
          "dataNascimento": "2023-02-12"
      }
      ```

</details>

- <details>
      <summary><strong>GET</strong> (todos os pets)</summary>

  - Url:
     - `/pet`
     - Exemplo: `http://localhost:3000/Pet?page=1&row=5&orderBy=5`

</details>

- <details>
      <summary><strong>GET</strong> (pet por id)</summary>

  - Url:
     - `/pet/{id}`
     - Exemplo: `http://localhost:3000/Pet/1`

</details>

- <details>
      <summary><strong>PUT</strong> (altera pet por id)</summary>

  - Url:
     - `/pet/{id}`
     - Exemplo: `http://localhost:3000/Pet/1`
  - Request:
    - Body:
      ```json
        {
            "nome": "string",
            "porte": "string",
            "raca": "string",
            "tutorId": 0,
            "dataNascimento": "2023-02-12"
        }
      ```
</details>

- <details>
      <summary><strong>DELETE</strong> (deleta pet por id)</summary>

  - Url:
     - `/pet/{id}`
     - Exemplo: `http://localhost:3000/Pet/1`

</details>

- <details>
      <summary><strong>GET</strong> (gera QrCode do pet por id)</summary>

  - Url:
     - `/pet/qrcode/{id}`
     - Exemplo: `http://localhost:3000/Pet/qrcode/1`

</details>

- <details>
      <summary><strong>GET</strong> (traz mais informações do pet por id)</summary>

  - Url:
     - `/pet/info/{id}`
     - Exemplo: `http://localhost:3000/Pet/info/1`

</details>

#### Tutor

- <details>
      <summary><strong>POST</strong> (cadastra tutor)</summary>

  - Url:
     - `/tutor`
     - Exemplo: `http://localhost:3000/tutor`
  - Request:
    - Body:
      ```json
      {
          "nome": "Jhon",
          "email": "user@example.com",
          "cep": "string",
          "password": "string"
      }
      ```

</details>

- <details>
      <summary><strong>GET</strong> (todos os tutores)</summary>

  - Url:
     - `/tutor`
     - Exemplo: `http://localhost:3000/tutor?page=1&row=5&orderBy=5`

</details>

- <details>
      <summary><strong>GET</strong> (tutor por id)</summary>

  - Url:
     - `/tutor/{id}`
     - Exemplo: `http://localhost:3000/tutor/1`

</details>

- <details>
      <summary><strong>PUT</strong> (altera tutor por id)</summary>

  - Url:
     - `/tutor/{id}`
     - Exemplo: `http://localhost:3000/tutor/1`
  - Request:
    - Body:
      ```json
        {
            "nome": "stringstri",
            "email": "user@example.com",
            "cep": "string",
            "password": "string"
        }
      ```
</details>

- <details>
      <summary><strong>DELETE</strong> (deleta tutor por id)</summary>

  - Url:
     - `/tutor/{id}`
     - Exemplo: `http://localhost:3000/tutor/1`

</details>

