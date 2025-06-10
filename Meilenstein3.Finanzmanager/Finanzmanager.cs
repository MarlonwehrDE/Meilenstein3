using System.ComponentModel;

namespace Meilenstein3.Finanzmanager;

public class Transaktion
{
    
    public bool istAusgabe { get; set; }
    public double Betrag { get; set; }
    public string Beschreibung { get; set; } = "";
    public DateTime Datum { get; set; } = DateTime.Now;

  
}

public class FinanzManager
{
    public List<Transaktion> Transaktionen { get; set; } = new ();

    public void NeueEinnahme(double betrag, string beschreibung)
    {
        var transaktion = new Transaktion
        {
            Betrag = betrag,
            Beschreibung = beschreibung,
            istAusgabe = false,
        };
        
        Transaktionen.Add(transaktion);
    }


    public void NeueAusgabe(double betrag, string beschreibung)
    {

        var transaktion = new Transaktion
        {
            Betrag = betrag,
            Beschreibung = beschreibung,
            istAusgabe = true,
        };
        
        Transaktionen.Add(transaktion);

    }
    
}

