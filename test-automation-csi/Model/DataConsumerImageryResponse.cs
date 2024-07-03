namespace test_automation_csi_api.Model;

public class DataConsumerImageryResponse
{
    public Guid ImageDataRequestId { get; set; }
    public Guid DataConsumerId { get; set; }
    public string DataConsumerReference { get; set; }
    public string RequestStatus { get; set; }
    public string EarliestAllowedTime { get; set; }
    public string LatestAllowedTime { get; set; }
    public string EarliestTimeOfDayLocal { get; set; }
    public string LatestTimeOfDayLocal { get; set; }
    public string LocalTimeZone { get; set; }
    public string SegmentGeometryJson { get; set; }
    public string SegmentGeometryWKT { get; set; }
    public bool AllowRearFacingCamera { get; set; }
    public bool AllowCabinCamera { get; set; }
    public string CountryOfDataCollection { get; set; }
    public string AreaRegionOrStateOfDataCollection { get; set; }
    public bool AllowStationaryCollection { get; set; }
    public int JourneyStartExclusionDistance { get; set; }
    public int JourneyEndExclusionDistance { get; set; }
}