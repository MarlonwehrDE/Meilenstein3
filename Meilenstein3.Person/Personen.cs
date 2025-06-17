
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Meilenstein3.Person
{
    

    class Programm
    {

    static void Main(string[] args) 
    {
            LinkedList<Personen> personen = new LinkedList<Personen>();


    }

    }
     public class Personen
    {
        
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Geburtstag { get; set; }
        public double Einkommen { get; set; }
        public bool Erwerbstaetig { get; set; }

        public Personen(string vorname, string nachname, DateTime geburtstag, double einkommen, bool erwerbstaetig)
        {
            Vorname = vorname;
            Nachname = nachname;
            Geburtstag = geburtstag;
            Einkommen = einkommen;
            Erwerbstaetig = erwerbstaetig;
        }

        public static void SpeicherePersonenListe(LinkedList<Personen> personen)
        {
            string dateipfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personen.json");
            var array = personen.ToArray();
            string json = JsonSerializer.Serialize(array);
            File.WriteAllText(dateipfad, json);
     
        }

        public static LinkedList<Personen> LadePersonenListe()
        {
            string dateipfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personen.json");

            if (!File.Exists(dateipfad))
                return new LinkedList<Personen>();

            try
            {
                string json = File.ReadAllText(dateipfad);
                var array = JsonSerializer.Deserialize<LinkedList<Personen>>(json);
                return new LinkedList<Personen>(array);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Datei:\n" + ex.Message);
                return new LinkedList<Personen>();
            }
        }
        public override string ToString()
        {
            return $"{Vorname} {Nachname}";
        }
    }
        
}

   
