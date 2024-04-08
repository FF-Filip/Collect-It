using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace CollectIt.Models
{
    class AllCategories
    {
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public AllCategories() { }

        public void LoadCategories()
        {
            Categories.Clear();

            string catFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.categories_data.txt");
            string itemFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.items_data.txt");
            Debug.WriteLine(catFilePath);

            if (!File.Exists(catFilePath))
                SaveCategories();

            try
            {
                using (StreamReader reader = new StreamReader(catFilePath))
                {
                    while(reader.Peek() >= 0)
                    {
                        Category category = new Category(reader.ReadLine());
                        Categories.Add(category);
                    }
                }

                using (StreamReader reader = new StreamReader(itemFilePath))
                {
                    while (reader.Peek() >= 0)
                    {
                        string line = reader.ReadLine();
                        List<string> attributes = line.Split(";").ToList();
                        Item item = new Item(attributes[0], attributes[1], attributes[2], Double.Parse(attributes[3], CultureInfo.InvariantCulture), attributes[4], bool.Parse(attributes[5]));
                        foreach(Category cat in Categories)
                        {
                            if(cat.Name == item.ParentCategory)
                            {
                                cat.Items.Add(item);
                                break;
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void SaveCategories()
        {
            string catFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.categories_data.txt");
            string itemFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.items_data.txt");

            if (File.Exists(catFilePath) || File.Exists(itemFilePath))
            {
                File.Delete(catFilePath);
                File.Delete(itemFilePath);
            }

            try
            {
                using (StreamWriter categoriesFile = new StreamWriter(catFilePath))
                {
                    using (StreamWriter itemsFile = new StreamWriter(itemFilePath))
                    {
                        foreach (Category cat in Categories)
                        {
                            categoriesFile.WriteLine(cat.Name);
                            foreach (Item item in cat.Items)
                            {
                                itemsFile.WriteLine(item.Id + ";" + item.Name + ";" + cat.Name + ";" + item.Price.ToString() + ";" + item.Status + ";" + item.IsItemSold);
                            }
                        }
                        categoriesFile.Close();
                        itemsFile.Close();
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        public void AddItem(Item newItem)
        {
            foreach (Category cat in Categories)
            {
                if (cat.Name == newItem.ParentCategory)
                {
                    if(cat.Items.Where( item => item.Id == newItem.Id).Count() == 0)
                    {
                        cat.Items.Add(newItem);
                        break;
                    }
                    else
                    {
                        int index = 0;
                        foreach (Item item in cat.Items)
                        {
                            if (item.Id == newItem.Id)
                            {
                                index = cat.Items.IndexOf(item);
                                break;
                            }
                        }
                        cat.Items[index] = newItem;
                    }
                }
            }
            SaveCategories();
        }
    }
}
