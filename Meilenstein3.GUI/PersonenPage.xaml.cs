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
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new PersonAnlegenPage());
        }

        private void Loeschen_Click(object sender, RoutedEventArgs e)
        {

            
               
            
        }
    }
}
