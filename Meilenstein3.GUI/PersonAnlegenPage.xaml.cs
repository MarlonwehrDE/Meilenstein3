using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Xml;
using Meilenstein3.Person;


namespace Meilenstein3.GUI
{
    /// <summary>
    /// Interaktionslogik für PersonAnlegenPage.xaml
    /// </summary>
   
    public partial class PersonAnlegenPage : Page
    {
       
        public PersonAnlegenPage()
        {
            InitializeComponent();
        }
        LinkedList<Person.Personen> personenListe = Meilenstein3.Person.Personen.LadePersonenListe();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string vorname = txtVorname.Text;
            string nachname = txtNachname.Text;

            // Geburtsdatum aus dem Kalender
            DateTime? geburtstagNullable = calGeburtstag.SelectedDate;
            if (geburtstagNullable == null)
            {
                MessageBox.Show("Bitte ein Geburtsdatum auswählen!");
                return;
            }
            DateTime geburtstag = geburtstagNullable.Value;

            // Einkommen parsen
            if (!double.TryParse(txtEinkommen.Text, out double einkommen) || einkommen < 0)
            {
                MessageBox.Show("Bitte ein gültiges, positives Einkommen eingeben.");
                return;
            }
           

            // Erwerbstätigkeit
            bool erwerbstaetig = chkErwerbstaetig.IsChecked == true;

            // Vorname/Nachname validieren (optional)
            if (!Namechecker(vorname) || !Namechecker(nachname))
            {
                MessageBox.Show("Vor- und Nachname dürfen nur Buchstaben enthalten.");
                return;
            }

            // Objekt erstellen und zur Liste hinzufügen
            Meilenstein3.Person.Personen neuePerson = new Personen(vorname, nachname, geburtstag, einkommen, erwerbstaetig);
            personenListe.AddLast(neuePerson);
            Meilenstein3.Person.Personen.SpeicherePersonenListe(personenListe);

            
        }
       

        static bool Namechecker(string name)
        {
            bool notLetter = true;

            int i = 0;
            while (i < name.Length)
            {
                for (int j = (int)'A'; j <= (int)'Z'; j++) //Hier statt 2 mal da ne schleife machen vllt. delegate für vorname und nachname
                {
                    if (name[i] == (char)j)
                    {
                        notLetter = false;

                    }
                }
                if (notLetter)
                {
                    for (int j = (int)'a'; j <= (int)'z'; j++)
                    {
                        if (name[i] == (char)j)
                        {
                            notLetter = false;

                        }
                    }
                    if (notLetter) return false;
                }
                i++;


            }
            return true;



        }
    }
}
