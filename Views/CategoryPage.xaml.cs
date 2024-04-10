namespace CollectIt.Views;

using Models;
using System.Collections.ObjectModel;
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

    protected override void OnAppearing()
    {
        foreach( Item item in (BindingContext as Category).Items)
        {
            Debug.WriteLine("Image name: " + item.Image);
        }
    }

    private void LoadCategory(string catName)
	{
        AllCategories allCategories = new AllCategories();
        allCategories.LoadCategories();
        Category category = allCategories.Categories.Where(c => c.Name == catName).FirstOrDefault();

        ObservableCollection<Item> itemsNotSold = new ObservableCollection<Item>();
        ObservableCollection<Item> itemsSold = new ObservableCollection<Item>();

        foreach (Item item in category.Items)
        {
            if(item.Status != "Sprzedany")
                itemsNotSold.Add(item);
            else
                itemsSold.Add(item);
        }

        category.Items.Clear();
        category.Items = itemsNotSold;
        foreach (Item item in itemsSold)
        {
            category.Items.Add(item);
        }

        BindingContext = category;
        this.Title = category.Name;
    }

    private async void AddNewItem_Clicked(object sender, EventArgs e)
    {
		ItemPage itemPage = new ItemPage
		{
			BindingContext = this.BindingContext,
		};

		await Navigation.PushAsync(itemPage);
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
					if(item.Id == itemToDelete.Id)
					{
                        cat.Items.Remove(item);
                        try
                        {
                            File.Delete(item.Image);
                        }
                        catch { }
                        break;
                    }
				}
				break;
            }
        }
        (BindingContext as Category).Items.Remove(menuItem.BindingContext as Item);
        allCategories.SaveCategories();
    }

    private async void EditItem_Clicked(object sender, EventArgs e)
    {
		MenuItem menuItem = sender as MenuItem;

        ItemPage itemPage = new ItemPage(menuItem.BindingContext as Item)
        {
            BindingContext = this.BindingContext,
        };

        await Navigation.PushAsync(itemPage);
    }

    private async void CategoryInfo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CategoryInfoPage
        {
            BindingContext = this.BindingContext as Category
        });
    }
}