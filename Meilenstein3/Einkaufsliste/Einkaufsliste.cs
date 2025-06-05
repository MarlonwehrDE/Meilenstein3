using System.Security.Cryptography.X509Certificates;

namespace Meilenstein3;

public class Einkaufsliste_Node
{
    public string artikelbezeichnung { get; set; }
    public int menge { get; set; }
    public float preis { get; set;}
    public bool gekauft { get; private set; }

    public Einkaufsliste_Node(string artikelbezeichnung, int menge, float preis)
    {
        this.menge = menge;
        this.preis = preis;
        this.artikelbezeichnung = artikelbezeichnung;
    }

}

public class Einkaufsliste
{
    LinkedList<Einkaufsliste_Node> einkaufsliste = new LinkedList<Einkaufsliste_Node>();

    public void AddArtikel(Einkaufsliste_Node artikel)
    {
        einkaufsliste.AddLast(artikel);
    }

    public void DeleteArtikel(Einkaufsliste_Node artikel)
    {
        if (einkaufsliste.Contains(artikel))
        {
            einkaufsliste.Remove(artikel);
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void changeQuantity(Einkaufsliste_Node artikel, int quantity)
    {
        if (einkaufsliste.Contains(artikel))
        {
            artikel.menge = quantity;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
        
    }

    public void changeName(Einkaufsliste_Node artikel, string name)
    {
        if (einkaufsliste.Contains(artikel))
        {
            artikel.artikelbezeichnung = name;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void changePrice(int newPrice, Einkaufsliste_Node artikel)
    {
        if (einkaufsliste.Contains(artikel))
        {
            artikel.preis = newPrice;
        }
        else
        {
            throw new NullReferenceException("Artikel nicht in der Einkaufsliste gefunden!");
        }
    }

    public void PrintArtikels()
    {
        foreach (Einkaufsliste_Node tempArtikel in einkaufsliste)
        {
            Console.WriteLine($"{tempArtikel.artikelbezeichnung} - {tempArtikel.menge} Stück, Preis: {tempArtikel.preis} Euro");
        }
    }
}
