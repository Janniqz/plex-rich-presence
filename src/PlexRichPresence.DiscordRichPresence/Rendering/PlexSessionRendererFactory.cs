using PlexRichPresence.Core;

namespace PlexRichPresence.DiscordRichPresence.Rendering;

public class PlexSessionRendererFactory
{
    public IPlexSessionRenderer BuildRendererForSession(PlexSession session) => session.MediaType switch
    {
        PlexMediaType.Movie => new MovieSessionRenderer(),
        PlexMediaType.Episode => new SeriesSessionRenderer(),
        PlexMediaType.Track => new MusicSessionRenderer(),
        PlexMediaType.Idle => new IdleSessionRenderer(),
        _ => new GenericSessionRenderer()
    };
}