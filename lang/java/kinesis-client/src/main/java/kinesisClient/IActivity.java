package datalake;

public interface IActivity {
    void recordAsync(Modlels.Activity record, String partitionKey);
}