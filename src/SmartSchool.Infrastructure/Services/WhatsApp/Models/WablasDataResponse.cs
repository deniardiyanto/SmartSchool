public class WablasDataResponse
{
    public string DeviceId { get; set; } = string.Empty;

    public int Quota { get; set; }

    public List<WablasMessageResponse> Messages { get; set; }
        = new();
}