using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plex.ServerApi.Clients.Interfaces;
using Plex.ServerApi.PlexModels.OAuth;
using PlexRichPresence.Services;

namespace PlexRichPresence.UI.Avalonia.ViewModels;

public partial class LoginPageViewModel : ObservableObject
{
    private readonly IPlexAccountClient _plexClient;
    private readonly INavigationService _navigationService;
    private readonly IStorageService _storageService;
    private readonly IWebClientService _webClientService;

    public LoginPageViewModel(IPlexAccountClient plexClient, INavigationService navigationService, IStorageService storageService, IWebClientService webClientService)
    {
        _plexClient = plexClient;
        _navigationService = navigationService;
        _storageService = storageService;
        _webClientService = webClientService;
    }

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(LoginWithCredentialsCommand))]
    private string _login = string.Empty;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(LoginWithCredentialsCommand))]
    private string _password = string.Empty;

    [RelayCommand(AllowConcurrentExecutions = false, CanExecute = "CanLoginWithCredentials")]
    private async Task LoginWithCredentials()
    {
        var account = await _plexClient.GetPlexAccountAsync(Login, Password);
        await StoreTokenAndNavigateToServerPage(account.AuthToken, account.Username);
    }

    private async Task StoreTokenAndNavigateToServerPage(string token, string userName)
    {
        await _storageService.PutAsync("plexUserName", userName);
        await _storageService.PutAsync("plex_token", token);
        await _navigationService.NavigateToAsync("servers");
    }

    public bool CanLoginWithCredentials => !(string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password));

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task LoginWithBrowser()
    {
        var oauthUrl = await OpenBrowserWithPin();
        var plexPin = await WaitForBrowserLogin(oauthUrl);

        var account = await _plexClient.GetPlexAccountAsync(plexPin.AuthToken);
        await StoreTokenAndNavigateToServerPage(plexPin.AuthToken, account.Username);
    }

    private async Task<OAuthPin> WaitForBrowserLogin(OAuthPin oauthUrl)
    {
        OAuthPin plexPin;
        while (true)
        {
            plexPin = await _plexClient.GetAuthTokenFromOAuthPinAsync(oauthUrl.Id.ToString());
            if (string.IsNullOrEmpty(plexPin.AuthToken))
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            else break;
        }

        return plexPin;
    }

    private async Task<OAuthPin> OpenBrowserWithPin()
    {
        var oauthUrl = await _plexClient.CreateOAuthPinAsync("");
        await _webClientService.OpenAsync(oauthUrl.Url);
        return oauthUrl;
    }
}