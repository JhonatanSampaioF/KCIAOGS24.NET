# Use a imagem base do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Defina o diretório de trabalho
WORKDIR /src

# Copie os arquivos de projeto
COPY ["KCIAOGS24.NET.csproj", "KCIAOGS24.NET/"]

# Restaure as dependências do projeto
RUN dotnet restore "KCIAOGS24.NET/KCIAOGS24.NET.csproj"

# Copie o restante dos arquivos
COPY . .

# Publique a aplicação
RUN dotnet publish "KCIAOGS24.NET/KCIAOGS24.NET.csproj" -c Release -o /app/publish

# Defina a imagem base do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Defina o diretório de trabalho
WORKDIR /app

# Copie a aplicação publicada para o diretório de trabalho
COPY --from=build /app/publish .

# Exponha a porta que o aplicativo vai rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "KCIAOGS24.NET.dll"]
