package datalake.kinesisclient;

import java.util.concurrent.CompletableFuture;
import org.apache.logging.log4j.core.Logger;

import datalake.schema.ActivityOuterClass;
import software.amazon.awssdk.services.kinesis.*;
import software.amazon.awssdk.services.kinesis.model.*;
import software.amazon.awssdk.core.SdkBytes;

public class KinesisClient implements Activity {
    private final KinesisAsyncClient kinesis;
    private final String streamName;
    private Logger logger;

    public KinesisClient(KinesisAsyncClient kinesis, String streamName, Logger logger) {
        this.kinesis = kinesis;
        this.streamName = streamName;
        this.logger = logger;
    }

    public void recordAsync(ActivityOuterClass.Activity record, String partitionKey) {
        PutRecordRequest putRecordRequest = PutRecordRequest.builder()
                .streamName(streamName)
                .partitionKey("partition-key-1")
                .data(SdkBytes.fromByteArray(record.toByteArray()))
                .build();
        PutRecordResponse response  = kinesis.putRecord(putRecordRequest).join();
        logger.debug("ShardId" + response.shardId());
    }
}