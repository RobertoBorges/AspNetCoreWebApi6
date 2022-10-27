#Demo AspNet API using Microsoft Identity library for Authentication

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

This workflow will build and push a .NET Core app to an Azure Web App when a commit is pushed to your default branch.

This workflow assumes you have already created the target Azure App Service web app.
For instructions see https://docs.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net60&pivots=development-environment-vscode

To configure this workflow:

1. Download the Publish Profile for your Azure Web App. You can download this file from the Overview page of your Web App in the Azure Portal.
    For more information: https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel#generate-deployment-credentials

2. Create a secret in your repository named AZURE_WEBAPP_PUBLISH_PROFILE, paste the publish profile contents as the value of the secret.
    For instructions on obtaining the publish profile see: https://docs.microsoft.com/azure/app-service/deploy-github-actions#configure-the-github-secret

 3. Change the value for the AZURE_WEBAPP_NAME. Optionally, change the AZURE_WEBAPP_PACKAGE_PATH and DOTNET_VERSION environment variables below.

For more information on GitHub Actions for Azure: https://github.com/Azure/Actions
For more information on the Azure Web Apps Deploy action: https://github.com/Azure/webapps-deploy
For more samples to get started with GitHub Action workflows to deploy to Azure: https://github.com/Azure/actions-workflow-samples


To run this on your environment, in GitHub create the following Secrets:

	AZURE_WEBAPP_PUBLISH_PROFILE
	TENANTID
	DOMAIN
	CLIENTID
	AUDIENCE