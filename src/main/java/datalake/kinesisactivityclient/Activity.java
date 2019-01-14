package datalake.kinesisactivityclient;

import datalake.schema.ActivityOuterClass;

public interface Activity {
    void recordAsync(ActivityOuterClass.Activity record, String partitionKey);
}