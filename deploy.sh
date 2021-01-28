#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

key=$1
src=$2

dotnet pack -c Release DvBCrud.EFCore/DvBCrud.EFCore.csproj
dotnet pack -c Release DvBCrud.EFCore.API/DvBCrud.EFCore.API.csproj

dotnet nuget push DvBCrud.EFCore/bin/Release/DvBCrud.EFCore.*.nupkg --api-key $key --source $src
dotnet nuget push DvBCrud.EFCore.API/bin/Release/DvBCrud.EFCore.API.*.nupkg --api-key $key --source $src
