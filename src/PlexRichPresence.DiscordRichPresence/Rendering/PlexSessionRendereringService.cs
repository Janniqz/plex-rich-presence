using DiscordRPC;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class PlexSessionRenderingService
{
    private readonly PlexSessionRendererStore _rendererStore;
    private readonly ILogger<PlexSessionRenderingService> _logger;

    public PlexSessionRenderingService(PlexSessionRendererStore rendererStore, ILogger<PlexSessionRenderingService> logger)
    {
        _rendererStore = rendererStore;
        _logger = logger;
    }

    public RichPresence? RenderSession(PlexSession session)
    {
        if (session.MediaType == PlexMediaType.Idle)
            return null;
        
        var renderedSession = _rendererStore
            .GetSessionRenderer(session)
            .RenderSession(session);

        _logger.LogInformation("Rendered Plex Session : {Session}", JsonConvert.SerializeObject(renderedSession));

        return renderedSession;
    }
}