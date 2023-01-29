# DvBCrud.EFCore
![Test Status](https://github.com/Dekamik/DvBCrud.EFCore/actions/workflows/test.yml/badge.svg)
![Build Status](https://github.com/Dekamik/DvBCrud.EFCore/actions/workflows/release.yml/badge.svg)

EFCore implementation of DvBCrud.

## [DvBCrud.EFCore](DvBCrud.EFCore)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore?label=DvBCrud.EFCore)](https://www.nuget.org/packages/DvBCrud.EFCore/)

The core implementation for handling repositories and entities using Entity Framework Core.

## [DvBCrud.EFCore.Services](DvBCrud.EFCore.Services)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore.Services?label=DvBCrud.EFCore.Services)](https://www.nuget.org/packages/DvBCrud.EFCore.Services/)

Templates for generating services, models and converters.

## [DvBCrud.EFCore.API](DvBCrud.EFCore.API)
[![Nuget](https://img.shields.io/nuget/v/DvBCrud.EFCore.API?label=DvBCrud.EFCore.API)](https://www.nuget.org/packages/DvBCrud.EFCore.API/)

Templates for generating CRUDControllers for Web APIs using EFCore.

## Component map

<img src='https://g.gravizo.com/svg?
	graph G {
		node [shape=box];
		DB [shape=cylinder];
		subgraph cluster0 {
			CrudController;
			label="DvBCrud.EFCore.API"
		}
		subgraph cluster1 {
			Service;
			Converter;
			Model;
			label="DvBCrud.EFCore.Services"
		}
		subgraph cluster2 {
			Repository;
			Entity;
			label="DvBCrud.EFCore.Core"
		}
		Repository -- Entity;
		Service -- Repository [label="Entity"];
		CrudController -- Service [label="Model"];
		Entity -- DB;
	}
'/>
