#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

tag=$1
key=$2
src=$3

dotnet pack -c Release -p:PackageVersion=$tag DvBCrud.EFCore/DvBCrud.EFCore.csproj
dotnet pack -c Release -p:PackageVersion=$tag DvBCrud.EFCore.API/DvBCrud.EFCore.API.csproj

dotnet nuget push DvBCrud.EFCore/bin/Release/DvBCrud.EFCore.*.nupkg --api-key $key --source $src
dotnet nuget push DvBCrud.EFCore.API/bin/Release/DvBCrud.EFCore.API.*.nupkg --api-key $key --source $src
