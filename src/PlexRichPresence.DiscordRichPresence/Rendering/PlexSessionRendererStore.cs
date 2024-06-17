using PlexRichPresence.Core;
using PlexRichPresence.DiscordRichPresence.Interfaces;
using PlexRichPresence.DiscordRichPresence.Thumbnail;
using PlexRichPresence.Services;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class PlexSessionRendererStore
{
    private readonly Dictionary<PlexMediaType, IPlexSessionRenderer> _renderers = new();
    
    public PlexSessionRendererStore(IWebClientService webClientService)
    {
        var musicThumbnailService = new MusicThumbnailService();
        var videoThumbnailService = new VideoThumbnailService(webClientService);
        
        _renderers[PlexMediaType.Movie] = new MovieSessionRenderer(videoThumbnailService);
        _renderers[PlexMediaType.Episode] = new SeriesSessionRenderer(videoThumbnailService);
        _renderers[PlexMediaType.Track] = new MusicSessionRenderer(musicThumbnailService);
        _renderers[PlexMediaType.Idle] = new IdleSessionRenderer();

        _renderers[PlexMediaType.Unknown] = new GenericSessionRenderer();
    }
    
    public IPlexSessionRenderer GetSessionRenderer(PlexSession session) => _renderers[session.MediaType];
}