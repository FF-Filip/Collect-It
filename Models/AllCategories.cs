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

            Category c1 = new Category();
            c1.Name = "Ksiazki";

            Item i1 = new Item();
            i1.Name = "Ferdydurke";

            c1.Items.Add(i1);
            Categories.Add(c1);
        }

        public void SaveCategories()
        {
            string filePath = Path.Combine(Constants.AppDataPath, "CollectItApp.data.txt");
            Debug.WriteLine(filePath);

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
