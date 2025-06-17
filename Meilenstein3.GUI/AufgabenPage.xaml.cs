using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
using System.Collections.ObjectModel;
using Meilenstein3.Person;


namespace Meilenstein3.GUI
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    /// 
    public class AufgabenZuweisung
    {
        public string[] ZugewiesenePersonen { get; set; } = new string[6];
    }
    public partial class AufgabenPage : Page
    {
        private List<Personen> personenListe;
        private ComboBox[] comboBoxes;
        private readonly string personenPfad = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personen.json");
        private readonly string zuweisungenPfad = "zugewiesenepersonen.json";
        //TODO muss automatisch aus JSON gezogen werden
        private bool[] isGreen = new bool[6];
        public AufgabenPage()
        {
            InitializeComponent();
            if (File.Exists("aufgabenStatus.json"))
            {
                try
                {
                    string json = File.ReadAllText("aufgabenStatus.json");
                    bool[] loaded = JsonSerializer.Deserialize<bool[]>(json);
                    if (loaded != null && loaded.Length == 6)
                    {
                        isGreen = loaded;
                        SetzeButtonFarben(); // Buttons beim erstem Laden setzen
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Laden des Aufgabenstatus: " + ex.Message);
                }
            }
            comboBoxes = new[] { comboAufgabe1, comboAufgabe2, comboAufgabe3, comboAufgabe4, comboAufgabe5, comboAufgabe6 };
            this.Loaded += AufgabenPage_Loaded;

        }
        private void AufgabenPage_Loaded(object sender, RoutedEventArgs e)
        {
            LadePersonen();
            InitialisiereComboBoxes();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var neueZuweisung = new AufgabenZuweisung();

            for (int i = 0; i < comboBoxes.Length; i++)
            {
                if (comboBoxes[i].SelectedItem is Personen person)
                {
                    neueZuweisung.ZugewiesenePersonen[i] = $"{person.Vorname} {person.Nachname}";
                }
            }

            File.WriteAllText(zuweisungenPfad, JsonSerializer.Serialize(neueZuweisung, new JsonSerializerOptions { WriteIndented = true }));
        }
        private void InitialisiereComboBoxes()
        {
            AufgabenZuweisung zuweisung;

            if (File.Exists(zuweisungenPfad))
            {
                string json = File.ReadAllText(zuweisungenPfad);
                zuweisung = JsonSerializer.Deserialize<AufgabenZuweisung>(json);
            }
            else
            {
                zuweisung = new AufgabenZuweisung();
            }

            for (int i = 0; i < comboBoxes.Length; i++)
            {
                ComboBox box = comboBoxes[i];

                // Personen in die ComboBox einfügen
                box.ItemsSource = personenListe;
                box.DisplayMemberPath = null; 
                string gespeicherterName = zuweisung.ZugewiesenePersonen[i];

                if (!string.IsNullOrWhiteSpace(gespeicherterName))
                {
                    foreach (var person in personenListe)
                    {
                        string name = person.Vorname + " " + person.Nachname;

                        if (name == gespeicherterName)
                        {
                            box.SelectedItem = person;
                            break;
                        }
                    }
                }
            }
        }
        private void LadePersonen()
        {
            if (File.Exists(personenPfad))
            {
                var json = File.ReadAllText(personenPfad);
                personenListe = JsonSerializer.Deserialize<LinkedList<Personen>>(json)?.ToList() ?? new List<Personen>();
            }
            else
            {
                personenListe = new List<Personen>();
            }
        }
        private void SetzeButtonFarben()
        {
            ColorToggleButton_Saugen.Background = isGreen[0] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            ColorToggleButton_Staubwischen.Background = isGreen[1] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            ColorToggleButton_Wischen.Background = isGreen[2] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            ColorToggleButton_Kochen.Background = isGreen[3] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            ColorToggleButton_Spülmaschine.Background = isGreen[4] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
            ColorToggleButton_Waschmaschine.Background = isGreen[5] ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }
        private void ColorToggleButton_Click(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (sender is Button btn)
            {
                name = ((Button)sender).Name;
            }

            if (isGreen[FindIndex(name)])
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Red);
                ToggleColor(name);
            }
            else
            {
                ((Button)sender).Background = new SolidColorBrush(Colors.Green);
                ToggleColor(name);
            }
            SafeToJSON(); // Speichert nach jeder Änderung
        }
        private void SafeToJSON()
        {
            string json = JsonSerializer.Serialize(isGreen);
            File.WriteAllText("aufgabenStatus.json", json);
        }

        private int FindIndex(string name)
        {
            switch (name)
            {
                case "ColorToggleButton_Saugen":
                    return 0;
                    break;
                case "ColorToggleButton_Staubwischen":
                    return 1;
                    break;
                case "ColorToggleButton_Wischen":
                    return 2;
                    break;
                case "ColorToggleButton_Kochen":
                    return 3;
                    break;
                case "ColorToggleButton_Spülmaschine":
                    return 4;
                    break;
                case "ColorToggleButton_Waschmaschine":
                    return 5;
                    break;
                default:
                    throw new Exception("Fehler: Name vom Button konnte nicht gefunden werden");
                    break;
            }
        }
        //Geändert Farbe im Array speichern
        private void ToggleColor(string name)
        {
            int index = FindIndex(name);

            if (isGreen[index])
            {
                isGreen[index] = false;
            }
            else
            {
                isGreen[index] = true;
            }
        }
    }

}
