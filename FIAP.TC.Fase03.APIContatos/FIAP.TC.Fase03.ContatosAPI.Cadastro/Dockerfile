FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

# Copia apenas o projeto e suas dependências (evita colidir com outros appsettings)
COPY FIAP.TC.Fase03.ContatosAPI.Cadastro ./FIAP.TC.Fase03.ContatosAPI.Cadastro
COPY FIAP.TC.FASE03.Shared ./FIAP.TC.FASE03.Shared

WORKDIR /build/FIAP.TC.Fase03.ContatosAPI.Cadastro

# Restore e publish só do projeto de Cadastro
RUN dotnet restore
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:5141
EXPOSE 5141

ENTRYPOINT ["dotnet", "FIAP.TC.Fase03.ContatosAPI.Cadastro.dll"]
