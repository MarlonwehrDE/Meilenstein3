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
   public FinanzManager finanzManager { get; set; } = new();
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

    private void TransaktionenHinzufuegen_Click(object sender, RoutedEventArgs e)
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
    
    public void LoeschenFinanzen_Click(object sender, RoutedEventArgs e)
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

    private void TransaktionenSpeichern_Click(object sender, RoutedEventArgs e)
    {
        List<Transaktion> transaktionen = finanzManager.Transaktionen.ToList();
        FinanzenSpeichern.Speichern(transaktionen);
    }
}