#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

ApiKey=$1
Source=$2

nuget pack DvBCrud.EFCore/DvBCrud.EFCore.csproj -Verbosity detailed
nuget pack DvBCrud.EFCore.API/DvBCrud.EFCore.API.csproj -Verbosity detailed

nuget push DvBCrud.EFCore/bin/Debug/DvBCrud.EFCore.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source
nuget push DvBCrud.EFCore.API/bin/Debug/DvBCrud.EFCore.API.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source
