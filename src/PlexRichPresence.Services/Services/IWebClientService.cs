namespace PlexRichPresence.ViewModels.Services;

public interface IWebClientService
{
    Task OpenAsync(string url);
}