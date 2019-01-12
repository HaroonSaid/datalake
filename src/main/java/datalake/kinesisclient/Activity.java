package datalake.kinesisclient;

import datalake.schema.ActivityOuterClass;

public interface Activity {
    void recordAsync(ActivityOuterClass.Activity record, String partitionKey);
}