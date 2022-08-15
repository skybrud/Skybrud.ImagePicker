@echo off
dotnet build src/Skybrud.ImagePicker --configuration Debug /t:rebuild /t:pack -p:BuildTools=0 -p:PackageOutputPath=C:/nuget