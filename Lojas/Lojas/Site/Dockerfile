# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o restante do projeto
COPY . ./

# Build da aplicação
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Criação da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

# Copiar arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Expor porta 80
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Site.dll"]
# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o restante do projeto
COPY . ./

# Build da aplicação
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Criação da imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

# Copiar arquivos publicados da etapa de build
COPY --from=build /app/publish .

# Expor porta 80
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Site.dll"]
