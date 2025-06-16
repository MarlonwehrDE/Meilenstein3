
using System.Windows;



namespace Meilenstein3.GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void Einkaufsliste_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new EinkaufslistePage());
    }
    
    private void Personen_Click(object sender, RoutedEventArgs e)
    {

        MainFrame.Navigate(new PersonenPage());
    }
    
    private void Finanzmanager_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new FinanzmanagerPage());
    }
    
}