namespace SmartSchool.Infrastructure.Configuration;

public class WablasOptions
{
    public const string SectionName = "Wablas";

    public string BaseUrl { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public string SecretKey { get; set; } = string.Empty;
}