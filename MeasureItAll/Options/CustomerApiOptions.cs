namespace MeasureItAll.Options;

public class CustomerApiOptions
{
    public const string SectionName = "CustomerApi";

    public string BaseUrl { get; set; } = string.Empty;

    public string ApiVersion { get; set; } = string.Empty;

    // Read from the environment variable CustomerApi__ApiKey, never from a committed file.
    public string ApiKey { get; set; } = string.Empty;
}
