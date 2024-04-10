using CollectIt.Models;
using System.Diagnostics;

namespace CollectIt.Views;

public partial class CategoryInfoPage : ContentPage
{
	public CategoryInfoPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        Debug.WriteLine((BindingContext as Category).Name);
        ItemsCountLabel.Text = (BindingContext as Category).Items.Count.ToString();
        int itemsForSaleCount = (BindingContext as Category).Items.Where(item => item.Status == "Na sprzeda¿").Count();
        int itemsSoldCount = (BindingContext as Category).Items.Where(item => item.Status == "Sprzedany").Count();
        ItemsForSaleLabel.Text = itemsForSaleCount.ToString();
        ItemsSoldLabel.Text = itemsSoldCount.ToString();
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}