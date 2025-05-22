FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

COPY FIAP.TC.Fase03.ContatosAPI.Atualizacao ./FIAP.TC.Fase03.ContatosAPI.Atualizacao
COPY FIAP.TC.FASE03.Shared ./FIAP.TC.FASE03.Shared

WORKDIR /build/FIAP.TC.Fase03.ContatosAPI.Atualizacao

RUN dotnet restore
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "FIAP.TC.Fase03.ContatosAPI.Atualizacao.dll"]
