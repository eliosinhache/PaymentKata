#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8090
EXPOSE 444
ENV ASPNETCORE_URLS=http://*:8090

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PaymentChecker.csproj", "."]
RUN dotnet restore "./PaymentChecker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PaymentChecker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentChecker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#
#RUN ln -fs /usr/share/zoneinfo/America/Lima /etc/localtime
#RUN dpkg-reconfigure --frontend noninteractive tzdata
ENTRYPOINT ["dotnet", "PaymentChecker.dll"]