# Bienvenidos al proyecto de Master Workshop

Este es un proyecto basado en la gestion de inventarios de Ferreterias, aunque puede ser adaptados a mas tipos de negocios como E-Commerces o tiendas de electronicos.

Las tecnologias utilizadas en este proyectos son: **AspNetCore** para el manejo del backend y **Angular** para el manejo del frontend

# Requisitos para correr el proyecto:

## Backend: 

Para el backend es necesario tener instalado [.Net](https://dotnet.microsoft.com/es-es/), preferiblemente en su ultima version. Para iniciar la aplicacion de la API basta con entrar a la carperta ``` ./master-workshop-api ``` y ejecutar el siguiente comando en la terminar

```bash
  dotnet run
```

El servidor por defecto corre en ``` http://localhost:5002```. La api posee una interfaz grafica generada automaticamente en **Swagger** y se puede acceder en la siguiente url ```http://localhost:5002/swagger/index.html```, ahi se pueden ver todos los endpoints y ejecutar comandos de ejemplo.

**NOTA:** La api no posee una base de datos, por lo que los datos estan guardados en memoria y solo persistiran siempre y cuando la api este corriendo, si es reiniciada, los valores agregados se perderan y volveran a los por defecto.

## Frontend 

Para el frontend los requisitos son un poco mas, es necesario tener instalado [**NodeJS**](https://nodejs.org/en). El frontend esta desarrollado en [**Angular**](https://angular.dev/).

Una vez instalado NodeJS deben instalar la CLI de Angular con el siguiente comando.

```bash
  npm install -g @angular/cli
```

Una vez instalado la CLI de angular se debe acceder a la ruta donde esta el proyecto ```/master-workshop-frontend``` e instalar las dependencias de node (Los famosos node_modules) con el siguiente comando:

```bash
  npm install
```

Una vez instaladas todas las dependencias se puede ejecutar el commando ``` ng serve``` para correr el proyecto de manera local.
El proyecto siempre se inicia en ```http://localhost:4200```.

### Para que el proyecto funcione correctamente debe ejecutarse tanto la API como el Frontend al mismo tiempo


