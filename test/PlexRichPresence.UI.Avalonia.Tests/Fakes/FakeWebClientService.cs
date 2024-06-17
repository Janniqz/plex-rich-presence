using System.Collections.Generic;
using System.Threading.Tasks;
using PlexRichPresence.ViewModels.Services;

namespace PlexRichPresence.UI.Avalonia.Tests.Fakes;

public class FakeWebClientService : IWebClientService
{
    public List<string> OpenedUrls { get; } = [];

    public Task OpenAsync(string url)
    {
        OpenedUrls.Add(url);
        return Task.CompletedTask;
    }
}