# 1. Imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copiar toda a solução
COPY . .

# Restaurar dependências
RUN dotnet restore Lojas.sln

# Build e publish do projeto principal
WORKDIR /app/Site
RUN dotnet publish Site.csproj -c Release -o /app/out

# 2. Imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

# Copiar arquivos publicados
COPY --from=build /app/out ./

# Definir variável de ambiente PORT
ENV PORT=8080
ENV CONNECTION_STRING=""

# Expor porta
EXPOSE 8080

# Porta que será exposta
# EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "Site.dll"]
