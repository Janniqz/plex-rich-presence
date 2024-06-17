using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PlexRichPresence.Services;

namespace PlexRichPresence.UI.Avalonia.Services;

public class WebClientService : IWebClientService
{
    public Task OpenAsync(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                Process.Start("xdg-open", url);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                Process.Start("open", url);
            else
                throw;
        }

        return Task.CompletedTask;
    }
}