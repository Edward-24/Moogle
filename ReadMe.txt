-------------Eduardo Antonio Pestana Fern�ndez C-122-----------------------
---------------------------Proyecto MOOGLE!--------------------------------
Para utilizar el proyecto debe copiar los documentos de texto sobre los que desea realizar la b�squeda en la carpeta Content y ejecutar el comando correspondiente en la carpeta ra�z dependiendo de su Sistema Operativo ( �make dev� para Linux, y �dotnet watch run --project MoogleServer� para Windows).

Una vez abierto el navegador y arrancado el programa podr� escribir un texto en la barra de b�squeda y se le mostrar� los documentos m�s relacionados con ese texto , as� como un peque�o recorte de su contenido donde m�s aparece su b�squeda y una sugerencia por si su b�squeda no da suficientes resultados.
Puede hacer uso de los siquientes operadores:

Mayor importancia (*): (*palabra) Al colocar el operador delante de la palabra , esta tendr� el doble de importancia.
Menor importancia (-): (!palabra) Al colocar el operador delante de la palabra , esta tendr� la mitad de importancia. 
Existencia(^): (^palabra) Al colocar el operador delante de la palabra , esta tendr� que aparecer obligatoriamente en el documento.
No existencia(!): (!palabra) Al colocar el operador delante de la palabra , esta no podr� aparecer en el documento.

* Destacar que las palabras deben estar separadas por un espacio(de no ser as� no tendr� en cuenta ning�n operador) y cada una admite �nicamente un operador(si tiene m�s operadores solo reconocer� el primero a la izquierda). 