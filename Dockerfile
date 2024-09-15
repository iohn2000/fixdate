# Use the official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the application code
COPY . ./

WORKDIR /app/fixDate
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/fixDate/out .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "fixDate.dll"]

#
# build project in docker image
# run in smaller dotnet images with runtime only
# docker build -t fixdatedocker .
# docker run -it --rm fixdatedocker -p . -v 3
#