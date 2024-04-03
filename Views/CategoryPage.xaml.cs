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
}