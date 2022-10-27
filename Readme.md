Debug locally with visual studio

This ode use Entity framework to build/run the database

Open Tools -> NuGet Package Manager > Package Manager Console(PMC) and run the following command in the PMC:

If you added new models run:
	Add-Migration Initial

Do update/create your local database use:
	Update-Database


The Add-Migration command generates code to create the initial database schema which is based on the model specified in the MovieContext class. The Initial argument is the migration name and any name can be used.

The Update-Database command runs the Up method in the Migrations/{time-stamp}_Initial.cs file, which creates the database.

Now, we will check the database created. Open View -> SQL Server Object Explorer.

To run this on your environment, in GitHub create the following Secrets:

	AZURE_WEBAPP_PUBLISH_PROFILE
	TENANTID
	DOMAIN
	CLIENTID
	AUDIENCE