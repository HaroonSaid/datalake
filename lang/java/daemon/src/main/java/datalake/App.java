package datalake;

import com.amazonaws.AmazonClientException;
import com.amazonaws.auth.profile.ProfileCredentialsProvider;
import com.amazonaws.services.kinesis.AmazonKinesis;
import com.amazonaws.services.kinesis.AmazonKinesisClientBuilder;

/**
 * Generate Kinesis Activity Events for a Data Lake Demo
 *
 */
public class App 
{
    public static void main( String[] args )
    {
    	ProfileCredentialsProvider credentialsProvider = new ProfileCredentialsProvider();
        try {
            credentialsProvider.getCredentials();
        } catch (Exception e) {
            throw new AmazonClientException(
                    "Cannot load the credentials from the credential profiles file. " +
                    "Please make sure that your credentials file is at the correct " +
                    "location (~/.aws/credentials), and is in valid format.",
                    e);
        }
        
        
        AmazonKinesis kinesis = AmazonKinesisClientBuilder.standard()
                .withCredentials(credentialsProvider)
                .withRegion("us-west-2")
                .build();
        
        KinesisClient client = new KinesisClient(kinesis,"name-1");
        Models.Activity recoord = new Models.Activity();
        client.recordAsync(record, "test-1");
       
    }
}
