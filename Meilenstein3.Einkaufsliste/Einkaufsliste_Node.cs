
using System.ComponentModel;

namespace Meilenstein3.Einkaufsliste;

public class Einkaufsliste_Node : INotifyPropertyChanged
{
   
    private string artikelbezeichnung;
    private int menge;
    private float preis;
    private Kategorien kategorie;
    private bool gekauft;

    public Einkaufsliste_Node(string artikelbezeichnung, int menge, float preis, Kategorien kategorie)
    {
        this.artikelbezeichnung = artikelbezeichnung;
        this.menge = menge;
        this.kategorie = kategorie;
        this.preis = preis;
    }

    public string Artikelbezeichnung
    {
        get => artikelbezeichnung;
        set
        {
            if (artikelbezeichnung != value)
            {
                artikelbezeichnung = value;
                OnPropertyChanged(nameof(Artikelbezeichnung));
            }
        }
    }

    public int Menge
    {
        get => menge;
        set
        {
            if (menge != value)
            {
                menge = value;
                OnPropertyChanged(nameof(Menge));
            }
        }
    }

    public float Preis
    {
        get => preis;
        set
        {
            if (Math.Abs(preis - value) > 0.001f)
            {
                preis = value;
                OnPropertyChanged(nameof(Preis));
            }
        }
    }

    public Kategorien Kategorie
    {
        get => kategorie;
        set
        {
            if (kategorie != value)
            {
                kategorie = value;
                OnPropertyChanged(nameof(Kategorie));
            }
        }
    }

    public bool Gekauft
    {
        get => gekauft;
        set
        {
            if (gekauft != value)
            {
                gekauft = value;
                OnPropertyChanged(nameof(Gekauft));
            }
        }
    }
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


