# CiberGestion

Se usó:
 - VisualStudio 2022
 - .NET6
 - ANGULAR 15.2.4
 - Node 16.15.1
 - npm 8.11.0
 - SQL SERVER
 - BootStrap
 
 


NOTA:
 - Tener en cuenta que el proyecto ubicado en la carpeta ControlUsuarios es la parte Backend y el proyecto(desarrollado en angular) ubicado en la carpeta control-usuario-front es la parte frontend
 - En el archivo appsettings.json del BACKEND en el key DefaultConnection especificar el servidor de su base de datos, y el usuario-contraseña si es que lo tuviera.
 - En el archivo appsettings.json del BACKEND en el key Path especificar la ruta de los logs de los tiempos de respuestode los servicios, caso contrario, se guardará en la raíz del
 - Ejecutar ambos proyectos para que pueda funcionar. Para compilar el front, abrir el proyecto FRONTEND en el VisualStudioCode luego en la TERMINAL ejecutar el comando [npm run start] sin los "[]". Para compilar el BACKEND, abrir la solución con el VisualStudio 2022 y compilarlo.
 - En el caso de que el puerto del local host del BACKEND sea distinto a "7229", actualizar el nuevo puerto en la variable global del FRONTEND llamado URI_API ubicado en el archivo "control-usuario-front\src\enviroments\environment.ts" proyecto.
 - Cabe indicar que los proyectos están estructurados en N-CAPAS
 - Ejecutar el script que se encuentra en la carpeta "Base de datos" en el SQL SERVER para crear la BD, tablas, procedures y datos necesarios.

Sin más, espero que la solución del reto se haya cumplido con este proyecto.
