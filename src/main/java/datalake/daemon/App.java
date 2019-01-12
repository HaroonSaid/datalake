package datalake.daemon;

import software.amazon.awssdk.regions.Region;
import software.amazon.awssdk.services.kinesis.KinesisAsyncClient;

/**
 * Generate Kinesis Activity Events for a Data Lake Demo
 *
 */
public class App 
{
    public static void main( String[] args )
    {
        Region region = Region.EU_WEST_2;
        KinesisAsyncClient kinesis = KinesisAsyncClient.builder()
                .region(region)
                .build();
        

    }
}
