namespace CollectIt.Views;

using Models;
using System.Diagnostics;

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
			AllCategories allCategories = new AllCategories();
			allCategories.LoadCategories();
            Item item = new Item(result, (BindingContext as Category).Name);
            (BindingContext as Category).Items.Add(item);
			foreach(Category cat in allCategories.Categories)
			{
				if(cat.Name == (BindingContext as Category).Name)
				{
					cat.Items.Add(item);
					break;
				}
			}
			allCategories.SaveCategories();
        }
	}

    private void DeleteItem_Clicked(object sender, EventArgs e)
    {
        MenuItem menuItem = sender as MenuItem;
		Item itemToDelete = menuItem.BindingContext as Item;
        AllCategories allCategories = new AllCategories();
        allCategories.LoadCategories();
        foreach (Category cat in allCategories.Categories)
        {
            if (cat.Name == (BindingContext as Category).Name)
            {
				foreach(Item item in cat.Items)
				{
					if(item.Name == itemToDelete.Name)
					{
                        cat.Items.Remove(item);
                        break;
                    }
				}
				break;
            }
        }
        (BindingContext as Category).Items.Remove(menuItem.BindingContext as Item);
        allCategories.SaveCategories();
    }
}