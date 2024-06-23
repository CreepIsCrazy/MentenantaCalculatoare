using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculatoare.Shared.Order;

public class Cart : INotifyPropertyChanged
{
    private List<Item> _items = [];
    public List<Item> Items
    {
        get => _items;
        set 
        { 
            _items = value; 
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(NumberOfItems));
            OnPropertyChanged(nameof(TotalAmount));
        }
    }


    public int NumberOfItems => Items.Count;
    public double TotalAmount => Items.Sum(x => x.Price * x.Quantity);

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
