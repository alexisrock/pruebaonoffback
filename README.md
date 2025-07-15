# 

<h1 align="center"> prueba pruebaonoffback </h1>



<h2 align="left"> Descripcion</h2>

<p>

Esta api rest fue  realizada como  prueba tecnica,  el api esta documentada con swagger, la idea

principal del api es que se pueda llevar un restristo de tareas a realizar

</p>

<h2 align="left"> Arquitectura</h2>

<p>

Esta api fue creada con una arquitectura limpia, la capa de infraestructura esta creada de tal forma que no tenga ninguna dependencia con el orm,

tambien se implemento mapper como libreria para gestionar la tranformacion a los dto, 

se utilizo la injeccion de dependencias de net 8, tambien tiene validacion con fluent validation y se implemento un middleware para manejar los errores.

</p>

<h2 align="left"> Tecnologias utilizadas</h2>

<p>

&nbsp;   <li>Net 8</li>

&nbsp;   <li>Entityframeworkcore</li>

&nbsp;   <li>Mapper</li>

&nbsp;   <li>Nlog</li>

&nbsp;   <li>NUnit</li>



</p>



<h2 align="left"> Prerequisitos</h2>

<p>

&nbsp;    

<ul> 

&nbsp;       <li> se debe tener instalado  el sdk de net8</li>

&nbsp;       <li> tener instaldo el comando dotnet ef para correr las migraciones</li> 

&nbsp;       <li> si no se tiene instalado ejecutar el siguiente comando      "dotnet tool install --global dotnet-ef" </li> 





</ul>

</p>





<h2 align="left"> Instalacion</h2>

<p>

&nbsp;    

<ul> 

&nbsp;       <li> en la carpeta del proyecto ejecutar el comando "dotnet restore"  </li>  

&nbsp;       <li> en la carpeta del proyecto ejecutar el comando "dotnet build"  </li>  

&nbsp;       <li> a continuacion ubicarse dentro de la carpeta  Infrastructure</li> 

&nbsp;       <li> ejecutar el comando  "dotnet ef migrations add InitialCreate --project ../Infrastructure --startup-project ../API"  </li> 

&nbsp;       <li> luego  ejecutar el comando "dotnet ef database update --project ../Infrastructure --startup-project ../API"  </li> 

&nbsp;       <li> y si desea desde visual studio puede iniciar el proyecto  </li> 

&nbsp;       

</ul>

</p>





<h2 align="left"> Ejecucion de los test</h2>

<p>

&nbsp;    

<ul> 

&nbsp;       <li> Tener instalado el generador de reportes de dotnet</li>

&nbsp;       <li> si no se tiene instalado ejecutar el siguiente comando "dotnet tool install --global dotnet-reportgenerator-globaltool"</li>

&nbsp;       <li> Ingresar a la carpeta que dice Test </li> 

&nbsp;       <li> luego ejecutar el comando 'dotnet test --collect:"XPlat Code Coverage"' </li> 

&nbsp;       <li> el xml que se genera se encuentra dentro de la carpeta   TestResults\\0cf8398f-c6d1-4541-8d68-16dceedc3436 esta ultima puede variar con el nombre de coverage.cobertura </li> 

&nbsp;       <li>  luego de generar el xml procedemos a generar el reporte con el comando 'reportgenerator -reports:TestResults/\*/coverage.cobertura.xml -targetdir:CoverageReport -reporttypes:Html'  </li> 

&nbsp;       <li> los resultados generados van hacer guardados en la carpeta CoverageReport el reporte que se debe validar es Api\_TareaController y Application\_TareaHandler que son las clases que se le realizaron los test </li>

</ul>

</p>





<h2 align="left"> Version del aplicativo</h2>

<p> V.0.0.1</p>

