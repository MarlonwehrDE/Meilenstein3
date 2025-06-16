using System.Windows;
using System.Windows.Controls;
using Meilenstein3.Einkaufsliste;


namespace Meilenstein3.GUI;

public partial class EinkaufslistePage : Page
{

    bool alreadyLoaded = false;
    public Einkaufsliste.Einkaufsliste MeineEinkaufsliste { get; set; } = new Einkaufsliste.Einkaufsliste();

    public void LoadEinkaufsliste()
    {
        if (!alreadyLoaded)
        {
            MeineEinkaufsliste =  Einkaufsliste.EinkaufslisteSpeicher.Laden();
            alreadyLoaded = true;
            MeineEinkaufsliste.AddEventOnJson(MeineEinkaufsliste.MeineEinkaufsliste);
        }
        
    }
    
    public EinkaufslistePage()
    {
        LoadEinkaufsliste();
        MeineEinkaufsliste.CalculateCostSum();
        InitializeComponent();
        
        DataContext = MeineEinkaufsliste; // Datenbindung setzen
    }

 

    private void ArtikelHinzufügen_Click(object sender, RoutedEventArgs e)
    {
      MeineEinkaufsliste.AddArtikel(new Einkaufsliste_Node("Neuer Eintrag", 0, 0 , Kategorien.Sonstiges));

        DataContext = MeineEinkaufsliste; // Datenbindung setzen
    }

    public void ListeSpeichern_Click(object sender, RoutedEventArgs e)
    {
        Einkaufsliste.EinkaufslisteSpeicher.Speichern(MeineEinkaufsliste.ToList());
    }

    public void Loeschen_Click(object sender, RoutedEventArgs e)
    {
        
        var button = sender as Button;
        var artikel = button?.Tag as Einkaufsliste_Node;

        if (artikel != null)
        {
            MeineEinkaufsliste.DeleteArtikel(artikel);
        }
        
        
    }
    

}

public static class KategorienValues
{
    public static Array All => Enum.GetValues(typeof(Kategorien));
}