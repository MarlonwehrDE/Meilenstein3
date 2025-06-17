
using System.Windows;



namespace Meilenstein3.GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary


public partial class MainWindow : Window
{
    private readonly EinkaufslistePage _einkaufslistePage = new EinkaufslistePage(); //readonly, damit nur einmal gesetzt wird, und danach nicht mehr verändert werden kann
    private readonly PersonenPage _personenPage = new PersonenPage();
    private readonly FinanzmanagerPage _finanzmanagerPage = new FinanzmanagerPage();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Einkaufsliste_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.Content != _einkaufslistePage)
            MainFrame.Navigate(_einkaufslistePage);
    }

    private void Personen_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.Content != _personenPage)
            MainFrame.Navigate(_personenPage);
    }

    private void Finanzmanager_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.Content != _finanzmanagerPage)
            MainFrame.Navigate(_finanzmanagerPage);
    }
}
