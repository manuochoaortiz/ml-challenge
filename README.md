# ml-challenge
.NetCore Api Service developed with DDD architecture and CQRS pattern.

**Docker Implementation:**  
I use docker-compose version: '3.8', just rune the next command to run the Api and Redis in docker  
```c=Production t=build docker-compose up --build```  

Api is running on localhost:64139 port and redis on localhost:6379.

To shut down the images run the command  
```c=Production t=build docker-compose down```

**EndPoins:**  
**```{host}/TrackerApi/TrackByIp?ip={IpAddres}```**  
Returns a json with Ip information.  
Example:
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
Returns a JSon with statistical information of request  
Example:
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
