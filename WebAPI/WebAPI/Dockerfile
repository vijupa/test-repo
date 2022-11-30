#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./WebAPI.csproj", "./"]
RUN dotnet restore "WebAPI.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM node:16 AS node-builder
#RUN mkdir -p /components/example
#WORKDIR /components/example
#COPY ./WebComponents/Example /components/example
#RUN npm install
#RUN npm run build
#WORKDIR /components/decision
#COPY ./WebComponents/Decision /components/decision
#RUN npm install
#RUN npm run build
WORKDIR /components/meeting
COPY ./WebComponents/Meeting /components/meeting
RUN npm install
RUN npm run build

FROM base AS final
RUN mkdir -p /app/ScriptFiles/components
WORKDIR /app
COPY --from=publish /app/publish .
#COPY --from=node-builder /components/example/dist/bundle.js ./ScriptFiles/components/example.js
#COPY --from=node-builder /components/decision/dist/bundle.js ./ScriptFiles/components/decision.js
COPY --from=node-builder /components/meeting/dist/bundle.js ./ScriptFiles/components/meeting.js
ENTRYPOINT ["dotnet", "WebAPI.dll"]