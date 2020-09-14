# restapiexample

This application was generated using JHipster 6.9.1 and JHipster .Net Core 1.0.0.

## Development

To start your application in the Debug configuration, simply run:

    dotnet run --verbosity normal --project ./src/Restapiexample/Restapiexample.csproj

For further instructions on how to develop with JHipster, have a look at [Using JHipster in development][].

## Building for production

To build the arifacts and optimize the restapiexample application for production, run:

    cd ./src/Restapiexample
    rm -rf ./src/Restapiexample/wwwroot
    dotnet publish --verbosity normal -c Release -o ./app/out ./Restapiexample.csproj

The `./src/Restapiexample/app/out` directory will contain your application dll and its depedencies.

## Testing

To launch your application's tests, run:

    dotnet test --list-tests --verbosity normal

### Code quality

By Script :

1. Run Sonar in container : `docker-compose -f ./docker/sonar.yml up -d`

2. Wait container was up Run `SonarAnalysis.ps1` and go to http://localhost:9001

Manually :

1. Run Sonar in container : `docker-compose -f ./docker/sonar.yml up -d`

2. Install sonar scanner for .net :

`dotnet tool install --global dotnet-sonarscanner`

3. Run `` dotnet sonarscanner begin /d:sonar.login=admin /d:sonar.password=admin /k:"Restapiexample" /d:sonar.host.url="http://localhost:9001" /s:"`pwd`/SonarQube.Analysis.xml" ``

4. Build your application : `dotnet build`

5. Publish sonar results : `dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin`

6. Go to http://localhost:9001

### Monitoring

1. Run container (uncomment chronograf and kapacitor if you would use it): `docker-compose -f ./docker/monitoring.yml up -d`

2. Go to http://localhost:3000 (or http://localhost:8888 if you use chronograf)

3. (Only for chronograf) Change influxdb connection string by `YourApp-influxdb`

4. (Only for chronograf) Change kapacitor connection string by `YourApp-kapacitor`

5. (Only for chronograf) You can now add dashboard (like docker), see your app log in Cronograf Log viewer and send alert with kapacitor

## Build a Docker image

You can also fully dockerize your application and all the services that it depends on. To achieve this, first build a docker image of your app by running:

    docker build -f ./src/Restapiexample/Dockerfile -t restapiexample .

Then run:

    docker run -p 80:80 restapiexample
