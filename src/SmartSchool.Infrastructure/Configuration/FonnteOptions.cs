namespace SmartSchool.Infrastructure.Configuration;

public class FonnteOptions
{
    public const string SectionName = "Fonnte";

    public string BaseUrl { get; set; } = "";

    public string Token { get; set; } = "";
}