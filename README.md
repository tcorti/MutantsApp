<h2><b>Mutants App</b></h3>

Introducción:

Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar
contra los X-Men.
Por lo tanto, quiere desarrollar un proyecto que detecte si un humano es mutante basándose en su secuencia de ADN. 

Esta es una API simple desarrollada en C# que consiste en determinar si una secuencia enviada pertenece o no a un mutante basándose en las siguientes reglas:

Se recibe como parámetro un array de Strings que representan cada fila de una tabla de (NxN) con la secuencia del ADN. Las letras de los Strings solo pueden ser: (A,T,C,G), las
cuales representa cada base nitrogenada del ADN.
Se sabe que un humano es mutante, si se encuentra más de una secuencia de cuatro letras iguales, de forma oblicua, horizontal o vertical.

<h5><b>Compilar Localmente</b></h5>
- Clonar el proyecto.
Nota: Se recomienda usar el IDE Visual Studio 2019 para mayor facilidad al compilar y testear.

- UnitTests: Si se utiliza el IDE se puede seleccionar la opción “Run Tests” sobre el proyecto Mutants.UnitTests para correr los tests unitarios (No se requiere DB local).

- API: Ejecutando el proyecto “Mutants” se puede navegar desde el browser a la URL https://localhost:5001/swagger (siendo 5001 el default port), desde donde se puede probar la API localmente usando swagger (Testear el endpoint de Stats requiere crear una DB local para que funcione)

- DB: Para testear usando una DB local, crear una base de datos en SQL Server llamada “Mutants” y luego correr en la misma el Script “CreateDnaTable.sql” que se encuentra en la carpeta Scripts del proyecto Mutants, para crear la tabla necesaria. Finalmente, reemplazar la ConnectionString "Connection" en appsettings.json por la adecuada a la base creada.

<h5><b>Api publica</b></h3>

Una demo de la API se encuentra Hosteada en azure, se puede acceder con la URL: https://mutants17.azurewebsites.net/


Para determinar si una secuencia pertenece a un mutante hacer un POST a https://mutants17.azurewebsites.net/mutant. 
Ejemplo del json a enviar en la request:
{"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]}

Para ver las estadísticas hacer un GET a https://mutants17.azurewebsites.net/stats
