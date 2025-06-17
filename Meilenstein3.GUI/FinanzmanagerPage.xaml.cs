using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Meilenstein3.Finanzmanager;
using Color = System.Drawing.Color;

namespace Meilenstein3.GUI;

public partial class FinanzmanagerPage : Page
{
   public static FinanzManager finanzManager { get; set; } = new();
   Brush color = Brushes.Black;
   
    
    public FinanzmanagerPage()
    {
        InitializeComponent();
        finanzManager.Transaktionen = FinanzenSpeichern.GetTransaktionen();
        KontostandOnOpen();
        
        
        DataContext = finanzManager;
    }

    public void KontostandOnOpen() //Kontostand berechnen bei Laden aus Datei
    {
        foreach (Transaktion t in finanzManager.Transaktionen)
        {
            if (t.Kategorie == FinanzKategorien.Einkommen)
            {
                finanzManager.Kontostand += t.Betrag;
            }
            else
            {
                finanzManager.Kontostand -= t.Betrag;
            }
        }
    }

    public static void AddGehaltPersonen(double gehalt, string vorname) //Wenn Person mit Gehalt hinzugefügt wird, dann wird es automatisch geaddet
    {
        if (gehalt > 0)
        {
            var addGehaltTransaktion = new Transaktion();
            addGehaltTransaktion.Betrag = gehalt;
            addGehaltTransaktion.Kategorie = FinanzKategorien.Einkommen;
            addGehaltTransaktion.Beschreibung = $"Gehalt von {vorname}";
            finanzManager.Transaktionen.Add(addGehaltTransaktion);
            finanzManager.Kontostand += gehalt;
            Console.WriteLine("Einkommen: " + gehalt);
        }
    }

    public static void AddEinkauf(double kosten) //Berechneten Einkaufspreis hinzufügen
    {
        if (kosten > 0)
        {
            var AddEinkaufTransaktion = new Transaktion();
            AddEinkaufTransaktion.Betrag = kosten;
            AddEinkaufTransaktion.Kategorie = FinanzKategorien.Lebensmittel;
            AddEinkaufTransaktion.Beschreibung = $"Einkaufskosten";
            finanzManager.Transaktionen.Add(AddEinkaufTransaktion);
            finanzManager.Kontostand -= kosten;
        }
        
    }
    
    
    private void TransaktionenHinzufuegen_Click(object sender, RoutedEventArgs e) //Hinzufügen einer Transktion über ein Popupwindow
    {
        var neueTransaktion = new Transaktion(); // Ein neues einzelnes Objekt erstellen
        var popUpWindow = new TransaktionenWindow
        {
            Owner = Window.GetWindow(this),
            DataContext = neueTransaktion // WICHTIG!
        };

        if (popUpWindow.ShowDialog() == true)
        {
            if (neueTransaktion.Kategorie == FinanzKategorien.Einkommen)
            {
                finanzManager.Kontostand += neueTransaktion.Betrag;
            }
            else
            {
                finanzManager.Kontostand -= neueTransaktion.Betrag;
            }
            
            finanzManager.Transaktionen.Add(neueTransaktion);
          
        }
    }
    
    public void LoeschenFinanzen_Click(object sender, RoutedEventArgs e) //Entfernen einer Transaktion
    {
        
        var button = sender as Button;
        var transaktion = button?.Tag as Transaktion;

        if (transaktion != null)
        {
            
            if (transaktion.Kategorie == FinanzKategorien.Einkommen)
            {
                finanzManager.Kontostand -= transaktion.Betrag;
            }
            else
            {
                finanzManager.Kontostand += transaktion.Betrag;
            }
            
            
            finanzManager.Transaktionen.Remove(transaktion);
        }
        
        
    }

    private void TransaktionenSpeichern_Click(object sender, RoutedEventArgs e) //Speichern der Transaktionen als JSON
    {
        List<Transaktion> transaktionen = finanzManager.Transaktionen.ToList();
        FinanzenSpeichern.Speichern(transaktionen);
    }
}