using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectIt.Models
{
    class Category
    {
        private string _name;
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        
        public string Name 
        {
            get => _name;
            set { 
                _name = value;
                OnPropertyChanged("Name");
            } 
        }

        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
