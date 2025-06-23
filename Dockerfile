# Imagem base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagem de build com SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia apenas o .csproj primeiro para restaurar depend�ncias
COPY ["COOPGO.csproj", "./"]

# Restaura depend�ncias
RUN dotnet restore "COOPGO.csproj"

# Copia o restante do c�digo
COPY . .

# Compila o projeto
RUN dotnet build "COOPGO.csproj" -c Release -o /app/build

# Publica a aplica��o
FROM build AS publish
RUN dotnet publish "COOPGO.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Cria imagem final com runtime e arquivos publicados
FROM base AS final
WORKDIR /app

# Copia arquivos do est�gio publish
COPY --from=publish /app/publish .

# Ponto de entrada da aplica��o
ENTRYPOINT ["dotnet", "COOPGO.dll"]
