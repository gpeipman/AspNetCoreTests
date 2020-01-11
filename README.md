# AspNetCoreTests

This project is ASP.NET Core application to demonstrate my work on ASP.NET Core unit and integration tests. 
Projects are thin and clean having just minimal code to show how testing related issues are solved.

## Unit tests

Unit tests project demonstrates the following features:

* Unit testing controllers
    * Checking if correct view is returned
    * Faking current user correctly
* Script to generate and display code coverage reports

NB! You need to install report generator manually. Open unit tests project folder in command line and type the following command: 

```
dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
```

## Integration tests

Although there are simpler ways to get started with ASP.NET Core integration tests, the approach here is more 
flexible:

* Use custom appsettings.json file for integration tests
* Use custom web application startup class

## References

* [Using xUnit with ASP.NET Core](https://gunnarpeipman.com/aspnet-core-xunit/)
* [Code coverage reports for ASP.NET Core](https://gunnarpeipman.com/aspnet-core-code-coverage/)
* [Using custom appsettings.json with ASP.NET Core integration tests](https://gunnarpeipman.com/aspnet-core-integration-tests-appsettings/)
* [Using custom startup class with ASP.NET Core integration tests](https://gunnarpeipman.com/aspnet-core-integration-test-startup/)
* [Faking Azure AD identity in ASP.NET Core unit tests](https://gunnarpeipman.com/aspnet-core-azure-ad-unit-test/)
* Faking user in ASP.NET Core controller tests