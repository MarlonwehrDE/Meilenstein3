using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

namespace Meilenstein3.Einkaufsliste;

public class EinkaufslisteSpeicher
{

    static string ordnerPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Meilenstein3.Einkaufsliste");
    static string dateiPfad = Path.Combine(ordnerPfad, "einkaufsliste.json");

    public static void Speichern(List<Einkaufsliste_Node> liste)
    {


        // Ordner erstellen, falls nicht vorhanden
        if (!Directory.Exists(ordnerPfad))
        {
            Directory.CreateDirectory(ordnerPfad);
        }

        // JSON serialisieren und speichern
        string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dateiPfad, json);
    }
    
    


    public static Einkaufsliste Laden()
    {
        if (!File.Exists(dateiPfad))
            return new Einkaufsliste();

        var json = File.ReadAllText(dateiPfad);
        var liste = JsonSerializer.Deserialize<List<Einkaufsliste_Node>>(json) ?? new();

        return new Einkaufsliste { MeineEinkaufsliste = new ObservableCollection<Einkaufsliste_Node>(liste) };
    }
}
