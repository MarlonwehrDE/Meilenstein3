using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Meilenstein3.Finanzmanager;


public class FinanzenSpeichern
{
    static string _ordnerPfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finanzdaten");
    static string _dateiPfad = Path.Combine(_ordnerPfad, "Transaktionen.json");

    public static void Speichern(List<Transaktion> transaktionen)
    {
        if (!Directory.Exists(_ordnerPfad))
        {
            Directory.CreateDirectory(_ordnerPfad);
        }

        string json = JsonSerializer.Serialize(transaktionen, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_dateiPfad, json);
    }

    public static ObservableCollection<Transaktion> GetTransaktionen()
    {
        if (!File.Exists(_dateiPfad))
        {
            return new ObservableCollection<Transaktion>();
        }

        var liste = JsonSerializer.Deserialize<List<Transaktion>>(File.ReadAllText(_dateiPfad)) ?? new List<Transaktion>();
        return new ObservableCollection<Transaktion>(liste);
    }
}


