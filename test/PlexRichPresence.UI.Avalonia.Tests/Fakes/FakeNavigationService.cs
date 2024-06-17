using System.Threading.Tasks;
using PlexRichPresence.Services;

namespace PlexRichPresence.UI.Avalonia.Tests.Fakes;

public class FakeNavigationService : INavigationService
{
    public string CurrentPage { get; private set; } = string.Empty;

    public Task NavigateToAsync(string page)
    {
        CurrentPage = page;
        return Task.CompletedTask;
    }
}