using Hymn_Book.Model;

namespace Hymn_Book.Views
{
    public partial class HymnDetailPage : ContentPage
    {
        public HymnDetailPage(Hymn hymn)
        {
            InitializeComponent();
            BindingContext = hymn;
        }
    }
}
