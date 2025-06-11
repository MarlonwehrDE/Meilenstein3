using System.Windows;
using Meilenstein3.Finanzmanager;
using Meilenstein3.GUI;

namespace Meilenstein3.GUI;

public partial class TransaktionenWindow : Window
{
    
    
    public TransaktionenWindow()
    {
        InitializeComponent();
    }

    public void TransaktionBuchen_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
        
    }
    
    
}
public static class KategorienValuesFinanzen
{
    public static Array All => Enum.GetValues(typeof(Finanzmanager.FinanzKategorien));
}