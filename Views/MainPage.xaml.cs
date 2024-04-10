using CollectIt.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

            if (string.IsNullOrWhiteSpace(catName))
                return;

            if(!Regex.IsMatch(catName, "^[\\p{L}\\- ]+$"))
            {
                await DisplayAlert("Uwaga", "Niedozowolona nazwa kategorii", "Ok");
                return;
            }

            Category category = new Category();
            category.Name = catName.Trim();

            ((AllCategories)BindingContext).Categories.Add(category);
            ((AllCategories)BindingContext).SaveCategories();
        }
        
        private async void CategorySelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count != 0)
            {
                var selectedCategory = (Category) e.CurrentSelection[0];
                await Shell.Current.GoToAsync($"{nameof(CategoryPage)}?{nameof(CategoryPage.selectedCategoryName)}={selectedCategory.Name}");
                CategoriesCollection.SelectedItem = null;
            }
        }

        private void DeleteCategory_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            foreach(Item item in (menuItem.BindingContext as Category).Items)
            {
                try
                {
                    File.Delete(item.Image);
                }
                catch (Exception ex) { }
            }

            ((AllCategories)BindingContext).Categories.Remove(menuItem.BindingContext as Category);
            ((AllCategories)BindingContext).SaveCategories();
        }

        private async void ChangeCategoryName_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string result = await DisplayPromptAsync("Zmiana nazwy", $"Podaj nową nazwę dla kategorii {(menuItem.BindingContext as Category).Name}");

            if (string.IsNullOrWhiteSpace(result))
            {
                await DisplayAlert("Uwaga", "Nieprawidłowa nazwa kategorii", "Ok");
                return;
            }

            (BindingContext as AllCategories).Categories.Where( c => c == (menuItem.BindingContext as Category)).FirstOrDefault().Name = result;
            ((AllCategories)BindingContext).SaveCategories();
        }
    }
}
