using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Meilenstein3.Einkaufsliste;



public enum Kategorien
{
    Obst,
    Gemuese,
    Fleischprodukte,
    Milchprodukte,
    Haushaltsprodukte,
    Teigwaren,
    Sonstiges,
    
    
} public class Einkaufsliste : INotifyPropertyChanged
{
    public ObservableCollection<Einkaufsliste_Node> MeineEinkaufsliste { get; set; } = new(); //Liste als ObservableCollection, damit auf Änderung der Liste in der Tabelle reagiert werden kann
    
    public Einkaufsliste()
    {
        MeineEinkaufsliste.CollectionChanged += CollectionChanged; //Event abonnieren, wenn Liste geändert wird (Löschen,Hinzufügen,....)

        // Alle bereits vorhandenen Artikel den EventHandler hinzufügen
        foreach (var item in MeineEinkaufsliste)
        {
            item.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
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
    
    
    private void Einkaufsliste_Node_PropertyChanged(object? sender, PropertyChangedEventArgs e) //Event, was ausgelöst wird, wenn ein Listenelement bearbeitet wird
    {
           CalculateCostSum();
    }
    private void CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)//Event, welches ausgelöst wird, wenn die Liste (als ganzes) verändert wird (z.B. neues Elemnt usw.)
    {
        if (e.OldItems != null) //Alle Elemente, die aus der Collection gelöscht wurden vom EventHandler entfernen
        {
            foreach (Einkaufsliste_Node oldItem in e.OldItems)
            {
                oldItem.PropertyChanged -= Einkaufsliste_Node_PropertyChanged;
            }
        }
        
        if (e.NewItems != null) //Alle neu hinzugefügten Elemente dem event hinzufügen
        {
            foreach (Einkaufsliste_Node newItem in e.NewItems)
            {
                newItem.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
            }
        }

        

        CalculateCostSum();
    }

    


    public void CalculateCostSum() //Berechnung der Kosten für jedes Element der Liste
    {
        double sum = 0;
        foreach (var artikel in MeineEinkaufsliste)
        {
            sum += artikel.Preis * artikel.Menge;
        }
        Gesamtkosten = sum;
        OnPropertyChanged(nameof(Gesamtkosten));
    }

    public void AddEventOnJson(ObservableCollection<Einkaufsliste_Node> meineListe) //nach Laden der Json wird EventHandler wieder hinzugefügt
    {
        meineListe.CollectionChanged -= CollectionChanged; //Falls bereits ein EventHandler geladen ist
        meineListe.CollectionChanged += CollectionChanged;

        foreach (var element in meineListe)
        {
            element.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
        }
        
    }

    

    public void AddArtikel(Einkaufsliste_Node artikel) //Artikel in Liste Hinzufügen und den EventHandler hinzufügen
    {
        MeineEinkaufsliste.Add(artikel);
        artikel.PropertyChanged += Einkaufsliste_Node_PropertyChanged;
    }

    public void DeleteArtikel(Einkaufsliste_Node artikel) //Artikel aus Liste entfernt und ggf. Preis der Artikel aus Gesamtkosten entfernen
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
    

    public event PropertyChangedEventHandler? PropertyChanged;  //Durch Interface implementiert, gibt ein Event aus, wenn ein Propertie eines Elements

    public void OnPropertyChanged(string propertyName) //Methode zum Auslösen des PropertyChanged-Events, um die GUI über Änderungen zu informieren
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
