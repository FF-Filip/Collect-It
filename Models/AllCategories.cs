using System.Collections.ObjectModel;
using System.Diagnostics;

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
                        List<string> attributes = line.Split(",").ToList();
                        Item item = new Item(attributes[0], attributes[1], bool.Parse(attributes[2]));
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
                                itemsFile.WriteLine(item.Name + "," + cat.Name + "," + item.IsItemSold);
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
        /*
        public void SaveCategories()
        {
            string catFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.categories_data.txt");
            string itemFilePath = Path.Combine(Constants.AppDataPath, "CollectItApp.items_data.txt");

            if (File.Exists(catFilePath))
                File.Delete(catFilePath);

            try
            {
                using (StreamWriter categoriesFile = new StreamWriter(catFilePath))
                {
                    foreach (Category cat in Categories)
                    {
                        categoriesFile.WriteLine(cat.Name);
                    }
                    categoriesFile.Close();
                }

                using (StreamWriter itemsFile = new StreamWriter(itemFilePath))
                {
                    foreach (Category cat in Categories)
                    {
                        foreach (Item item in cat.Items)
                        {
                            itemsFile.WriteLine(item.Name + "," + "," + item.IsItemSold);
                        }
                    }
                    itemsFile.Close();
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }
        */
    }
}
