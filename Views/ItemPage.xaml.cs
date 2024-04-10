using CollectIt.Models;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CollectIt.Views;

public partial class ItemPage : ContentPage
{
    private Item item { get; set; }
    private FileResult fileResult { get; set; }


    public ItemPage(Item item = null)
	{
		InitializeComponent();
        this.item = item;
        StatusPicker.ItemsSource = new List<string>
        {
            "Nowy",
            "U¿ywany",
            "Na sprzeda¿",
            "Sprzedany",
            "Chcê kupiæ"
        };

        RatingPicker.ItemsSource = new List<int>
        {
            1,2,3,4,5,6,7,8,9,10
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
            RatingPicker.SelectedItem = item.Rating;
        }
    }

    private async void AddItem_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(NameEditor.Text))
        {
            AllCategories allCategories = new AllCategories();
            allCategories.LoadCategories();

            if(!Regex.IsMatch(NameEditor.Text, "^[\\p{L}\\- ]+$"))
            {
                await DisplayAlert("Uwaga", "Niedozwolona nazwa przedmiotu", "Ok");
                return;
            }

            Item newItem = new Item(null, NameEditor.Text, (BindingContext as Category).Name, Double.Parse(PriceEntry.Text.Replace(",", "."), CultureInfo.InvariantCulture), StatusPicker.SelectedItem.ToString(), int.Parse(RatingPicker.SelectedItem.ToString()));
            if (this.item != null)
            {
                newItem.Id = this.item.Id;
                newItem.Image = this.item.Image;
            }
            if((BindingContext as Category).Items.Where( item => item.Name == newItem.Name).Count() > 0 && this.item == null)
            {
                bool answer = await DisplayAlert("Uwaga", $"Element {newItem.Name} ju¿ instnieje. Czy chcesz dodaæ taki sam element?", "Tak", "Nie");
                if (!answer)
                    return;
            }
            
            if(this.fileResult != null)
            {
                if(File.Exists(newItem.Image))
                    File.Delete(newItem.Image);

                if(fileResult.ContentType == "image/png")
                    newItem.Image = FileSystem.AppDataDirectory + "\\Images\\" + $"{newItem.Id}.png";
                else
                    newItem.Image = FileSystem.AppDataDirectory + "\\Images\\" + $"{newItem.Id}.jpg";

                if (!Directory.Exists(FileSystem.AppDataDirectory + "\\Images"))
                    Directory.CreateDirectory(FileSystem.AppDataDirectory + "\\Images");


                File.Copy(fileResult.FullPath, newItem.Image, true);
            }

            (BindingContext as Category).Items.Add(newItem);
            allCategories.AddItem(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }

    private async void AddImage_Clicked(object sender, EventArgs e)
    {
        fileResult = await PickAndShow(new PickOptions());
        if (fileResult != null)
        {
            if (fileResult.FileName.EndsWith("png"))
                fileResult.ContentType = "image/png";
            else
                fileResult.ContentType = "image/jpg";
        }
    }

    public async Task<FileResult> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

            return result;
        }
        catch (Exception ex)
        {

        }

        return null;
    }
}