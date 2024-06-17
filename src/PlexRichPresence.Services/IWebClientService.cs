namespace PlexRichPresence.Services;

public interface IWebClientService
{
    Task OpenAsync(string url);
}