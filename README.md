# 🚀 Projeto C# com MongoDB Atlas (Porta 443)

Este repositório demonstra como conectar uma aplicação **C#** ao **MongoDB Atlas** utilizando a **porta 443** (TLS/HTTPS), ideal para redes que bloqueiam a porta padrão `27017`.

---

## 📋 Pré-requisitos

- Conta gratuita no [MongoDB Atlas](https://www.mongodb.com/atlas)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou VS Code
- Pacote NuGet **MongoDB.Driver**

---

## 🛠 Configuração do MongoDB Atlas

### 1️⃣ Criar Cluster
1. Acesse o MongoDB Atlas.
2. Clique em **Build a Cluster** e escolha o **Free Tier**.

### 2️⃣ Liberar IPs
1. Vá em **Security → Network Access**.
2. Clique em **Add IP Address**.
3. Escolha **Allow Access from Anywhere**.
4. Salve `0.0.0.0/0`.

### 3️⃣ Criar Usuário
1. Vá em **Security → Database Access**.
2. Clique em **Add New Database User**.
3. Defina usuário e senha.
4. Dê permissões **Atlas Admin** ou **Read and write to any database**.

> ⚠ Se a senha tiver caracteres especiais (`@ # % !`), use **URL encode**:
> - `@` → `%40`
> - `#` → `%23`
> - `%` → `%25`

### 4️⃣ Obter String de Conexão SRV (porta 443)
1. No Atlas: **Clusters → Connect → Drivers**.
2. Copie a string:
   mongodb+srv://USUARIO:SENHA@cluster0.abc123.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0
3. O prefixo `mongodb+srv://` indica conexão **TLS via porta 443**.

---

## 📦 Criando o Projeto C#

### Criar Console App
```bash
dotnet new console -n MongoDBPorta443
cd MongoDBPorta443
dotnet add package MongoDB.Driver


