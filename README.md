# O que é o Seed

Seed é um projeto de template para construir sistemas com administrativos e até mesmo sites, o objetivo desse projeto é fornecer o esqueleto básico de um projeto dotnet core com uma API Rest, um FontEnd SPA Angular e um SSO com Identity serve 4, prover as camadas, as dependências básicas entre elas e pacotes principais como logs e acesso a dados. 

Depois de clonado esse repositório, existe uma projeto nele chamado **Gerador.Gen.Core** ele utiliza uma série de arquivos txts para construir as classes do projeto baseado em um banco de dados. Ou seja, basta modelar o banco e depois especificar o nome das tabelas no arquivo **ConfigContext**, algo como segue abaixo.

```
TableInfo = new UniqueListTableInfo
{
	new TableInfo().FromTable("Sample").MakeBack().MakeFront().AndConfigureThisFields(new  List<FieldConfig> {

		new FieldConfig(){

			Name = "descricao",
			TextEditor = true
		},
		new FieldConfig(){

			Name = "FilePath",
			Upload = true
		}

	}),
	new TableInfo().FromTable("SampleType").MakeBack().MakeFront(),
	new TableInfo().FromTable("SampleItem").MakeBack().MakeFront()
}
```

A estrutura é dividida em vários repositórios independentes que são gerenciados também pelo gerador na classe ** ConfigExternalResources **, ela vai apontar para os repositórios do git que representam os seguintes componentes:


1. Arquivos de Template para o Back-end
1. Arquivos de Templates para o Front-end
1. Um Framework para o Back-end
1. Um Framework para o Front-end
1. Um esqueleto de projeto para um sistema administrativo
1. Um esqueleto de projeto para um site convecional
1. Um esqueleto de projeto para toda a solução de front ao back


#Repositorio Atuais

1. [gerador-template-back](https://github.com/wilsonsantosnet/gerador-template-back)
1. [gerador-template-front](https://github.com/wilsonsantosnet/template-gerador-front-coreui-angular6.0)
1. [gerador-framework-back](https://github.com/wilsonsantosnet/backend-framework-common)
1. [gerador-framework-front](https://github.com/wilsonsantosnet/framework-angular6.0-CRUD)
1. [gerador-project-admin-front](https://github.com/wilsonsantosnet/gerador-project-admin-front)
1. [gerador-project-site-front](https://github.com/wilsonsantosnet/gerador-project-site-front)
1. [gerador-project-all-solution](https://github.com/wilsonsantosnet/gerador-project-all-solution)


###### Caso esteja interessado em baixar o SEED e rodar o gerador siga as instruções do artigo:
1. [Gerador de Código](https://medium.com/@wilsonsantos_66971/gerador-de-c%C3%B3digo-7e3c08981e43)

###### Para saber mais consulte essa lista de artigos relacionados ao gerador e aos frameworks citados acima 
1. [Gerador Init()](https://medium.com/@wilsonsantos_66971/brain-board-b3bf5e550cd9)


###### Digrama macro (obs.: está meio desatualizado, mas serve como referência)
![Diagrama 1](flow.png?raw=true "Flow")

