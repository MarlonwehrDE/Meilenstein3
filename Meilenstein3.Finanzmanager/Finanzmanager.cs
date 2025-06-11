using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Meilenstein3.Finanzmanager;

public enum FinanzKategorien
{
    Einkommen,
    Lebensmittel,
    Miete,
    Freizeit,
    Sonstiges
    
}

public class Transaktion
{

    public bool istAusgabe { get; set; }
    public double Betrag { get; set; }
    public string Beschreibung { get; set; } = "";
    public DateTime Datum { get; set; } = DateTime.Now;
    public FinanzKategorien Kategorie { get; set; } = FinanzKategorien.Sonstiges;
    

    
}



public class FinanzManager : INotifyPropertyChanged
{
    private double _kontostand;
    public double Kontostand
    {
        get => _kontostand;
        set
        {
            if (_kontostand != value)
            {
                _kontostand = value;
                OnPropertyChanged(nameof(Kontostand));
                OnPropertyChanged(nameof(Color));
            }
        }
    }

    public Brush Color
    {
        get
        {
            if(Kontostand == 0)
                return Brushes.Black;
            else if(Kontostand < 0)
                return Brushes.Red;
            else
            {
                return Brushes.Green;
            }
        }
    }
    
    public ObservableCollection<Transaktion> Transaktionen { get; set; } = new ();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    

    
    
}

