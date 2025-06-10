using System.IO;
using System.Text.Json;

namespace Meilenstein3.Finanzmanager;


public class FinanzenSpeichern()
{
    static string _ordnerPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Meilenstein3.Finanzmanager");
    static string _dateiPfad = Path.Combine(_ordnerPfad, "Transaktionen.json");

    
    public static void Speichern(List<Transaktion> transaktionen){

        if (!File.Exists(_dateiPfad))
        {
            File.Create(_dateiPfad);
        }
        
        string json =
            JsonSerializer.Serialize(transaktionen, new JsonSerializerOptions { WriteIndented = true }); //Formatieren der Liste zu JSON-Format mit WhiteSpaces
        File.WriteAllText(_dateiPfad, json);
    }

    public static List<Transaktion>? GetTransaktionen()
    {

        if (!File.Exists(_dateiPfad))
        {
            return new List<Transaktion>();
        }
        
        return JsonSerializer.Deserialize<List<Transaktion>>(File.ReadAllText(_dateiPfad)) ?? new List<Transaktion>();
        
    }

}