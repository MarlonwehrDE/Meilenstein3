using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;
using System.Windows;

namespace Meilenstein3.Person
{


    class Programm
    {

    static void Main(string[] args) 
    {
        LinkedList<Personen> personen= new LinkedList<Personen>();


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
            string json = JsonConvert.SerializeObject(array, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(dateipfad, json);
            MessageBox.Show($"Gespeichert unter:\n{dateipfad}\nMit {array.Length} Personen.");
        }
        public static LinkedList<Personen> LadePersonenListe()
        {
            string dateipfad = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personen.json");

            if (!File.Exists(dateipfad))
                return new LinkedList<Personen>();

            try
            {
                string json = File.ReadAllText(dateipfad);
                var array = JsonConvert.DeserializeObject<Personen[]>(json);
                return new LinkedList<Personen>(array ?? new Personen[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Datei:\n" + ex.Message);
                return new LinkedList<Personen>();
            }
        }

    }
        
}

   





