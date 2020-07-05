# DateTimeSubmission

[![Build Status](https://dev.azure.com/christophbergmeister/DateTimeSubmission/_apis/build/status/bergmeister.DateTimeSubmission?branchName=master)](https://dev.azure.com/christophbergmeister/DateTimeSubmission/_build/latest?definitionId=66&branchName=master)

# High level design

An Azure function receives a POST request and via an output binding, places a DTO (data transfer object) into Cosmos DB. The DTO object is in a separate project for decoupling purposes.
The benefit of this serverless setup is that it is highly elastic and high availability, global data distribution,  request queuing or load balancing are all being taken care of automatically. It should be noted that CosmosDB has a higher pricing point and Azure storage could also be used as an alternative. It uses .Net Core 3.1 for the runtime, which is one of the fastest runtimes available but another language would also be possible. It has built in monitoring using application insights and CosmosDB automatically takes a backup every 4 hours.

# Setup

1. Create a resource group.
2. Create an Azure DevOps pipeline pointing to the [.azure-pipelines.yaml](.azure-pipelines.yaml) file and a service endpoint named `DateTimeSubmission` for the above resource group
3. Create a default Azure Function based on .Net runtime on Windows in the created resource group
4. Create a default Cosmos DB in the created resource group
5. In the Azure DevOps pipeline, add a secret variable named `CosmosDBConnectionString` and give it the value of the Cosmos DB connection string
6. Run the pipeline to build and deploy the code to the Azure function.
7. Test it by issuing `POST` request to the function URL of the deployed Azure function named `DateTimeSubmission`. You will see the entry in Cosmos DB.

# Future work that could be done

- Automate creation of Azure Function and Cosmos DB, ideally using ARM or terraform.
- Split the deployment build into separate build and deployment and parameterise it to handle multiple environments.
- Consider multi region deployment of Azure functions, private endpoint, API versioning or different authentication methods (at the moment it is public but key based). Consider using a Linux based runtime.
- Consider geo redundance for Cosmos DB, availability zones, private endpoint, different consistency options for better performance.
- Write unit and E2E tests and integrate into CI.
- Async APIs
