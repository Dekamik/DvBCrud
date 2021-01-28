#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

key=$1
src=$2

dotnet nuget pack DvBCrud.EFCore/DvBCrud.EFCore.csproj
dotnet nuget pack DvBCrud.EFCore.API/DvBCrud.EFCore.API.csproj

dotnet nuget push DvBCrud.EFCore/bin/Debug/DvBCrud.EFCore.*.nupkg --api-key $key --source $src
dotnet nuget push DvBCrud.EFCore.API/bin/Debug/DvBCrud.EFCore.API.*.nupkg --api-key $key --source $src
