namespace MeasureItAll.Options;

public class WingineerApiOptions
{
    public const string SectionName = "WingineerApi";

    public string BaseUrl { get; set; } = string.Empty;

    public string ApiVersion { get; set; } = string.Empty;

    // Read from the environment variable WingineerApi__ApiKey, never from a committed file.
    public string ApiKey { get; set; } = string.Empty;
}
