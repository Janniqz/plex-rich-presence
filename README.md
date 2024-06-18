# Plex Rich Presence

Plex Rich Presence is a multiplatform .NET 8 GUI App that allows you to display your current PLEX session in your Discord Rich presence status.

<img src="https://github.com/Janniqz/plex-rich-presence/blob/master/src/PlexRichPresence.UI.Avalonia/Assets/plex-rich-presence.png?raw=true" width="250" height="250">

New features from version 2.0 : 

- Supports non-admin users
- Supports choosing a server
- CLI version
- PLEX SSO Login

## Release Version

Releases for windows and linux can be found [here](https://github.com/Janniqz/plex-rich-presence/releases/latest)

## Screenshots

![screenshots](screenshots/login.png)

![screenshots](screenshots/server.png)

![screenshots](screenshots/activity.png)

## Build & Run form sources

Requires .NET 8+ SDK

```
cd src/PlexRichPresence.UI.Avalonia
dotnet run
```

## Libraries used

- [AvaloniaUI](https://avaloniaui.net/)
- [.NET MVVM Toolkit](https://github.com/CommunityToolkit/)
- [Moq](https://github.com/devlooped/moq)
- [PlexApi](https://github.com/jensenkd/plex-api)
- [Discord RPC .NET](https://github.com/Lachee/discord-rpc-csharp)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [MetaBrainz.MusicBrainz](https://github.com/Zastai/MetaBrainz.MusicBrainz)
- [MetaBrainz.MusicBrainz.CoverArt](https://github.com/Zastai/MetaBrainz.MusicBrainz.CoverArt)
- Microsoft DI

## Special Thanks

[@Ombrelin] initial development<br>
[@GrandKhan] for the logo

Thanks to [Discord](https://discord.com/) and [PLEX Media Server](https://plex.tv)
