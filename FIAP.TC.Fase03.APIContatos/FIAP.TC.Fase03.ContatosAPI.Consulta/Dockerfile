FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY FIAP.TC.Fase03.ContatosAPI.Consulta ./FIAP.TC.Fase03.ContatosAPI.Consulta
COPY FIAP.TC.FASE03.Shared ./FIAP.TC.FASE03.Shared

WORKDIR /source/FIAP.TC.Fase03.ContatosAPI.Consulta
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4.25.2
WORKDIR /home/site/wwwroot
COPY --from=build /out ./

ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 80

ENTRYPOINT [ "/azure-functions-host/Microsoft.Azure.Functions.EntryPoint" ]
