using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Meilenstein3.GUI
{

    public partial class PersonenPage : Page
    {
        public ObservableCollection<Meilenstein3.Person.Personen> PersonenListe { get; set; }

        public PersonenPage()
        {
            InitializeComponent();

            // Lade die LinkedList und konvertiere sie zu ObservableCollection
            var geladeneListe = Meilenstein3.Person.Personen.LadePersonenListe();
            PersonenListe = new ObservableCollection<Meilenstein3.Person.Personen>(geladeneListe);

            DataContext = this;

            foreach (Meilenstein3.Person.Personen person in PersonenListe)
            {
                FinanzmanagerPage.AddGehaltPersonen(person.Einkommen, person.Vorname);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new PersonAnlegenPage(this));
            
        }

       private void Loeschen_Click(object sender, RoutedEventArgs e)
    {
    var button = sender as Button;
    var person = button?.Tag as Meilenstein3.Person.Personen;

    if (person != null && PersonenListe.Contains(person))
    {
        PersonenListe.Remove(person);

        // Nach dem Entfernen: Liste speichern
        var linkedList = new LinkedList<Meilenstein3.Person.Personen>(PersonenListe);
        Meilenstein3.Person.Personen.SpeicherePersonenListe(linkedList);
    }
    }
    }
}
