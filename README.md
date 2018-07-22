# datalake

  Create a data lake in S3 using AWS Kinesis and Analyze the data with AWS Glue

## Usage

### Building

  The project require dotnet core 2.1 SDK to be installed, download from https://www.microsoft.com/net/download/dotnet-core/2.1

#### To build
  
  clone the respostory

  git clone https://github.com/HaroonSaid/datalake.git

  cd datalake

  dotnet build

### Deploying the Firehose Lambda

#### Prerequists

  You have an AWS Account, you have installed aws cli on your computer

  You have created S3 bucket.

  You have created Kinesis Stream

  You have configure Kinesis Data Firhose pointing to S3 bucket (with tranformation enabled)

### Deploying Lambda 

  build and deploy the firehose lambda, edit deploy.json with the correct IAM role

  cd datalake/LambdaFirehose

  dotnet build -c Release

  dotnet lambda deploy-function -cfg deploy.json

### Configure Firehose

  Configure Kinesis Firehose to invoke the lambda function via the console or command line

### Test with deamon to generate data activity to the kinesis stream

  cd deamon

  dotnet run -s myStreamName -r us-east-1 (optional, defaults to us-east-1)