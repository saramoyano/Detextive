# Detextive
Aplicacion de análisis de texto

DETEXTIVE busca servir como una herramienta de ayuda al proceso de investigación, permitiendo organizar, clasificar, etiquetar y visualizar la información. La aplicación tiene dos funcionalidades principales: etiquetado de fragmentos de un documento y creación de nubes de palabras. Este software se centra en el almacenamiento, reorganización y visualización de la información obtenida a partir de documentos, así como de la creada por el usuario. 
Detextive está aun en desarrollo y pretendo hacerle varios cambios.

- La aplicación fue desarrollada en VisualStudio Community 2019, v.16.5.4 con Plataforma Universal de Windows (UWP)
- PostgreSQL y EFCore (se utilizó Npgsql)

Instrucciones: 

- Clonar el proyecto
- Modificar transitoriamente la propiedad TargetFramework del archivo de proyecto para que apunte a .NET Core app 2.2 y a .NET Standard 2.0. Comentar la línea del TargetFramework bajo la etiqueta PropertyGroup y habilitar la de TargetFrameworks.
- A continuación recargar el proyecto. 
- Una vez recargado, se debe establecer el proyecto AccesoDatos como proyecto de inicio y realizar la migración utilizando la Consola del Administrador de paquetes con Add-Migration nombre_migracion. Si se produce un error, se deben descargar los otros dos proyectos.
- Cuando aparece el mensaje que indica que la migración fue exitosa, se aplican los cambios a la BB.DD. 
- Una vez realizada y comprobada la migración en BB.DD. se vuelve a modificar el archivo de configuración para que el proyecto pueda ser referenciado desde UWP. Quitar el comentario en TargetFramework y volver a comentar la línea siguiente. 
- Ejecutar

