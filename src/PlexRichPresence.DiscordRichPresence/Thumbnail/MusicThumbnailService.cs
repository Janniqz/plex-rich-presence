using System.Net;
using System.Reflection;
using System.Text;
using MetaBrainz.Common;
using MetaBrainz.MusicBrainz;
using MetaBrainz.MusicBrainz.CoverArt;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using Plex.ServerApi.PlexModels.Audio;
using PlexRichPresence.Core;
using PlexRichPresence.Tools.Helpers;

namespace PlexRichPresence.DiscordRichPresence.Thumbnail;

public class MusicThumbnailService : ThumbnailServiceBase
{
    private string _currentAlbum = string.Empty;
    private string _currentArtist = string.Empty;

    private readonly Query _musicBrainzClient;
    private readonly CoverArt _coverArtClient;
    
    private readonly char[] LUCENE_ESCAPE_CHARACTERS = ['+', '-', '&', '|', '!', '(', ')', '{', '}', '[', ']', '^', '\"', '~', '*', '?', ':', '\\'];
    
    public MusicThumbnailService()
    {
        var version = new Version(Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version).ToString(3);
        _musicBrainzClient = new Query("PlexRichPresence", version, "mailto:mail@janniqz.moe");
        _coverArtClient = new CoverArt("PlexRichPresence", version, "mailto:mail@janniqz.moe");
    }

    protected override Task<string?> GetThumbnailURL_Internal(PlexSession session)
    {
        _currentAlbum = session.MediaParentTitle;
        _currentArtist = session.MediaGrandParentTitle;
        
        var query = $"release:{EscapeLuceneQuery(_currentAlbum)} AND (artist:{EscapeLuceneQuery(_currentArtist)} OR label:{EscapeLuceneQuery(_currentArtist)})";
        var releaseGroups = _musicBrainzClient.FindReleaseGroupsAsync(query).Result;
        foreach (var releaseGroup in releaseGroups.Results)
        {
            if (!VerifyReleaseGroup(releaseGroup.Item, _currentAlbum, _currentArtist))
                continue;
            
            foreach (var release in releaseGroup.Item.Releases)
            {
                try
                {
                    var covers = _coverArtClient.FetchReleaseAsync(release.Id).Result;
                    var image = covers.Images[0];
                    return Task.FromResult(image.Thumbnails.Small?.AbsoluteUri);
                }
                catch (Exception e)
                {
                    if (e is AggregateException { InnerExceptions.Count: 1 } ae)
                        e = ae.InnerExceptions[0];
                    if (e is HttpError { Status: HttpStatusCode.NotFound })
                        continue;
                    
                    // TODO Add Logging
                    
                    throw;
                }
            }
        }
        
        return Task.FromResult((string?) null);
    }

    protected override bool NeedsUpdate(PlexSession session) => session.MediaParentTitle != _currentAlbum || session.MediaGrandParentTitle != _currentArtist;
    
    private string EscapeLuceneQuery(string query)
    {
        var sb = new StringBuilder(query.Length);
        foreach (var c in query)
        {
            if (LUCENE_ESCAPE_CHARACTERS.Contains(c))
                sb.Append('\\');
            sb.Append(c);
        }
        return sb.ToString();
    }

    private bool VerifyReleaseGroup(IReleaseGroup release, string targetAlbum, string targetArtist, double similarityThreshold = 0.8)
    {
        // If there are no Releases, ignore the Group
        if (release.Releases == null)
            return false;
        
        // If the Album doesn't match, ignore
        if (release.Title == null || targetAlbum.CompareStrings(release.Title) < similarityThreshold)
            return false;

        // If the Artist / Label doesn't match, ignore
        return release.ArtistCredit?.Any(credit =>
        {
            var artist = credit.Artist;
            return (credit.Name != null && targetArtist.CompareStrings(credit.Name) >= similarityThreshold) ||
                   artist != null && (
                       (artist.Name != null && targetArtist.CompareStrings(artist.Name) >= similarityThreshold) ||
                       (artist.SortName != null && targetArtist.CompareStrings(artist.SortName) >= similarityThreshold) ||
                       (artist.Disambiguation != null && targetArtist.CompareStrings(artist.Disambiguation) >= similarityThreshold));
        }) ?? false;
    }
}