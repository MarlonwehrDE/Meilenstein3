using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Meilenstein3.Einkaufsliste;



public enum Kategorien
{
    Obst,
    Gemuese,
    Fleischprodukte,
    Milchprodukte,
    Haushaltsprodukte,
    Sonstiges,
    
}

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



public class Einkaufsliste : INotifyPropertyChanged
{
    public ObservableCollection<Einkaufsliste_Node> MeineEinkaufsliste { get; set; } = new();
    

    public void AddArtikel(Einkaufsliste_Node artikel)
    {
        MeineEinkaufsliste.Add(artikel);
    }

    public void DeleteArtikel(Einkaufsliste_Node artikel)
    {
        if (!MeineEinkaufsliste.Remove(artikel))
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void ChangeQuantity(Einkaufsliste_Node artikel, int quantity)
    {
        if (MeineEinkaufsliste.Contains(artikel))
        {
            artikel.Menge = quantity;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void ChangeName(Einkaufsliste_Node artikel, string name)
    {
        if (MeineEinkaufsliste.Contains(artikel))
        {
            artikel.Artikelbezeichnung = name;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void ChangePrice(Einkaufsliste_Node artikel, float newPrice)
    {
        if (MeineEinkaufsliste.Contains(artikel))
        {
            artikel.Preis = newPrice;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void ChangeCategorie(Einkaufsliste_Node artikel, Kategorien kategorie)
    {
        if (MeineEinkaufsliste.Contains(artikel))
        {
            artikel.Kategorie = kategorie;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void PrintArtikels()
    {
        foreach (var artikel in MeineEinkaufsliste)
        {
            Console.WriteLine($"{artikel.Artikelbezeichnung} ({artikel.Kategorie}) - {artikel.Menge} Stück, Preis: {artikel.Preis} Euro");
        }
    }
    
    public List<Einkaufsliste_Node> ToList()
    {
        return MeineEinkaufsliste.ToList();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
