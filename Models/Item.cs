using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectIt.Models
{
    public class Item : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _parentCategory;
        private double _price;
        private string _status;
        private bool _isItemSold;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ParentCategory
        {
            get => _parentCategory;
            set
            {
                _parentCategory = value;
                OnPropertyChanged("ParentCategory");
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public bool IsItemSold
        {
            get => _isItemSold;
            set
            {
                _isItemSold = value;
                OnPropertyChanged("IsItemBought");
            }
        }

        public Item()
        {

        }

        public Item(string Id, string Name, string ParentCategory, double Price, string Status, bool IsItemSold = false)
        {
            if (Id == null)
            {
                this.Id = Guid.NewGuid().ToString();
            }
            else
            {
                this.Id = Id;
            }
                
            this.Name = Name;
            this.ParentCategory = ParentCategory;
            this.Price = Price;
            this.Status = Status;
            this.IsItemSold = IsItemSold;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
