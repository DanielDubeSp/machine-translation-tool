FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update -y && apt-get install python3.7 python3-pip -y
#python3-dev  python3.7-dev python3-pip

RUN python3 -m pip install --upgrade pip

RUN pip3 install google_trans_new
RUN pip3 install requests
RUN pip3 install six
# python3 Translate.py

WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MachineTranslationTool.API/MachineTranslationTool.API.csproj", "."]
RUN dotnet restore "./MachineTranslationTool.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MachineTranslationTool.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MachineTranslationTool.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY "google_trans_new.py" "/usr/local/lib/python3.7/dist-packages/google_trans_new/."
ENTRYPOINT ["dotnet", "MachineTranslationTool.API.dll"]