This solution contains a web application for the first exercise of the Interparking test.  
The test description can be found [here](https://satellitbe-my.sharepoint.com/:b:/g/personal/tuan-tu_tran_satellit_be/ESrO8LcH4s1Ai1x7530VeT0BOorhEVdDQaPRRxFra_KSJw?e=4YzjTx)

## General application architecture

It's an MVC core application that uses a local SQLite database to persist data and queries the Azure Maps api for routing information.

The web application lives in the `InterparkingTestWebApp` project that references the `InterparkingTest.Application` class library project.

That class library contains the application logic exposed by the `IApplicationFacade` interface.  
That interface is implemented by `ApplicationFacade` that delegates work to:  
- `IRouteRepository` to list routes and get a particular route detail
- `IRouteModificationService` to add/update routes.

The route modification service is again a facade to:
- calculate the route distance by calling the `IRoutingService`
- calculate the fuel consumption by calling the `IFuelConsumptionService`
- persist the route by calling the `IRouteRepository`

The `RouteRepository` is implemented using EFCore.

There's a couple of class diagrams to illustrate this in the `Doc` folder of the application class library.

So the web application only really talks to the `IApplicationFacade`.

Then there are some tests in `InterparkingTest.Application.Tests`

## Route modification form

In the route creation/edit form, when the user searches for a parking, the web application makes requests to the Azure Maps search API to find
the coordinates of the parking.

## Dependencies

This application needs to be able to write to the local filesystem to persist the SQLite database.  
It will try to write it to the file `routes.sqlite3` where it is deployed.  
(consider making this configurable)

Furthermore, it needs an API key to communicate with the Azure Maps routing API and the search API.  
This key can be configured through the standard ASP.NET Core configuration mechanisms, under the key `AzureMapsOptions:ApiKey`.  
But for convenience, there's a hard-coded api key in the `AzureMapsRoutingService`.
