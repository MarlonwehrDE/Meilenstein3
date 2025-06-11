using System.Reflection.Metadata.Ecma335;
using System.Windows;
using System.Windows.Controls;
using Meilenstein3.Finanzmanager;

namespace Meilenstein3.GUI;

public partial class FinanzmanagerPage : Page
{
    public Finanzmanager.Transaktion FinanzmanagerTransaktion { get; set; } = new();
    
    public FinanzmanagerPage()
    {
        InitializeComponent();
        
        DataContext = FinanzmanagerTransaktion;
    }

    private void TransaktionenHinzufuegen_Click(object sender, RoutedEventArgs e)
    {
        TransaktionenWindow PopUpWindow = new TransaktionenWindow
        {
            Owner = Window.GetWindow(this)
        };
        
        PopUpWindow.ShowDialog();
    }
}