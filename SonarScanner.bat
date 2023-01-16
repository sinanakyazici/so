dotnet sonarscanner begin /k:"so" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="sqp_affcb075ffcbd386e24a9c2f4f76324d23ea6077"
dotnet build so.sln
dotnet sonarscanner end /d:sonar.login="sqp_affcb075ffcbd386e24a9c2f4f76324d23ea6077"
pause