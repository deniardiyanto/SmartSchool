public class WablasSendResponse
{
    public bool Status { get; set; }

    public string Message { get; set; } = string.Empty;

    public WablasDataResponse Data { get; set; } = new();
}