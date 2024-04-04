using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectIt.Models
{
    class AllCategories
    {
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        public AllCategories() { }

        public void LoadCategories()
        {
            Categories.Clear();

            string filePath = Path.Combine(Constants.AppDataPath, "CollectItApp.data.txt");
            Debug.WriteLine(filePath);

            if (!File.Exists(filePath))
                SaveCategories();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while(reader.Peek() >= 0)
                    {
                        Category category = new Category();
                        category.Name = reader.ReadLine();
                        Categories.Add(category);
                    }
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void SaveCategories()
        {
            string filePath = Path.Combine(Constants.AppDataPath, "CollectItApp.data.txt");

            if (File.Exists(filePath))
                File.Delete(filePath);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    foreach (Category cat in Categories)
                    {
                        outputFile.WriteLine(cat.Name);
                    }
                    outputFile.Close();
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }
    }
}
