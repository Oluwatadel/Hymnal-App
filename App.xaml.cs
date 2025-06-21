using Hymn_Book.Intefaces.Repository;

namespace Hymn_Book
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            //_hymnRepository.ResetDatabase();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = _serviceProvider.GetRequiredService<AppShell>();
            return new Window(shell);
        }

    }
};