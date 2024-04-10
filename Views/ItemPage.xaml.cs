using CollectIt.Models;
using System.Diagnostics;
using System.Globalization;

namespace CollectIt.Views;

public partial class ItemPage : ContentPage
{
    private Item item { get; set; }

	public ItemPage(Item item = null)
	{
		InitializeComponent();
        this.item = item;
        StatusPicker.ItemsSource = new List<string>
        {
            "Nowy",
            "U¿ywany",
            "Na sprzedaŸ",
            "Sprzedany",
            "Chcê kupiæ"
        };
	}

    protected override void OnAppearing()
    {
        Debug.WriteLine(BindingContext as Category);
        if(item != null)
        {
            NameEditor.Text = item.Name;
            StatusPicker.SelectedItem = item.Status;
            PriceEntry.Text = item.Price.ToString();
        }
    }

    private async void AddItem_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEditor.Text))
        {
            AllCategories allCategories = new AllCategories();
            allCategories.LoadCategories();
            Item newItem = new Item(null, NameEditor.Text, (BindingContext as Category).Name, Double.Parse(PriceEntry.Text.Replace(",", "."), CultureInfo.InvariantCulture), StatusPicker.SelectedItem.ToString());
            if (this.item != null)
            {
                newItem.Id = this.item.Id;
            }
            if((BindingContext as Category).Items.Where( item => item.Name == newItem.Name).Count() > 0 && this.item == null)
            {
                bool answer = await DisplayAlert("Uwaga", $"Element {newItem.Name} ju¿ instnieje. Czy chcesz dodaæ taki sam element?", "Tak", "Nie");
                if (!answer)
                    return;
            }
            (BindingContext as Category).Items.Add(newItem);
            allCategories.AddItem(newItem);
            await Shell.Current.GoToAsync("..");
        }
    }
}