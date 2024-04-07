using CollectIt.Models;
using System.Diagnostics;

namespace CollectIt.Views;

public partial class ItemPage : ContentPage
{
    private Item item { get; set; }

	public ItemPage(Item item = null)
	{
		InitializeComponent();
        this.item = item;
	}

    protected override void OnAppearing()
    {
        Debug.WriteLine(BindingContext as Category);
        if(item != null)
        {
            NameEditor.Text = item.Name;
        }
    }

    private async void AddItem_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEditor.Text))
        {
            AllCategories allCategories = new AllCategories();
            allCategories.LoadCategories();
            Item newItem = new Item(null, NameEditor.Text, (BindingContext as Category).Name);
            if (this.item != null)
            {
                newItem.Id = this.item.Id;
            }
            (BindingContext as Category).Items.Add(newItem);
            allCategories.AddItem(newItem);
            await Shell.Current.GoToAsync("..");
        }
    }
}