using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CollectIt.Models
{
    class Category : INotifyPropertyChanged
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

        public Category() { }

        public Category(string Name)
        {
            this.Name = Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
