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
    
} public class Einkaufsliste : INotifyPropertyChanged
{
    
    public Einkaufsliste()
    {
        MeineEinkaufsliste.CollectionChanged += CollectionChanged;

        // Wichtig: Bereits existierende Artikel abonnieren
        foreach (var item in MeineEinkaufsliste)
        {
            item.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
            item.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
        }
    }

    private void CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (Einkaufsliste_Node newItem in e.NewItems)
            {
                newItem.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
            }
        }

        if (e.OldItems != null)
        {
            foreach (Einkaufsliste_Node oldItem in e.OldItems)
            {
                oldItem.PropertyChanged -= Einkaufsliste_Node_PropertyChanged;
            }
        }

        CalculateCostSum();
    }


   

    private double gesamtkosten;

    public double Gesamtkosten
    {
        get => gesamtkosten;

        set
        {
            if (value != gesamtkosten)
            {
                gesamtkosten = value;
                OnPropertyChanged(nameof(Gesamtkosten));
            }
        }
    }
    public ObservableCollection<Einkaufsliste_Node> MeineEinkaufsliste { get; set; } = new();
    
    private void Einkaufsliste_Node_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
           CalculateCostSum();
    }

    


    public void CalculateCostSum()
    {
        double sum = 0;
        foreach (var artikel in MeineEinkaufsliste)
        {
            sum += artikel.Preis * artikel.Menge;
        }
        Gesamtkosten = sum;
        OnPropertyChanged(nameof(Gesamtkosten));
    }

    

    public void AddArtikel(Einkaufsliste_Node artikel)
    {
        MeineEinkaufsliste.Add(artikel);
        artikel.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
    }

    public void DeleteArtikel(Einkaufsliste_Node artikel)
    {
        gesamtkosten -= artikel.Preis * artikel.Menge;
        OnPropertyChanged(nameof(Gesamtkosten));
        if (!MeineEinkaufsliste.Remove(artikel))
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }
/* Durch die Properties nicht mehr nötig!!
 
 
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
    */
    
    public List<Einkaufsliste_Node> ToList()
    {
        return MeineEinkaufsliste.ToList();
    }
    

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged(string propertyName)
    {
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
