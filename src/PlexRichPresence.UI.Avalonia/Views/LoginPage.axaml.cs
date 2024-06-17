using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LoginPageViewModel = PlexRichPresence.UI.Avalonia.ViewModels.LoginPageViewModel;

namespace PlexRichPresence.UI.Avalonia.Views;

public partial class LoginPage : UserControl
{
    public LoginPage()
    {
        InitializeComponent();
        DataContext = this.CreateInstance<LoginPageViewModel>();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}