namespace CollectIt.Views;

using Models;

[QueryProperty(nameof(selectedCategoryName), nameof(selectedCategoryName))]

public partial class CategoryPage : ContentPage
{
	public string selectedCategoryName
	{
		get => selectedCategoryName;
		set
		{
			LoadCategory(value);
		}
	}

	public CategoryPage()
	{
		InitializeComponent();
	}

	private void LoadCategory(string catName)
	{
        AllCategories allCategories = new AllCategories();
        allCategories.LoadCategories();
        Category category = allCategories.Categories.Where(c => c.Name == catName).FirstOrDefault();
        BindingContext = category;
    }

    private async void AddNewItem_Clicked(object sender, EventArgs e)
    {
		string result = await DisplayPromptAsync("Nowy przedmiot kolekcji", "Wprowadź nazwę przedmiotu");
		if(!string.IsNullOrWhiteSpace(result))
		{
			Item item = new Item();
			item.Name = result;
			(BindingContext as Category).Items.Add(item);
		}
    }
}