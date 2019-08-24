# enrouteapi

To track the current endpoints provided by the API:

- Dublin Bus:
  - http://enrouteapi.azurewebsites.net/api/busstopinfo this takes about 30 seconds to run, returns a JSON list of every DublinBus and GoAheadIreland bus stop.
  
  - http://enrouteapi.azurewebsites.net/api/busstopinfofast this is a fast version of the above endpoint, not guaranteed to be as up-to-date though.
  
  - http://enrouteapi.azurewebsites.net/api/busrti/<stopId> this returns the real-time-information for the given bus stopId.
  
  - http://enrouteapi.azurewebsites.net/api/busrouteinfo/<routeId> this returns the JSON list of stop along the given routeId.
  
  Luas:
  
  - http://enrouteapi.azurewebsites.net/api/luasstopinfo live-calculated list of all the luas stops in JSON.
  
  - http://enrouteapi.azurewebsites.net/api/luasstopinfofast faster but not live-calulated version of the above.
  
  - http://enrouteapi.azurewebsites.net/api/luasrti/<stopAbrev> returns JSON formatted real-time-information for the given
  
  
  To Do:
  
  - http://enrouteapi.azurewebsites.net/api/luascoords/{id} Return coordinates for entered Luas Stop
  - http://enrouteapi.azurewebsites.net/api/buscoords/{id} Return coordinates for entered Bus Stop
  - http://enrouteapi.azurewebsites.net/api/traincoords/{id} Return coordinates for entered Train Station
  
  - http://enrouteapi.azurewebsites.net/api/luasSearch/ Returns all the luas stops in Geographical Order
  - http://enrouteapi.azurewebsites.net/api/trainSearch/ Returns all the train stops in Alphabetical Order
  - http://enrouteapi.azurewebsites.net/api/busSearch/ Returns all the bus stops in Numerical Order
  
  - http://enrouteapi.azurewebsites.net/api/luasSearch/{id} Returns all the luas stops in Alphabetical Order filtered to          start with what you have entered
  
  - http://enrouteapi.azurewebsites.net/api/trainSearch/{id} Returns all the train stops in Alphabetical Order filtered to start with what you have entered
  
  - http://enrouteapi.azurewebsites.net/api/busSearch/{id} Returns all the bus stops in Numerical Order filtered to start with what you have entered

  - http://enrouteapi.azurewebsites.net/api/busrouteoperator Returns bus route operator