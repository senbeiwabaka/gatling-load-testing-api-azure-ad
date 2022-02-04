# gatling-load-testing-api-azure-ad
Using gatling load tester on api secured by Azure AD

Required:
* DotNet 6
* Code Editor such as VSCode
* Java 11
* SBT/Maven

Install the required items above. Setup an Azure AD registration for a Web API.

You will need to go to /api/appsettings.json and add the `domain`, `tenatid`, and `clientid` from your app registration. You can find the `domain` by typing in "Azure Active Directory" into the search and going to that specific blade. From there on the overview page look for the "Primary Domain" and use that. You can find the `tenantid` and `clientid` on the app registration overview page.

For the API use [README](./api/README.md)

For the load tester use [README](./loadtesting/README.md)