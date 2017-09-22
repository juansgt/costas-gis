# costas-gis
Back-end del sistema de información geográfica para tratamiento de
las ocupaciones del espacio de dominio público marítimo terrestre. Su implementación está escrita en .NET 4.5, en concreto: Web API 2 para los servicios Web RESTful, Entity Framework 6 como ORM.
<p></p>
Los servicios más representativos que la aplicación expone son:
<p></p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;● Importación de datos de las ocupaciones desde archivo KML incluyendo su geometria<p></p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;● Operaciones CRUD sobre el conjunto de ocupaciones<p></p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;● Conversión de las geometrias de cada ocupación en coordenadas cartesianas a un sistema angular de &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;latitud y longitud para ser consumidas por la vista<p></p>


Forma parte del Proyecto de Fin de Máster en Ingeniería Informática (Facultad de informática de la UDC). Esta aplicación expone a través de un API RESTful los servicios que los siguientes clientes consumen:
<p></p>
Cliente Web https://github.com/juansgt/costas-gis-web
<p></p>
Cliente Android https://github.com/juansgt/costas-gis-android
<p></p>
Para información detallada del diseño e implementación puedes descargar la memoria del proyecto aquí: 
<p></p>
https://drive.google.com/file/d/0B-VXo11O1dmpc3Vma0tQTmJIR00/view?usp=sharing
