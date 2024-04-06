using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectIt.Models
{
    class Item : INotifyPropertyChanged
    {
        private string _name;
        private string _parentCategory;
        private bool _isItemSold;

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

        public bool IsItemSold
        {
            get => _isItemSold;
            set
            {
                _isItemSold = value;
                OnPropertyChanged("IsItemBought");
            }
        }

        public Item(string Name, string parentCategory, bool IsItemSold = false)
        {
            this.Name = Name;
            this.ParentCategory = parentCategory;
            this.IsItemSold = IsItemSold;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
