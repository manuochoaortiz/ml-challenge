# ml-challenge
.NetCore Api Service developed with DDD architecture and CQRS pattern.

Docker Implementation:

EndPoins:
	{host}/TrackerApi/TrackByIp?ip={IpAddres}
	returns a json with Ip information, ex:
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
	
	{host}/TrackerApi/GetCounterCountry
	Returns a JSon with statistical information of request
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