namespace Hymn_Book
{
    public partial class AppShell : Shell
    {
        public Command HelpCommand { get; }
        public AppShell()
        {
            InitializeComponent();
            HelpCommand = new Command(OnHelpClicked);
            BindingContext = this; 
        }

        private async void OnHelpClicked()
        {
            // You can open a dialog, navigate somewhere,
            // or just display an alert
            await Shell.Current.DisplayAlert(
                "Help",
                "Here you can provide help or contact us.",
                "OK");
        }   
    }

}
