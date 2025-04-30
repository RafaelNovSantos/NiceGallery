# Projeto Relatório de Produtos

Este projeto é uma aplicação Blazor (com Blazor WebAssembly) que permite visualizar e exportar um relatório de produtos para PDF. O projeto utiliza **MudBlazor** para a interface de usuário e **jsPDF** para a geração de PDFs no front-end.

## Tecnologias Utilizadas

- **Blazor WebAssembly**: Para o desenvolvimento do front-end interativo.
- **MudBlazor**: Framework de componentes UI para Blazor.
- **ASP.NET Core**: Para o back-end da API que fornece os dados dos produtos.
- **jsPDF**: Biblioteca JavaScript para a geração de PDFs no front-end.
- **C#**: Linguagem de programação para o back-end e front-end (Blazor).
- **JavaScript**: Para funcionalidades de exportação em PDF.
- **Postgres**: Banco de dados relacional, configurado no projeto para banco em memória, facilitando a execução em outras máquinas.

## Funcionalidades

- **Visualização de Relatório**: Exibe uma lista de produtos, incluindo nome e preço.
- **Busca e Filtragem**: Permite pesquisar produtos por nome ou preço.
- **Exportação para PDF**: Gera um relatório em PDF com todos os produtos listados.
- **Ordenação**: O relatório pode ser ordenado por nome ou preço.

## Requisitos

- **.NET SDK 9**: Para rodar o back-end e o Blazor. [Baixar aqui](https://dotnet.microsoft.com/download/dotnet).
- **Editor de código**: Visual Studio.

## Instruções de Execução

### 1. Clonando o repositório

Clone o repositório com o comando:

```bash
git clone https://github.com/RafaelNovSantos/NiceGallery.git
dotnet restore
```

### 2. Instalar ferramenta  DotNet EF para gerar a tabela do banco Postgres
```bash
cd NiceGallery.Api
dotnet tool install —global dotnet-ef
```

### 3. Criar a base de dados e rodar as migrações (Caso não deseje utilizar o banco de dados em memória)
```bash
dotnet ef migrations add Product
dotnet ef database update
```

E mude também a configuração do ASPNETCORE_ENVIRONMENT para "Staging", se quiser utilizar o banco de dados em memória utilize "Development"
![image](https://github.com/user-attachments/assets/af3b06ca-0925-4e5e-af6e-2feb3a931d86)


### 4. Para rodar a API e o Front-End juntos, configure a inicialização para a configuração "Start API and Front" e clique em "Start"

![image](https://github.com/user-attachments/assets/f203e8da-1b1a-4883-9a7f-b9c1d957e4d6)

### Passo 5: Executando o Projeto

#### Executando o Back-End (API)

A API será iniciada e você verá um caminho como:

- `https://localhost:7097`
- `http://localhost:5088`

Esses dois endereços são possíveis dependendo da configuração da sua aplicação. **Use sempre o `https://` quando for possível**.

#### Executando o Front-End (Blazor)

O **Blazor (Front-End)** será iniciado e você verá um caminho como:

- `https://localhost:7187`
- `http://localhost:5110`

### Passo 6: Testando a Comunicação entre API e Front-End

O **Front-End** irá fazer requisições à **API**, então certifique-se de que ambos os projetos estão rodando simultaneamente.

Você pode abrir o **Blazor** no navegador usando a URL do Front-End (por exemplo, `https://localhost:7187`) e verificar se os dados da API estão sendo carregados corretamente.

