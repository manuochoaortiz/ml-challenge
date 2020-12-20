# ml-challenge
Api de .NetCore usando el enfoque DDD y patrones CQRS y Repository

**Dockerización**  
Utilicé docker-compose version: '3.8' y multi-stage en el Docker File.  
Comando para ejecutar el docker compose y subir las imagenes de Api y Redis
```c=Production t=build docker-compose up --build```  

La Api se ejecuta en localhost:64139 y redis en localhost:6379.

Comando para bajar las imagenes  
```c=Production t=build docker-compose down```

**EndPoins:**  
**```{host}/TrackerApi/TrackByIp?ip={IpAddres}```**  
Retorna un JSON con la informacion de una ip.  
Ejemplo:
```json
{
  "ip": "5.6.7.8",
  "País": "Germany",
  "ISO Code": "DE",
  "Idiomas": [
	"German"
  ],
  "Fecha Actual": [
	"UTC+01:00 - 03:07 18/12/2020"
  ],
  "Distancia estimada": "11489 KM de (-34, -58) a (51, 9)",
  "Moneda": "EUR (1 USD = 0,817599 EUR)"
}
```
Azure & RedisLab Free Demo [https://manu-ml-challenge.azurewebsites.net/TrackerApi/TrackByIp?ip=5.6.7.8](https://manu-ml-challenge.azurewebsites.net/TrackerApi/TrackByIp?ip=5.6.7.8)  


**```{host}/TrackerApi/GetCounterCountry```**  
Retorna un JSON con informacion estadistica de las ip solicitadas
Ejemplo:
```json
{
  "Distancia mas lejana": "Germany 11489 KM",
  "Distancia mas cercana": "United States 8960 KM",
  "Distancia promedio": "9803 KM",
  "Estadisticas": [
	{
	  "Pais": "United States",
	  "Distancia": "8960 KM",
	  "Invocaciones": 10
	},
	{
	  "Pais": "Germany",
	  "Distancia": "11489 KM",
	  "Invocaciones": 5
	}
  ]
}
```  
Azure & RedisLab Free Demo [https://manu-ml-challenge.azurewebsites.net/TrackerApi/GetCounterCountry](https://manu-ml-challenge.azurewebsites.net/TrackerApi/GetCounterCountry)  
