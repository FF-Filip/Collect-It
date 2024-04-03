using CollectIt.Views;

namespace CollectIt
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        }
    }
}
