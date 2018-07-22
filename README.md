# datalake

  Create a data lake in S3 using AWS Kinesis and Analyze the data with AWS Glue

## Usage

### Building

  The project require dotnet core 2.1 SDK to be installed, download from https://www.microsoft.com/net/download/dotnet-core/2.1

#### To build
  
  clone the respostory.
  git clone https://github.com/HaroonSaid/datalake.git.
  cd datalake.
  dotnet build.

### Deploying FirhoseLambda

#### Prerequists

  You have an AWS Account.

  You need S3 bucket created.

  You need Kinesis Stream Configured

  You need Kinesis Firhose Configured and pointing to S3 bucket (with tranformation enabled)

### Deployment

  build and deploy the firehose lambda

  cd datalake/LambdaFirehose
  dotnet build -c Release
  dotnet lambda deploy-function -cfg deploy.json 

### Configure Firehose

  Configure Kinesis Firehose to invoke the lambda function via the console or command line
