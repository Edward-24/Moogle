-------------Eduardo Antonio Pestana Fernández C-122-----------------------
---------------------------Proyecto MOOGLE!--------------------------------
Para utilizar el proyecto debe copiar los documentos de texto sobre los que desea realizar la búsqueda en la carpeta Content y ejecutar el comando correspondiente en la carpeta raíz dependiendo de su Sistema Operativo ( “make dev” para Linux, y “dotnet watch run --project MoogleServer” para Windows).

Una vez abierto el navegador y arrancado el programa podrá escribir un texto en la barra de búsqueda y se le mostrará los documentos más relacionados con ese texto , así como un pequeño recorte de su contenido donde más aparece su búsqueda y una sugerencia por si su búsqueda no da suficientes resultados.
Puede hacer uso de los siquientes operadores:

Mayor importancia (*): (*palabra) Al colocar el operador delante de la palabra , esta tendrá el doble de importancia.
Menor importancia (-): (!palabra) Al colocar el operador delante de la palabra , esta tendrá la mitad de importancia. 
Existencia(^): (^palabra) Al colocar el operador delante de la palabra , esta tendrá que aparecer obligatoriamente en el documento.
No existencia(!): (!palabra) Al colocar el operador delante de la palabra , esta no podrá aparecer en el documento.

* Destacar que las palabras deben estar separadas por un espacio(de no ser así no tendrá en cuenta ningún operador) y cada una admite únicamente un operador(si tiene más operadores solo reconocerá el primero a la izquierda). 