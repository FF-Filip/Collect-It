using CollectIt.Models;
using System.Diagnostics;

namespace CollectIt.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new AllCategories();
        }

        protected override void OnAppearing()
        {
            ((AllCategories)BindingContext).LoadCategories();
        }
        
        private async void AddNewCategory_Clicked(object sender, EventArgs e)
        {
            string catName = await DisplayPromptAsync("Nowa kategoria", "Podaj nazwę nowej kategorii");

            if(!string.IsNullOrWhiteSpace(catName))
            {
                Category category = new Category();
                category.Name = catName;

                ((AllCategories)BindingContext).Categories.Add(category);
                ((AllCategories)BindingContext).SaveCategories();
            } else
            {
                await DisplayAlert("Uwaga", "Nieprawidłowa nazwa kategorii", "Ok");
            }
        }
        
        private async void CategorySelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count != 0)
            {
                var selectedCategory = (Category) e.CurrentSelection[0];
                Debug.WriteLine("cat name: " + selectedCategory.Name);
                await Shell.Current.GoToAsync($"{nameof(CategoryPage)}?{nameof(CategoryPage.selectedCategoryName)}={selectedCategory.Name}");
                CategoryCollection.SelectedItem = null;
            }
        }
    }
}
