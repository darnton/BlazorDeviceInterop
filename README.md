# BlazorDeviceInterop
Blazor library for interacting with browser Web APIs.

Currently offering:

- A GeolocationService to retrieve the latitude and longitude of your device from the browser.

## Setup
- Install NuGet package *Darnton.Blazor.DeviceInterop* into your Blazor project
- Add the AddScoped command to your services in the DI container (in Program.cs of your Blazor web project)
```
builder.Services
    .AddScoped<IGeolocationService, GeolocationService>()
```
- In the code section of your Razor page, add a property to GeolocationService, with an inject attribute (so the DI can populate the property at run time)
```
    [Inject] 
    public IGeolocationService GeolocationService { get; set; }
```

## Usage

Call *await GeolocationService.GetCurrentPosition()* to get your device's current location.

Example code:

```
        var currentPositionResult = await GeolocationService.GetCurrentPosition();
        var lat = currentPositionResult.Position.Coords.Latitude;
        var lng = currentPositionResult.Position.Coords.Longitude;
        var alt = currentPositionResult.Position.Coords.Altitude;
        Console.WriteLine($"Current device location is latitude {lat}, longitude {lng}, altitude {alt}.");
```

See also:

- Example code in BlazorDeviceTestRig folder of this github project
- [Geolocation in Blazor](https://darnton.co.nz/2020/11/29/geolocation-in-blazor/)
