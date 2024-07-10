FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL stage=builder
WORKDIR /expensiflow
COPY . .
WORKDIR /expensiflow/src/ExpensiFlow.Api
RUN --mount=type=cache,target=/root/.nuget,sharing=locked dotnet publish "ExpensiFlow.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0
USER app
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "ExpensiFlow.Api.dll"]