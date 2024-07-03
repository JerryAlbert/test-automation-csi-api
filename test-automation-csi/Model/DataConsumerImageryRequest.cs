namespace test_automation_csi_api.Model;

public class DataConsumerImageryRequest
{
    public string Id { get; set; }
    public string DataConsumerId { get; set; }
    public string DataConsumerReference { get; set; }
    public string RequestStatus { get; set; }
    public string SegmentGeometryJson { get; set; }
    public string RequestType { get; set; }
    public string RequestTypePurpose { get; set; }
    public DateTime EarliestAllowedTime { get; set; }
    public DateTime LatestAllowedTime { get; set; }
    public TimeSpan EarliestTimeOfDayLocal { get; set; }
    public TimeSpan LatestTimeOfDayLocal { get; set; }
}