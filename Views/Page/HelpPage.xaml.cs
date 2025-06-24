namespace Hymn_Book.Views.Page;

public partial class HelpPage : ContentPage
{
    public HelpPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Call the global command
        if (Application.Current is App app && app.HelpCommand.CanExecute(null))
        {
            app.HelpCommand.Execute(null);
        }
    }
}