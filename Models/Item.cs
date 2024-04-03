using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectIt.Models
{
    class Item
    {
        private string _name;
        private bool _isItemBought;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public bool IsItemBought
        {
            get => _isItemBought;
            set
            {
                _isItemBought = value;
                OnPropertyChanged("IsItemBought");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
