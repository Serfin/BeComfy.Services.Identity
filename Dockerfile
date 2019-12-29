FROM mcr.microsoft.com/dotnet/core/sdk:3.0
WORKDIR /app
COPY ./src/BeComfy.Services.Identity/bin/Release/netcoreapp3.0 .
ENV ASPNETCORE_URLS http://*:5010
ENV ASPNETCORE_ENVIRONMENT Release
EXPOSE 5010
ENTRYPOINT ["dotnet", "BeComfy.Services.Identity.dll"]